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
    public QuestManager questManager;
    public InventoryManager invManager;
    public GameManager gameManager;

    public int SpeedBasecost;
    public int JumpBasecost;
    public int InvBasecost;
    public int WeightBasecost;

    public int SpeedLvl;
    public int JumpLvl;
    public int InvLvl;
    public int WeightLvl;

    private int SpeedCurrentcost;
    private int JumpCurrentcost;
    private int InvCurrentcost;
    private int WeightCurrentcost;

    public int MedicineCost;

    public TextMeshProUGUI SpeedCostTXT;
    public TextMeshProUGUI SpeedLVLTXT;
    public TextMeshProUGUI JumpCostTXT;
    public TextMeshProUGUI JumpLVLTXT;
    public TextMeshProUGUI InvCostTXT;
    public TextMeshProUGUI InvLVLTXT;
    public TextMeshProUGUI WeightCostTXT;
    public TextMeshProUGUI WeightLVLTXT;
    public TextMeshProUGUI MedicineCostTXT;
    // Start is called before the first frame update
    void Start()
    {    
        SpeedCostTXT.text = SpeedBasecost.ToString();
        SpeedLVLTXT.text = SpeedLvl.ToString();

        JumpCostTXT.text = JumpBasecost.ToString();
        JumpLVLTXT.text = JumpLvl.ToString();

        InvCostTXT.text = InvBasecost.ToString();
        InvLVLTXT.text = InvLvl.ToString();

        WeightCostTXT.text = WeightBasecost.ToString();
        WeightLVLTXT.text = WeightLvl.ToString();

        MedicineCostTXT.text = MedicineCost.ToString();
        SpeedCurrentcost = SpeedBasecost;
        JumpCurrentcost = JumpBasecost;
        InvCurrentcost = InvBasecost;
        WeightCurrentcost = WeightBasecost;
        
    }

    public void CloseTradingUI(){
        UpgradeUI.SetActive(false);
        Player.InteractSystem.canInteract = true;
        Player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;  
        questManager.ResumeAllTimers();
    }
    public void OpenTradingUI(){
        UpgradeUI.SetActive(true);
        Player.InteractSystem.canInteract = false;
        Player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;  
    }

    public void UpgradeSpeed(){
        //do check if player has enough gold
        if(questManager.TotalGold > SpeedCurrentcost){
            //reduce gold
            questManager.RemoveGold(SpeedCurrentcost);
            SpeedLvl++;
            SpeedCurrentcost = SpeedBasecost * SpeedLvl;
            Player.walkingspeedMod = Player.walkingspeedIncrease * SpeedLvl;            
            //updates text in upgrade ui
            SpeedCostTXT.text = SpeedCurrentcost.ToString();
            SpeedLVLTXT.text = SpeedLvl.ToString();
        }
        
        

    }
    public void UpgradeJump(){
        if(questManager.TotalGold > JumpCurrentcost){
            questManager.RemoveGold(JumpCurrentcost);
            JumpLvl++;
            JumpCurrentcost = JumpBasecost * JumpLvl;
            Player.JumpHeightMod = Player.JumpHeightIncrease * JumpLvl;

            //updates text in upgrade ui
            JumpCostTXT.text = JumpCurrentcost.ToString();
            JumpLVLTXT.text = JumpLvl.ToString();
        }
    }
    public void UpgradeInventorySlots(){
        if(questManager.TotalGold > InvCurrentcost){
            questManager.RemoveGold(InvCurrentcost);
            InvLvl++;
            InvCurrentcost = InvBasecost * InvLvl;
            Player.inventorySlotsMod = Player.inventorySlotsIncrease * InvLvl;
            
            //updates text in upgrade ui
            InvCostTXT.text = InvCurrentcost.ToString();
            InvLVLTXT.text = InvLvl.ToString();
            invManager.SetInvSlots(Player.inventorySlotsMod + Player.inventorySlots );
        }
    }
    public void UpgradeWeightCap(){
        if(questManager.TotalGold > WeightCurrentcost){
            //reduce gold
            questManager.RemoveGold(WeightCurrentcost);
            WeightLvl++;
            WeightCurrentcost = WeightBasecost * WeightLvl;
            Player.WeightCapMod = Player.WeightCapIncrease * WeightLvl;
            

            //updates text in upgrade ui
            WeightCostTXT.text = WeightCurrentcost.ToString();
            WeightLVLTXT.text = WeightLvl.ToString();
            invManager.updateInvWeight();
        }
    }

    public void BuyMedicine(){
        if(questManager.TotalGold > MedicineCost){
            gameManager.WinGame();
        }
        //win game
    }
}

