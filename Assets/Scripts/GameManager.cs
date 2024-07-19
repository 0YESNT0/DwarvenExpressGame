using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<DialogueLine> NarrativeDialogue;
    public GameObject DialogueUI;
    public GameObject Player;
    public GameObject QuestManager;
    public GameObject InventoryManager;

    public GameObject InGameUI;
    public GameObject GameOverPanel;

    //gameover text
    public GameObject LoseOnTimeoutTxt;
    public GameObject LoseOnDeliveryFailTxt;
    public GameObject GameOverTXT;
    //win text
    public GameObject WinMessage;


    private bool isLose = false;

    void Start(){
        InGameUI.SetActive(true);
        Player.GetComponent<PlayerController>().InteractSystem.canInteract = false;
        Player.GetComponent<PlayerController>().canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    void Update(){
        if(QuestManager.GetComponent<QuestManager>().AllowGameOverCheck){
            GameOverCheck();
        }
        
    }
    public void GameOverCheck(){
        if((QuestManager.GetComponent<QuestManager>().GlobalTimerCurrentValue <= 0 || QuestManager.GetComponent<QuestManager>().FailedQuests >= 3) && isLose == false){
            //shows and hides specific UI
            InGameUI.SetActive(false);
            GameOverPanel.SetActive(true);
            //shows cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WinMessage.SetActive(false);
            if(QuestManager.GetComponent<QuestManager>().GlobalTimerCurrentValue <= 0){
                LoseOnTimeoutTxt.SetActive(true);
                LoseOnDeliveryFailTxt.SetActive(false);
                return;
            }
            if(QuestManager.GetComponent<QuestManager>().FailedQuests >= 3){
                LoseOnDeliveryFailTxt.SetActive(true);
                LoseOnTimeoutTxt.SetActive(false);
                return;
            }
        }
        
    }
    public void WinGame(){
        QuestManager.GetComponent<QuestManager>().PauseAllTimers();
        //hides fail messages
        LoseOnDeliveryFailTxt.SetActive(false);
        LoseOnTimeoutTxt.SetActive(false);
        GameOverTXT.SetActive(false);
        
        GameOverPanel.SetActive(true);
        WinMessage.SetActive(true);
    }    

}
