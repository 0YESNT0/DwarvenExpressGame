using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonScript : MonoBehaviour
{
    public string SceneToLoad;
    public List<GameObject> ActivatePanels;
    public List<GameObject> DeactivatePanels;
    public GameObject DurationSelectionPanel;
    public QuestManager Qmanager;
    public DialogueManager DialogueM;
    public GameManager gManager;
    // Start is called before the first frame update
    public void QuitBTN(){
        Application.Quit();
    }
    public void PlayGameBTN(){
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
        //goes to actual game scene
    }

    public void SetDurationTo15Min(){
        Qmanager.SetGameTimer(900);
        Startnarrative();
    }
    public void SetDurationTo10Min(){
        Qmanager.SetGameTimer(600);
        Startnarrative();
    }
    public void SetDurationTo5Min(){
        Qmanager.SetGameTimer(300);
        Startnarrative();
    }

    public void Startnarrative(){
        Qmanager.AllowGameOverCheck = true;
        DurationSelectionPanel.SetActive(false);
        DialogueM.StartDialogue(gManager.NarrativeDialogue,null,DeactivatePanels);
    }

}
