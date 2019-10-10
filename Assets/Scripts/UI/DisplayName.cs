using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayName : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    public string text;
    
    private NameDisplayController controller;
    
    private void Start() {
        controller = FindObjectOfType<NameDisplayController>();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        controller.DisplayText(text);
    }

    public void OnPointerExit(PointerEventData eventData) {
        controller.Hide();
    }
}
