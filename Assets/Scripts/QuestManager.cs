using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string QuestID;
    public string QuestInfo;
    public float QuestTime;
    public float Distance;
    public string QuestItem;
    public GameObject QuestTarget;

    public int QuestReward;

}

public class QuestManager : MonoBehaviour
{
    public List<QuestData> PlayerQuests;
    public GameObject QuestListContainer;

    public GameObject QuestItemPrefab;
    public GameObject WaypointPrefab;
    public GameObject QuestUI;
    public TimerScript GameTimer;
    public GameObject Player;
    public GameObject Waypoints;
    public InventoryManager inventoryManager;
    public TextMeshProUGUI GameTimerTXT;
    public TextMeshProUGUI TotalGoldTXT;
    public TextMeshProUGUI FailedQuestCountTXT;

    public GameObject NPCPool;

    public int GlobalTimerBaseValue = 10;
    public int GlobalTimerCurrentValue;
    public int FailedQuests = 0;
    public int TotalGold = 0;

    public int questGiverPopulationPercentage = 25;

    public bool AllowGameOverCheck = true;

    void Start(){
        QuestUI.SetActive(false);
        ClearWaypoints();
        FailedQuestCountTXT.text = FailedQuests.ToString();
        TotalGoldTXT.text = TotalGold.ToString();
        RandomizeQuestGiver();                  
        inventoryManager.updateInvWeight();
    }

    void Update(){        
        UpdateTargetDistance();
        if(PlayerQuests.Count > 0){
            UpdateWaypoint();
        }
        CheckQuestTimers();  
        UpdateGlobalTimer();      
        
    }

    public void SetGameTimer(int value){
        GlobalTimerBaseValue = value;
        GameTimer.SetTime(GlobalTimerBaseValue);   
        GlobalTimerCurrentValue = (int)GameTimer.GetCurrentTime();   
    }

    public void UpdateQuestList(){
        ClearQuestList();
        if(PlayerQuests.Count <= 0){
            QuestUI.SetActive(false);
            
        }
        else{
            QuestUI.SetActive(true); 
            if(QuestListContainer.transform.childCount < PlayerQuests.Count){
                GameObject newObject = Instantiate(QuestItemPrefab);
                newObject.transform.SetParent(QuestListContainer.transform);
                newObject.transform.localScale = new Vector3(1,1,1);
                newObject.SetActive(false); 
            }            
            int childind = 0;
            foreach (QuestData QD in PlayerQuests){
                QuestListContainer.transform.GetChild(childind).gameObject.SetActive(true);
                QuestListContainer.transform.GetChild(childind).gameObject.GetComponent<QuestListItem>().SetData(QD);                
                
                childind++;
            }
                        
        }
        UpdateWaypointCount();
        

    }
    
    public void AddQuest(QuestData questData){
        PlayerQuests.Add(questData);
        UpdateQuestList();
    }
    public void CompleteQuest(string questID){
        QuestData value = PlayerQuests.Find(item => item.QuestID == questID);  
        AddGold(value.QuestReward + (int)value.QuestTime);      
        PlayerQuests.Remove(value);
        UpdateQuestList(); 
        RandomizeQuestGiver();                    
    }

    public void ClearQuestList(){
        foreach(Transform child in QuestListContainer.transform){
            child.gameObject.SetActive(false);
        }
    }

    public void PauseAllTimers(){
        foreach(Transform child in QuestListContainer.transform){
            child.gameObject.GetComponent<QuestListItem>().Timer.PauseTimer();
        }
        GameTimer.PauseTimer();
    }
    public void ResumeAllTimers(){
        foreach(Transform child in QuestListContainer.transform){
            child.gameObject.GetComponent<QuestListItem>().Timer.ResumeTimer();
        }
        GameTimer.ResumeTimer();
    }

