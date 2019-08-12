using System.Collections;
using System.Runtime.Remoting.Messaging;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour {

    public TextMeshProUGUI textField;
    public AdjustChatboxBG adjuster;
    public UIFader fader;

    private bool hidden;

    private void Start() {
        //Only called on active component. So make sure the component is active at the beginning of the scene.
        gameObject.SetActive(false);
    }

    public void Show(string text) {
        gameObject.SetActive(true);
        textField.text = text;
        adjuster.AdjustChatbox(textField.preferredHeight);
        fader.FadeIn(0.1f);
        hidden = false;
    }

    public void Hide() {
        if (!IsVisible()) return;
        hidden = true;
        fader.FadeOut(0.1f, OnFadeOutCompleted);
        textField.text = "";
    }

    private void OnFadeOutCompleted() {
        gameObject.SetActive(false);
    }
    
    public bool IsVisible() {
        return !hidden && gameObject.activeSelf;
    }
    
}
