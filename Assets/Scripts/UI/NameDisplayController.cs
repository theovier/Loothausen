using TMPro;
using UnityEngine;

public class NameDisplayController : MonoBehaviour {
    
    public float fadeInDuration = 0.1f;
    public float fadeOutDuration = 0.1f;
    
    private TextMeshProUGUI textField;
    private UIFader fader;
    private bool hidden;
    
    private void Start() {
        textField = GetComponent<TextMeshProUGUI>();
        fader = gameObject.AddComponent<UIFader>();
        fader.uiElement = GetComponent<CanvasGroup>();
        gameObject.SetActive(false);
    }
    
    public void DisplayText(string text) {
        gameObject.SetActive(true);
        textField.text = text;
        fader.StopCurrentFade();
        fader.FadeIn(fadeInDuration);
        hidden = false;
    }

    public void Hide() {
        if (!IsVisible()) return;
        hidden = true;
        fader.FadeOut(fadeOutDuration, OnFadeOutCompleted);
    }
    
    private void OnFadeOutCompleted() {
        gameObject.SetActive(false);
    }
    
    public bool IsVisible() {
        return !hidden && gameObject.activeSelf;
    }
}
