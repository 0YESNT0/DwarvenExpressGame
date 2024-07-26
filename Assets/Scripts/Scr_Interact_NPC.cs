using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Interact_NPC : Scr_Interactable
{
    public List<DialogueData> RegularDialogueLines;
    public List<DialogueData> QuestGiveDialogueLines;
    public List<DialogueData> ItemGetLines;
    public GameObject NPCPool;
    public string ItemToReceive;

    public string QuestItemToDeliver;
    public int DeliveryDifficulty;
    public bool hasQuest = false; 
    private int QuestTimer;   

    public DialogueManager DlgManager;  
    public InventoryManager invManager;

    public GameObject QuestidentifierOBJ;

    void Start(){
        //Sets managers
        QuestTimer = 180;       
        CheckIfShowQuestIdentifier();    
    }
    public override void Interact()
    {
        if(hasQuest && invManager.HasEmptySlot() != null){
            //adds item when npc has delivery quest
            invManager.Additem(QuestItemToDeliver);
            QuestData newquest = new QuestData();

            //sets up randomized quest ID and target
            newquest.QuestID = (UnityEngine.Random.Range(1,100)).ToString();
            Item itemToAdd = invManager.ItemDatabase.Find(item => item.ItemID == QuestItemToDeliver);
            int rnd = UnityEngine.Random.Range(0, (NPCPool.transform.childCount));
            GameObject npcTarget = NPCPool.transform.GetChild(rnd).gameObject;
            while(npcTarget.GetComponent<Scr_Interact_NPC>().id == id || npcTarget.GetComponent<Scr_Interact_NPC>().isQuestTarget == true || npcTarget.GetComponent<Scr_Interact_NPC>().hasQuest == true){
                rnd = UnityEngine.Random.Range(0, (NPCPool.transform.childCount));
                npcTarget = NPCPool.transform.GetChild(rnd).gameObject;
            }                       
            float npcdistance = Vector3.Distance(gameObject.transform.position,npcTarget.transform.position);
            newquest.QuestInfo = "Deliver " + itemToAdd.ItemName + " to " + npcTarget.GetComponent<Scr_Interact_NPC>().id;
            //need to work on randomizing quest time randomization
            newquest.QuestTime = QuestTimer * ((npcdistance/100) + 0.20f);
            //sets quest target
            newquest.QuestTarget = npcTarget;
            //sets initial distance to delivery target
            newquest.Distance = npcdistance;
            newquest.QuestReward = (int)(newquest.Distance * 2);
            //quest item id
            newquest.QuestItem = QuestItemToDeliver;

            //sets npc target the quest id they complete on interact
            npcTarget.GetComponent<Scr_Interact_NPC>().isQuestTarget = true;
            npcTarget.GetComponent<Scr_Interact_NPC>().questIDToComplete = newquest.QuestID;
            npcTarget.GetComponent<Scr_Interact_NPC>().ItemToReceive = itemToAdd.ItemID;

            questMngr.AddQuest(newquest);
            rnd = UnityEngine.Random.Range(0,QuestGiveDialogueLines.Count - 1);
            DlgManager.StartDialogue(QuestGiveDialogueLines[rnd].Dialogueline);               
            hasQuest = false;         
        }
        else if(isQuestTarget){
            isQuestTarget = false;
            //completes delivery if they are a delivery target
            questMngr.CompleteQuest(questIDToComplete);
            int rnd = UnityEngine.Random.Range(0,ItemGetLines.Count - 1);
            DlgManager.StartDialogue(ItemGetLines[rnd].Dialogueline); 
            if(invManager.Hasitem(ItemToReceive) != null){
                invManager.RemoveItem(ItemToReceive);
            }
            
            questIDToComplete = null;
            
            
        }
        else{
            int rnd = UnityEngine.Random.Range(0,RegularDialogueLines.Count - 1);
            DlgManager.StartDialogue(RegularDialogueLines[rnd].Dialogueline); 
        }
        CheckIfShowQuestIdentifier();
    }

    public void CheckIfShowQuestIdentifier(){
        if(hasQuest){
            QuestidentifierOBJ.SetActive(true);
        }
        else{
            QuestidentifierOBJ.SetActive(false);
        }
    }

    public void MakeQuestGiver(){
        int randItemInd = UnityEngine.Random.Range(0, invManager.ItemDatabase.Count-1);         
        QuestItemToDeliver = invManager.ItemDatabase[randItemInd].ItemID;
        hasQuest = true;
        CheckIfShowQuestIdentifier();

    }

}
