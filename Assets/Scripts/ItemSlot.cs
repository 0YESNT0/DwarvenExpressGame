using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    //[HideInInspector]
    public Item ItemData = null;
    public UnityEngine.UI.Image ItemIcon;
    void Start(){
        ClearSlot();
    }
    public void SetItem(Item item){        
        ItemData = item;
        ItemIcon.enabled = true;
        ItemIcon.sprite = ItemData.ItemIcon; 
        
    }
    public void ClearSlot(){
        ItemData = null;
        ItemIcon.enabled = false;
    }
    public bool isSlotEmpty(){
        if(ItemData == null){
            return true;
        }
        else{
            return false;
        }
    }
}
