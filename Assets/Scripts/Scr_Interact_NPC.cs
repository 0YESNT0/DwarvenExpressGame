using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_Interact_NPC : Scr_Interactable
{
    public List<string> RegularDialogueLines;
    public List<string> QuestGiveDialogueLines;
    public List<string> ItemGetLines;
    public GameObject NPCPool;
    public string ItemToReceive;

    public string QuestItemToDeliver;
    public int DeliveryDifficulty;
    public bool hasQuest = false;    

    private DialogueManager DlgManager;  
    private InventoryManager invManager;

    public GameObject QuestidentifierOBJ;

    void Start(){
        //Sets managers
        DlgManager = GameObject.Find("GameManager").GetComponent<GameManager>().DialogueUI.GetComponent<DialogueManager>();        
        questMngr = GameObject.Find("GameManager").GetComponent<GameManager>().QuestManager.GetComponent<QuestManager>();
        invManager = GameObject.Find("GameManager").GetComponent<GameManager>().InventoryManager.GetComponent<InventoryManager>();
        CheckIfShowQuestIdentifier();
    }
    public override void Interact()
    {
        if(hasQuest){
            //adds item when npc has delivery quest
            invManager.Additem(QuestItemToDeliver);
            QuestData newquest = new QuestData();

            //sets up randomized quest and target
            newquest.QuestID = (UnityEngine.Random.Range(1,100)).ToString();
            Item itemToAdd = invManager.ItemDatabase.Find(item => item.ItemID == QuestItemToDeliver);
            int rnd = UnityEngine.Random.Range(0, (NPCPool.transform.childCount));
            GameObject npcTarget = NPCPool.transform.GetChild(rnd).gameObject;
            while(npcTarget.GetComponent<Scr_Interact_NPC>().id == id || npcTarget.GetComponent<Scr_Interact_NPC>().isQuestTarget == true || npcTarget.GetComponent<Scr_Interact_NPC>().hasQuest == true){
                rnd = UnityEngine.Random.Range(0, (NPCPool.transform.childCount));
                npcTarget = NPCPool.transform.GetChild(rnd).gameObject;
            }            
            
            newquest.QuestInfo = "Deliver " + itemToAdd.ItemName + " to " + npcTarget.GetComponent<Scr_Interact_NPC>().id;
            //need to work on randomizing quest time randomization
            newquest.QuestTime = 5;
            //sets quest target
            newquest.QuestTarget = npcTarget;
            //sets initial distance to delivery target
            newquest.Distance = Vector3.Distance(gameObject.transform.position,npcTarget.transform.position);
            //quest item id
            newquest.QuestItem = QuestItemToDeliver;

            //sets npc target the quest id they complete on interact
            npcTarget.GetComponent<Scr_Interact_NPC>().isQuestTarget = true;
            npcTarget.GetComponent<Scr_Interact_NPC>().questIDToComplete = newquest.QuestID;
            npcTarget.GetComponent<Scr_Interact_NPC>().ItemToReceive = itemToAdd.ItemID;

            questMngr.AddQuest(newquest);

            DlgManager.StartDialogue(QuestGiveDialogueLines,id);               
            hasQuest = false;         
        }
        else if(isQuestTarget){
            //completes delivery if they are a delivery target
            questMngr.CompleteQuest(questIDToComplete);
            DlgManager.StartDialogue(ItemGetLines,id);
            if(invManager.Hasitem(ItemToReceive) != null){
                invManager.RemoveItem(ItemToReceive);
            }
            
            questIDToComplete = null;
            isQuestTarget = false;
        }
        else{
            DlgManager.StartDialogue(RegularDialogueLines,id);
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

}
