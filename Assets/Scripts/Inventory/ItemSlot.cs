using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    public Image iconImage;
    public GameObject indicator;
    public Vector2 scaleOnHover = new Vector2(1.2f, 1.2f);
    public float scaleDurationOnHover = 0.25f;

    public Item Item {
        get { return item; }
        set {SetItem(value); Show();}
    }
    
    private Item item;
    private RectTransform rectTransform;
    private NameDisplayController tooltip;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        tooltip = GameObject.FindGameObjectWithTag("TooltipBox").GetComponent<NameDisplayController>();
        Hide();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.DisplayText(item.itemName);
        rectTransform.DOScale(new Vector3(scaleOnHover.x, scaleOnHover.y, 0), scaleDurationOnHover).SetAutoKill(false);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Hide();
        rectTransform.DOPlayBackwards();
    }
    
    public bool IsEmpty() {
        return !item;
    }

    public void SetItem(Item item) {
        this.item = item;
        iconImage.sprite = item.icon;
    }

    public bool HasItem(Item item) {
        return this.item.Equals(item);
    }
    
    public void Clear() {
        item = null;
        Hide();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }
    
    
}
