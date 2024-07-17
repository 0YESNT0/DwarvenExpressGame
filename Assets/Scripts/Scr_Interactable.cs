using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scr_Interactable : MonoBehaviour
{
    public string id;
    public bool isQuestTarget = false;
    public string questIDToComplete;
    public QuestManager questMngr;
    public abstract void Interact();
}
