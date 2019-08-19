﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    public RectTransform inventoryBar;
    public float fadeAnimDuration = 0.25f;
    public float slideAnimationDuration = 0.5f;
    public float targetPositionY = -125;
    
    private UIFader fader;

    private void Awake() {
        fader = gameObject.AddComponent<UIFader>();
        fader.uiElement = inventoryBar.GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        fader.FadeIn(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY, slideAnimationDuration);
    }

    public void OnPointerExit(PointerEventData eventData) {
        fader.FadeOut(fadeAnimDuration);
        inventoryBar.DOAnchorPosY(targetPositionY + inventoryBar.sizeDelta.y, slideAnimationDuration);
    }
}