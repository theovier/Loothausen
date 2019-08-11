using System.Collections;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour {

    public TextMeshProUGUI textField;
    public AdjustChatboxBG adjuster;
    public UIFader fader;

    private bool hidden;

    private void Start() {
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
        StartCoroutine(DeactiveAfterFade(0.1f));
        textField.text = "";
        hidden = true;
    }
    
    private IEnumerator DeactiveAfterFade(float fadeDuration) {
        fader.FadeOut(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        print("deactivated");
        gameObject.SetActive(false);
    }


    public bool IsVisible() {
        return !hidden;
    }
    
}
