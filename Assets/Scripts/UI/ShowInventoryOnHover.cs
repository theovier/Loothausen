using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInventoryOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private ToggleInventoryUI toggleInventoryUi;
    private bool visibilityLock;
    private bool cursorInside;

    private void Awake() {
        toggleInventoryUi = GetComponent<ToggleInventoryUI>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        cursorInside = true;
        Player.Instance.HasInventoryOpened = true;
        toggleInventoryUi.ShowInventoryBar();
    }

    public void OnPointerExit(PointerEventData eventData) {
        cursorInside = false;
        Player.Instance.HasInventoryOpened = false;
        if (visibilityLock) return;
        toggleInventoryUi.HideInventoryBar();
    }
    
    public void Lock() {
        visibilityLock = true;
    }

    public void Unlock() {
        visibilityLock = false;
        if (!cursorInside) {
            toggleInventoryUi.HideInventoryBar();
        }
    }
}
