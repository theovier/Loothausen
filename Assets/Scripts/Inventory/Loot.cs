using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Loot : MonoBehaviour, IPointerClickHandler {

    public List<Item> loot;
    private bool looted;
    
    public void OnPointerClick(PointerEventData eventData) {
        if (!looted) {
            //todo: better to access player and from there the inventory, so we can trigger some gathering animation as well.
            var inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
            loot.ForEach(inventory.AddItem);
            looted = true;
        }
    }
}
