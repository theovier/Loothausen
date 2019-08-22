using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayName : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public NameDisplayController controller;
    public string text;
    
    public void OnPointerEnter(PointerEventData eventData) {
        controller.DisplayText(text);
    }

    public void OnPointerExit(PointerEventData eventData) {
        controller.Hide();
    }
}
