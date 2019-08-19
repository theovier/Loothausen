using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    public NameDisplayController tooltip;
    public Vector2 scaleOnHover = new Vector2(1.2f, 1.2f);
    public float scaleDurationOnHover = 0.25f;
    public Item item;
    public Image iconImage;
    public GameObject indicator;
    
    private bool isPointerOver;
    private RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        iconImage.sprite = item.icon;
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        isPointerOver = true;
        tooltip.DisplayText(item.itemName);
        rectTransform.DOScale(new Vector3(scaleOnHover.x, scaleOnHover.y, 0), scaleDurationOnHover).SetAutoKill(false);
    }

    public void OnPointerExit(PointerEventData eventData) {
        isPointerOver = false;
        tooltip.Hide();
        rectTransform.DOPlayBackwards();
    }
}
