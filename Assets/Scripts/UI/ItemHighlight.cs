using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHighlight : MonoBehaviour {

    public TextMeshProUGUI itemNameTextfield;
    public Image itemIcon;
    public UIRotation rays;
    public float fadeInDuration = 0.25f;
    public float stayInFocusDuration = 1.5f;
    public float fadeOutDuration = .5f;
    
    private CanvasGroup canvasGroup;
    private UIFader fade;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        fade = gameObject.AddComponent<UIFader>();
        fade.uiElement = canvasGroup;
        Hide();
    }
    
    public void Show(Item item) {
        itemIcon.sprite = item.icon;
        itemNameTextfield.text = item.itemName;
        FadeIn();
        ScaleUpAnimation();
        PlayRayRotation();
        StartCoroutine(WaitThenFadeOut());
    }

    private IEnumerator WaitThenFadeOut() {
        yield return new WaitForSeconds(stayInFocusDuration);
        FadeOut();
    }

    private void FadeIn() {
        fade.FadeIn(fadeInDuration);
    }

    private void FadeOut() {
        fade.FadeOut(fadeOutDuration);
    }

    private void ScaleUpAnimation() {
        transform.DOScale(Vector3.one, fadeInDuration);
    }

    private void PlayRayRotation() {
        rays.Play();
    }

    private void StopRayRotation() {
        rays.Stop();
    }
    
    private void Hide() {
        canvasGroup.alpha = 0;
    }

    public void Reset() {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        Hide();
        StopRayRotation();
    }
}
