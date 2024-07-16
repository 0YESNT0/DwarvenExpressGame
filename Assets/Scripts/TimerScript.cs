using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{    
    private float baseTime;
    private float currentTime;
    private bool isPaused = false;
    private bool isStopped = true;
    private bool justFinished = false;


    void Update()
    {
        if (isPaused == false){
            currentTime -= Time.deltaTime;
        }
        
        if(currentTime <= 0 && isStopped == false){
            StopTimer();
            justFinished = true;
        }
    }

    public void SetTime(float time){
        baseTime = time;
        currentTime = time;
    }
    public void StartTimer(){
        isPaused = false;
        isStopped = false;
    }

    public void PauseTimer(){
        isPaused = true;
    }

    public void ResumeTimer(){
        isPaused = false;
    }
    public void StopTimer(){        
        isStopped = true;
        isPaused = true;        
    }
    public bool isTimerPaused(){
        return isPaused;
    }
    public float GetCurrentTime(){        
        return currentTime;
    }

    public bool TimerEnded(){
        if(justFinished == true){
            Debug.Log("timer end");
            justFinished = false;
            return true;
        }
        else{
            return false;
        }
    }
}
