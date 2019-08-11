using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour {
    
    public TextMeshProUGUI textField;
    public AdjustChatboxBG adjuster;
    public UIFader fader;
    
    private bool hidden;
    
    private void Start() {
        fader.FadeOut(0);
    }

    public void Show(string text) {
        fader.FadeIn(0.1f);
        textField.text = text;
        hidden = false;
        adjuster.AdjustChatbox(textField.preferredHeight);
    }

    public void Hide() {
        fader.FadeOut(0.1f);
        textField.text = "";
        hidden = true;
    }

    public bool IsVisible() {
        return !hidden;
    }
    
}
