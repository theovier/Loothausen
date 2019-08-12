using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameDisplayController : MonoBehaviour {

    public TextMeshProUGUI textField;
    public UIFader fader;
    public float fadeInDuration = 0.1f;
    public float fadeOutDuration = 0.1f;
    
    private bool hidden;
    
    private void Start() {
        gameObject.SetActive(false);
    }
    
    public void DisplayText(string text) {
        gameObject.SetActive(true);
        textField.text = text;
        fader.FadeIn(fadeInDuration);
        hidden = false;
    }

    public void Hide() {
        if (!IsVisible()) return;
        hidden = true;
        StartCoroutine(DeactiveAfterFade(fadeOutDuration));
    }
    
    private IEnumerator DeactiveAfterFade(float fadeDuration) {
        fader.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        gameObject.SetActive(false);
    }
    
    public bool IsVisible() {
        return !hidden && gameObject.activeSelf;
    }
}
