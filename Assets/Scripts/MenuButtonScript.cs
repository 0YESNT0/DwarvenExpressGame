using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    public string ChangeToScene;
    public GameObject ActivatePanel;
    public List<GameObject> DeactivatePanels;
    public QuestManager Qmanager;
    // Start is called before the first frame update
    public void QuitBTN(){
        Application.Quit();
    }
    public void MainMenuBTN(){

    }
    public void PlayAgainTBN(){

    }
    public void PlayGameBTN(){
        //goes to actual game scene
    }

    public void SetDurationTo15Min(){

    }
    public void SetDurationTo10Min(){

    }
    public void SetDurationTo5Min(){

    }

}
