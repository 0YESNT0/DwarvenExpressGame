using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueTalkerName;
    public PlayerController Player;
    public QuestManager QuestManager;

    private int currentDialogueLine = 0;
    private List<string> dialogueList;
    private string talkerName;
    
    //trade list
    //quest give

    public void StartDialogue(List<string> dlgList, string talker){
        dialogueList = dlgList;        
        talkerName = talker;
        Player.canMove = false;
        Player.InteractSystem.canInteract = false;
        UpdateDialogueDisp(); 
        dialogueUI.SetActive(true);  
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;   
        QuestManager.PauseAllTimers();     
                     
    }

    public void UpdateDialogueDisp(){
        if(talkerName == null){
            dialogueTalkerName.gameObject.SetActive(false);            
        }
        else{
            dialogueTalkerName.gameObject.SetActive(true); 
            dialogueTalkerName.text = talkerName;                    
        }
        
        dialogueText.text = dialogueList[currentDialogueLine];
        
    }
    public void NextDialogueLine(){
        currentDialogueLine++;
        if(currentDialogueLine > dialogueList.Count()-1){
            EndDialogue();
        }
        else{
            UpdateDialogueDisp();       
        }
    }

    public void EndDialogue(){
        currentDialogueLine = 0;
        dialogueList = null;
        dialogueUI.SetActive(false);

        Player.InteractSystem.canInteract = true;
        Player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  

        QuestManager.UpdateQuestList();
        QuestManager.ResumeAllTimers();
        
    }

}