    public void UpdateTargetDistance(){
        if(PlayerQuests.Count > 0){
            foreach(QuestData item in PlayerQuests){
                item.Distance = Vector3.Distance(Player.transform.position,item.QuestTarget.transform.position);            
            }
        }
        
    }
    public void UpdateWaypoint(){
        float waypointOffset = 10;
        foreach(Transform child in Waypoints.transform){  
            if(child.gameObject.GetComponent<WaypointScript>().questData != null){
                WaypointScript childscript = child.GetComponent<WaypointScript>();
                Vector3 pos =  childscript.questData.QuestTarget.transform.position;
                pos.y = pos.y + 1;
                Vector2 ScreenPos = Camera.main.WorldToScreenPoint(pos);
                //clamps waypoint to edges of screen
                float maxX = Screen.width  - waypointOffset;
                float minX = waypointOffset;
                float maxY = Screen.height - waypointOffset;
                float minY = waypointOffset;
                ScreenPos.x = Mathf.Clamp(ScreenPos.x,minX,maxX);
                ScreenPos.y = Mathf.Clamp(ScreenPos.y,minY,maxY);

                if(Vector3.Dot(childscript.questData.QuestTarget.transform.position - Player.transform.position, Player.GetComponent<PlayerController>().playerCamera.transform.forward) < 0){
                    if(ScreenPos.x < Screen.width){
                        ScreenPos.x = maxX;
                    }
                    else{
                        ScreenPos.x = minX;
                    }
                }

                child.transform.position = ScreenPos;
                child.gameObject.SetActive(true);
            }
            
        }
    }
    public void UpdateWaypointCount(){
        //checks if there is need to add more waypoints based on amount of quests
        while(Waypoints.transform.childCount < PlayerQuests.Count){            
            Debug.Log("Adding waypoints");
            GameObject newObject = Instantiate(WaypointPrefab);
                newObject.transform.SetParent(Waypoints.transform);
                newObject.transform.localScale = new Vector3(1,1,1);
                newObject.SetActive(false); 
        }

        ClearWaypoints();
        int ind = 0;
        foreach(Transform child in Waypoints.transform){            
            WaypointScript childScript = child.gameObject.GetComponent<WaypointScript>();
            if(ind < PlayerQuests.Count){
                childScript.SetInfo(PlayerQuests[ind]);
            }
            
            ind++;
        }

    }
    public void ClearWaypoints(){
        foreach(Transform child in Waypoints.transform){
            child.gameObject.GetComponent<WaypointScript>().ClearWaypointData();
            child.gameObject.SetActive(false);
        }
    }

    public void CheckQuestTimers(){
        foreach(QuestData data in PlayerQuests){
            if(data.QuestTime <= 0){
                FailQuest(data.QuestID);
            }            
        }
    }
    public void FailQuest(string id){
        QuestData value = PlayerQuests.Find(item => item.QuestID == id);
        PlayerQuests.Remove(value);
        inventoryManager.RemoveItem(value.QuestItem);
        FailedQuests++;
        FailedQuestCountTXT.text = FailedQuests.ToString();
        UpdateQuestList();
        RandomizeQuestGiver();
    }
    public void AddGold(int gold){   
        TotalGold += gold;     
        TotalGoldTXT.text = TotalGold.ToString();
    } 
    public void RemoveGold(int gold){   
        TotalGold -= gold;     
        TotalGoldTXT.text = TotalGold.ToString();
    }   

    public void UpdateGlobalTimer(){
        GlobalTimerCurrentValue = (int)GameTimer.GetCurrentTime();
        int mins = GlobalTimerCurrentValue / 60;
        int sec = GlobalTimerCurrentValue%60;
        string secondstr;

        if(sec < 10){
            secondstr = "0" + sec.ToString();
        }
        else{
            secondstr = sec.ToString();
        }


        GameTimerTXT.text = mins.ToString() + ":" + secondstr;
    }
    public int GetQuestCount(){
        //return amount of quest giver
        int questCount = 0;
        foreach(Transform child in NPCPool.transform){
            if(child.GetComponent<Scr_Interact_NPC>().hasQuest){
                questCount++;
            }
        }
        return questCount;
    }
    public void RandomizeQuestGiver(){
        int questGiverMaxCount = (int)Mathf.Ceil((NPCPool.transform.childCount) * (questGiverPopulationPercentage/100f));
        int randomizationattempt = 0;        
        while(GetQuestCount() < questGiverMaxCount && randomizationattempt < questGiverMaxCount){
            int rnd = UnityEngine.Random.Range(0, (NPCPool.transform.childCount));
            Scr_Interact_NPC npcTarget = NPCPool.transform.GetChild(rnd).GetComponent<Scr_Interact_NPC>();
            if(npcTarget.hasQuest == false && npcTarget.isQuestTarget == false){
                npcTarget.MakeQuestGiver();              
            }
            else{
                randomizationattempt++;
            }
            
        }
        if(randomizationattempt >= questGiverMaxCount ){
            Debug.Log("Failed to make a quest giver");
        }
        
        //check amount of quest giver first
        //make a quest giver once a quest is finished or failed
    }
    public void StartGameTimer(){
        GameTimer.StartTimer();
    }
}

 


