using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite icon;
    public ItemType type;

    public bool Unseen {
        get => unseen;
        set => unseen = value;
    }

    //used to determine if the player has "seen" (e.g. hovered over) the item in the inventory
    private bool unseen = true;
}
