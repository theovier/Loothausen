using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour, IItemContainer {

    public List<ItemSlot> ItemSlots;

    protected virtual void OnValidate() {
        GetComponentsInChildren(true, ItemSlots);
    }
    
    public virtual bool CanAddItem(Item item) {
        return true;
    }
    
    public virtual bool AddItem(Item item) {
        //ItemSlots.Add(item);
        return true;
    }

    public virtual bool RemoveItem(Item item) {
        //return ItemSlots.Remove(item);
        return true;
    }

    public virtual void Clear() {
        //ItemSlots = new List<Item>();
    }
}
