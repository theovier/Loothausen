using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHighlight : MonoBehaviour {
    
    public TextMeshProUGUI itemNameTextfield;
    public Transform itemInfoTransform;
    public Image itemIcon;
    public float fadeInDuration = 1f;
    public float stayInFocusDuration = 2.5f;
    public float fadeOutDuration = 1f;
    
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
        PlayScaleUpAnimation();
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
        fade.FadeOut(fadeOutDuration, Hide);
    }

    private void PlayScaleUpAnimation() {
        itemInfoTransform.DOScale(Vector3.one, fadeInDuration);
    }
    
    private void Hide() {
        canvasGroup.alpha = 0;
        itemInfoTransform.localScale = new Vector2(0.25f, 0.25f);
    }

    public void Reset() {
        Hide();
    }
}
