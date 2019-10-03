using DG.Tweening;
using UnityEngine;

public class ToggleInventory : MonoBehaviour {

    public RectTransform inventoryBar;
    public float fadeAnimDuration = 0.25f;
    public float slideAnimationDuration = 0.5f;
    public float targetPositionY = -125;

    private UIFader fader;

    private void Awake() {
        fader = gameObject.AddComponent<UIFader>();
        fader.uiElement = inventoryBar.GetComponent<CanvasGroup>();
    }

    public void ShowInventoryBar() {
        fader.FadeIn(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY, slideAnimationDuration);
    }
    
    public void HideInventoryBar() {
        fader.FadeOut(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY + inventoryBar.sizeDelta.y, slideAnimationDuration);
    }
}