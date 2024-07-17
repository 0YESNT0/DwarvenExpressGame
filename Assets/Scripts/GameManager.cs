using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject DialogueUI;
    public GameObject AlchemyUI;
    public GameObject SmithingUI;
    public GameObject TradingUI;
    public GameObject Player;
    public GameObject QuestManager;
    public GameObject InventoryManager;

    public GameObject InGameUI;
    public GameObject GameOverPanel;
    public GameObject LoseOnTimeoutTxt;
    public GameObject LoseOnDeliveryFailTxt;

    private bool isLose = false;

    void Start(){
        InGameUI.SetActive(true);
    }

    void Update(){
        GameOverCheck();
    }
    public void GameOverCheck(){
        if((QuestManager.GetComponent<QuestManager>().GlobalTimerCurrentValue <= 0 || QuestManager.GetComponent<QuestManager>().FailedQuests >= 3) && isLose == false){
            //shows and hides specific UI
            InGameUI.SetActive(false);
            GameOverPanel.SetActive(true);
            //shows cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

    public void QuitBTN(){

    }
    public void MainMenuBTN(){

    }
    public void PlayAgainTBN(){

    }

}
