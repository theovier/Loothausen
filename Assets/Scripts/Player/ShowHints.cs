using UnityEngine;

public class ShowHints : MonoBehaviour {

    private GameObject[] hints;
    
    private void Start() { 
        hints = GameObject.FindGameObjectsWithTag("Hint");
        HideHints();
    }

    private void Update() {
        if (Input.GetButtonDown("ShowHints")) {
            HighlightHints();    
        }
        else if (Input.GetButtonUp("ShowHints")) {
            HideHints();
        }
    }
    
    private void HighlightHints() {
        foreach (var hint in hints) {
            hint.SetActive(true);
        }
    }
    
    private void HideHints() {
        foreach (var hint in hints) {
            hint.SetActive(false);
        }
    }
}
