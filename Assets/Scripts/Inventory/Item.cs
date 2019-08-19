using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Item/Item")]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite icon;
    public ItemType type;
}
