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
        Qmanager.SetGameTimer(60*15);
        Startnarrative();
    }
    public void SetDurationTo10Min(){
        Qmanager.SetGameTimer(60*10);
        Startnarrative();
    }
    public void SetDurationTo30Min(){
        Qmanager.SetGameTimer(60 * 30);
        Startnarrative();
    }

    public void Startnarrative(){
        Qmanager.AllowGameOverCheck = true;
        DurationSelectionPanel.SetActive(false);
        DialogueM.StartDialogue(gManager.NarrativeDialogue,null,DeactivatePanels);
    }

    public void ResumeGame(){
        foreach(GameObject item in DeactivatePanels){
            item.SetActive(false);
        }
        Qmanager.ResumeAllTimers();
        Qmanager.Player.GetComponent<PlayerController>().InteractSystem.canInteract = true;
        Qmanager.Player.GetComponent<PlayerController>().canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    public void PauseGame(){
        foreach(GameObject item in ActivatePanels){
            item.SetActive(true);
        }
        Qmanager.Player.GetComponent<PlayerController>().InteractSystem.canInteract = false;
        Qmanager.Player.GetComponent<PlayerController>().canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        Qmanager.PauseAllTimers();
    }

}
