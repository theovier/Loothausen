using DG.Tweening;
using UnityEngine;

public class ToggleInventoryUI : MonoBehaviour {

    public RectTransform inventoryBar;
    public float fadeAnimDuration = 0.25f;
    public float slideAnimationDuration = 0.5f;
    public float targetPositionY = -125;

    private UIFader fader;
    private bool isVisible;

    private void Awake() {
        fader = gameObject.AddComponent<UIFader>();
        fader.uiElement = inventoryBar.GetComponent<CanvasGroup>();
    }

    public void ShowInventoryBar() {
        fader.FadeIn(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY, slideAnimationDuration);
        isVisible = true;
    }
    
    public void HideInventoryBar() {
        fader.FadeOut(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY + inventoryBar.sizeDelta.y, slideAnimationDuration);
        isVisible = false;
    }

    public void ToggleInventoryBar() {
        if (isVisible) {
            HideInventoryBar();
        }
        else {
            ShowInventoryBar();
        }
    }
}