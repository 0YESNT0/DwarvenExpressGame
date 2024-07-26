using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Item
{
    public string ItemID;
    public string ItemName;
    public int Weight;  
    public Sprite ItemIcon; 
}

public class InventoryManager : MonoBehaviour
{
    public List<Item> ItemDatabase;
    public GameObject Player;
    public GameObject Inventory;
    public TextMeshProUGUI WeightTXT; 
    public GameObject InventorySlotPrefab;

    void Start(){
        SetInvSlots(Player.GetComponent<PlayerController>().inventorySlots);
    }
    public void Additem(string id){
        Item itemToAdd = ItemDatabase.Find(item => item.ItemID == id);
        ItemSlot slot = HasEmptySlot();
        if(slot != null){            
            slot.SetItem(itemToAdd);
        }
        updateInvWeight();
        
    }
    public ItemSlot Hasitem(string id){
        foreach(Transform child in Inventory.transform){ 
            ItemSlot slot = child.gameObject.GetComponent<ItemSlot>();           
            if(slot.ItemData != null){                
                if(slot.ItemData.ItemID == id){
                    return child.gameObject.GetComponent<ItemSlot>();
                }
                
            }
        }
        return null;
    }    

    public void RemoveItem(string id){
        ItemSlot slot = Hasitem(id);
        slot.ClearSlot(); 
        updateInvWeight();       
    }

    public ItemSlot HasEmptySlot(){
        foreach(Transform child in Inventory.transform){ 
            ItemSlot chldScr = child.gameObject.GetComponent<ItemSlot>();         
            if(chldScr.isSlotEmpty() == true){                
                return chldScr;
            }
        }
        return null;
    }

    public void updateInvWeight(){ 
        int TempWeight = 0;       
        foreach(Transform child in Inventory.transform){
            if(child.GetComponent<ItemSlot>().ItemData != null){
                TempWeight += child.GetComponent<ItemSlot>().ItemData.Weight;
            }
        }
        Player.GetComponent<PlayerController>().SetCurrentWeight(TempWeight);
        WeightTXT.text = ((int)TempWeight).ToString() + "/" + (Player.GetComponent<PlayerController>().WeightCapBase + Player.GetComponent<PlayerController>().WeightCapMod).ToString();

    }
    public void SetInvSlots(int Count){
        while(Inventory.transform.childCount < Count){
            GameObject newObject = Instantiate(InventorySlotPrefab);
                newObject.transform.SetParent(Inventory.transform);
                newObject.transform.localScale = new Vector3(1,1,1);
                newObject.SetActive(true); 
        }

    }
}

