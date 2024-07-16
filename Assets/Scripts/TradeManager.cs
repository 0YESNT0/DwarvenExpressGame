using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingData
{
    public string GiveItemID;
    public string RecieveItemID;
}



public class TradeManager : MonoBehaviour
{
    public GameObject TradingUI;
    public PlayerController Player;
    public bool tradeUIActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseTradingUI(){
        TradingUI.SetActive(false);
        Player.InteractSystem.canInteract = true;
        Player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  
    }
    public void OpenTradingUI(){
        TradingUI.SetActive(true);
        Player.InteractSystem.canInteract = false;
        Player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
        tradeUIActivate = false;
    }
}

