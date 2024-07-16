using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestListItem : MonoBehaviour
{
    public QuestData questData;
    public TextMeshProUGUI questTimerTxt;
    public TextMeshProUGUI questNameTxt;
    public TextMeshProUGUI questDistanceTxt;
    public TimerScript Timer;   
    public bool QuestFailed = false; 
    void Start(){        
    }
    void Update(){
        questData.QuestTime = Timer.GetCurrentTime();      
        questTimerTxt.text = ((int)questData.QuestTime).ToString(); 
        questDistanceTxt.text = ((int)questData.Distance).ToString() + " m";
    }
    public void SetData(QuestData qD){
        questData = qD;

        questNameTxt.text = qD.QuestInfo; 
        questTimerTxt.text = qD.QuestTime.ToString();
        Timer.SetTime(qD.QuestTime);
        Timer.StartTimer();

    }

    public string GetQuestID(){
        return questData.QuestID;;
    }

    public void setDistanceValue(int dist){
        questData.Distance = dist;
    }
}
