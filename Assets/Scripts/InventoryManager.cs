using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.Animations;
using UnityEngine;

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
    public int TotalWeight;
    public GameObject Player;
    public GameObject Inventory;

    public void Additem(string id){
        Item itemToAdd = ItemDatabase.Find(item => item.ItemID == id);
        ItemSlot slot = HasEmptySlot();
        if(slot != null){            
            slot.SetItem(itemToAdd);
        }
        
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
}

