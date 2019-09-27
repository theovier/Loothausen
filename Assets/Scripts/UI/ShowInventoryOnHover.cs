﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInventoryOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public RectTransform inventoryBar;
    public float fadeAnimDuration = 0.25f;
    public float slideAnimationDuration = 0.5f;
    public float targetPositionY = -125;

    private UIFader fader;
    private bool visibilityLock;

    private bool cursorInside;

    private void Awake() {
        fader = gameObject.AddComponent<UIFader>();
        fader.uiElement = inventoryBar.GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        cursorInside = true;
        Player.Instance.HasInventoryOpened = true;
        ShowInventoryBar();
    }

    public void OnPointerExit(PointerEventData eventData) {
        cursorInside = false;
        Player.Instance.HasInventoryOpened = false;
        if (visibilityLock) return;
        HideInventoryBar();
    }

    private void ShowInventoryBar() {
        fader.FadeIn(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY, slideAnimationDuration);
    }
    
    private void HideInventoryBar() {
        fader.FadeOut(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY + inventoryBar.sizeDelta.y, slideAnimationDuration);
    }

    public void Lock() {
        visibilityLock = true;
    }

    public void Unlock() {
        visibilityLock = false;
        if (!cursorInside) {
            HideInventoryBar();
        }
    }
}