using System.Collections.Generic;
using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour {

    public Transform groundPosition;
    private Dictionary<SpriteRenderer, int> offsetLookup = new Dictionary<SpriteRenderer, int>();

    private void Awake() {
        foreach (var o in GetComponentsInChildren<SpriteRenderer>()) {
            offsetLookup.Add(o, o.sortingOrder);
        }
    }
    
    private void LateUpdate() {
        foreach (var o in offsetLookup) {
            o.Key.sortingOrder = (int) (groundPosition.position.y * -100f) + o.Value;
        }
    }
}
