using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string TalkerName;
    public string DialogueText;
}


public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueTalkerName;
    public GameObject dialogueTalkerNameBG;
    public PlayerController Player;
    public QuestManager QuestManager;

    private int currentDialogueLine = 0;
    private List<DialogueLine> dialogueList;
    private string talkerName;
    private List<GameObject> UIToOpenList;
    private List<GameObject> UIToCloseList;
    //trade list
    //quest give

    public void StartDialogue(List<DialogueLine> dlgList){
        if(dlgList.Count > 0){
            dialogueList = dlgList;        
            Player.canMove = false;
            Player.InteractSystem.canInteract = false;
            UpdateDialogueDisp(); 
            dialogueUI.SetActive(true);  
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;   
            QuestManager.PauseAllTimers();   
        }
        else{
            Debug.Log("Dialogue Is empty");
        }    
                     
    }
    public void StartDialogue(List<DialogueLine> dlgList, List<GameObject> UIToOpen){
        if(dlgList.Count > 0){
            dialogueList = dlgList;        
            Player.canMove = false;
            Player.InteractSystem.canInteract = false;
            UpdateDialogueDisp(); 
            dialogueUI.SetActive(true);  
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;   
            QuestManager.PauseAllTimers(); 
            UIToOpenList = UIToOpen;            
        }
        else{
            Debug.Log("Dialogue Is empty");
        }
        
                     
    }
    public void StartDialogue(List<DialogueLine> dlgList, List<GameObject> UIToOpen,List<GameObject> UIToClose){
        if(dlgList.Count > 0){
            dialogueList = dlgList;        
            Player.canMove = false;
            Player.InteractSystem.canInteract = false;
            UpdateDialogueDisp(); 
            dialogueUI.SetActive(true);  
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;   
            QuestManager.PauseAllTimers(); 
            UIToOpenList = UIToOpen;
            UIToCloseList = UIToClose;           
        }
        else{
            Debug.Log("Dialogue Is empty");
        }
        
                     
    }

    public void UpdateDialogueDisp(){
        if(dialogueList[currentDialogueLine].TalkerName == String.Empty){
            dialogueTalkerNameBG.gameObject.SetActive(false);            
        }
        else{
            dialogueTalkerNameBG.gameObject.SetActive(true); 
            dialogueTalkerName.text = dialogueList[currentDialogueLine].TalkerName;                    
        }
        
        dialogueText.text = dialogueList[currentDialogueLine].DialogueText;
        
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

         
        
        if(UIToOpenList != null){
            Debug.Log("Opening UI");
            foreach(GameObject obj in UIToOpenList){
                obj.SetActive(true);
            }
        }
        else{
            Player.InteractSystem.canInteract = true;
            Player.canMove = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; 
            QuestManager.UpdateQuestList();
            QuestManager.ResumeAllTimers();
        }
        if(UIToCloseList != null){
            foreach(GameObject obj in UIToCloseList){
                obj.SetActive(false);
            }
        }
        UIToOpenList = null;
        UIToCloseList = null;
        
    }

}
