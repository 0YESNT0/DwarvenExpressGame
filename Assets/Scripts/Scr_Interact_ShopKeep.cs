using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Interact_ShopKeep : Scr_Interactable
{
    public List<DialogueLine> RegularDialogueLines;
    public DialogueManager DlgManager;  
    public List<GameObject> UIToOpen;

    public override void Interact(){
        DlgManager.StartDialogue(RegularDialogueLines,UIToOpen);
    }    
}
