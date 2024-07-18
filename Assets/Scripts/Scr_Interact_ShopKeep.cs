using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Interact_ShopKeep : Scr_Interactable
{
    public List<string> RegularDialogueLines;
    public DialogueManager DlgManager;  
    public GameObject UIToOpen;

    public override void Interact(){
        DlgManager.StartDialogue(RegularDialogueLines,id,UIToOpen);
    }    
}
