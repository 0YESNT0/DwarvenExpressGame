using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Interact_ShopKeep : Scr_Interactable
{
    public List<DialogueData> RegularDialogueLines;
    public DialogueManager DlgManager;  
    public List<GameObject> UIToOpen;

    public override void Interact(){
        int rnd = UnityEngine.Random.Range(0,RegularDialogueLines.Count - 1);
        DlgManager.StartDialogue(RegularDialogueLines[rnd].Dialogueline, UIToOpen);         
    }    
}
