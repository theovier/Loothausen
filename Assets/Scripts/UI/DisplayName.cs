using UnityEngine;

public class DisplayName : MonoBehaviour {

    public NameDisplayController controller;
    public string text;
    
    private void OnMouseEnter() {
        controller.DisplayText(text);
    }

    private void OnMouseExit() {
        controller.Hide();
    }
    
}
