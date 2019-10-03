using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInventoryOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private ToggleInventory toggleInventory;
    private bool visibilityLock;
    private bool cursorInside;

    private void Awake() {
        toggleInventory = GetComponent<ToggleInventory>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        cursorInside = true;
        Player.Instance.HasInventoryOpened = true;
        toggleInventory.ShowInventoryBar();
    }

    public void OnPointerExit(PointerEventData eventData) {
        cursorInside = false;
        Player.Instance.HasInventoryOpened = false;
        if (visibilityLock) return;
        toggleInventory.HideInventoryBar();
    }
    
    public void Lock() {
        visibilityLock = true;
    }

    public void Unlock() {
        visibilityLock = false;
        if (!cursorInside) {
            toggleInventory.HideInventoryBar();
        }
    }
}
