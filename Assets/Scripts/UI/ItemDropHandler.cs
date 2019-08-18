using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler {
    
    public void OnDrop(PointerEventData eventData) {
        RectTransform inventoryPanel = transform as RectTransform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(inventoryPanel, Input.mousePosition)) {
            Debug.Log("item used on something");
        }
    }
}
