using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TradingData
{
    public string GiveItemID;
    public string RecieveItemID;
}



public class UpgradeManager : MonoBehaviour
{
    public GameObject UpgradeUI;
    public PlayerController Player;
    public bool tradeUIActivate = false;
    public QuestManager questManager;
    public InventoryManager invManager;

    public int SpeedBasecost;
    public int JumpBasecost;
    public int InvBasecost;
    public int WeightBasecost;

    public int SpeedLvl = 0;
    public int JumpBaseLvl = 0;
    public int InvBaseLvl = 0;
    public int WeightBaseLvl = 0;

    private int SpeedCurrentcost;
    private int JumpCurrentcost;
    private int InvCurrentcost;
    private int WeightCurrentcost;

    public TextMeshProUGUI SpeedCostTXT;
    public TextMeshProUGUI SpeedLVLTXT;
    public TextMeshProUGUI JumpCostTXT;
    public TextMeshProUGUI JumpLVLTXT;
    public TextMeshProUGUI InvCostTXT;
    public TextMeshProUGUI InvLVLTXT;
    public TextMeshProUGUI WeightCostTXT;
    public TextMeshProUGUI WeightLVLTXT;
    // Start is called before the first frame update
    void Start()
    {
        if(UpgradeUI.activeSelf){
            UpgradeUI.SetActive(false);
        }
        SpeedCostTXT.text = SpeedBasecost.ToString();
        SpeedLVLTXT.text = SpeedLvl.ToString();
        
    }

    public void CloseTradingUI(){
        UpgradeUI.SetActive(false);
        Player.InteractSystem.canInteract = true;
        Player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  
    }
    public void OpenTradingUI(){
        UpgradeUI.SetActive(true);
        Player.InteractSystem.canInteract = false;
        Player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
        tradeUIActivate = false;
    }

    public void UpgradeSpeed(){
        //do check if player has enough gold
        //reduce gold based on cost
        SpeedLvl++;
        SpeedCurrentcost = SpeedBasecost * SpeedLvl;
        Player.walkingspeedMod = Player.walkingspeedIncrease * SpeedLvl;


        //updates text in upgrade ui
        SpeedCostTXT.text = SpeedCurrentcost.ToString();
        SpeedLVLTXT.text = SpeedLvl.ToString();
        

    }
    public void UpgradeJump(){

    }
    public void UpgradeInventorySlots(){

    }
    public void UpgradeWeightCap(){

    }
}

