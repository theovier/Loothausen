using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Loot : MonoBehaviour, IPointerClickHandler {

    public List<Item> loot;
    private bool looted;
    
    public void OnPointerClick(PointerEventData eventData) {
        if (!looted) {
            foreach (var item in loot) {
                Player.Instance.inventory.AddItem(item);
            }
            looted = true;
        }
    }
}
