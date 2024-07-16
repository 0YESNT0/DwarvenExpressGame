using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class WaypointScript : MonoBehaviour
{
    public QuestData questData;
    public TextMeshProUGUI TargetNameTXT;
    public TextMeshProUGUI TargetDistanceTXT;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        TargetDistanceTXT.text = ((int)questData.Distance).ToString() + " m";
        
    }

    public void SetInfo(QuestData data){
        questData = data;
        TargetNameTXT.text = data.QuestTarget.GetComponent<Scr_Interact_NPC>().id;
    }
    public void ClearWaypointData(){
        questData = null;
    }
}
