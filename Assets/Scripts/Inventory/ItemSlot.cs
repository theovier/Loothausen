using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IWrapper<Item>, IPointerEnterHandler, IPointerExitHandler {
    
    public Vector2 scaleOnHover = new Vector2(1.2f, 1.2f);
    public float scaleDurationOnHover = 0.25f;
    
    private Item item;
    private Image iconImage;
    private RectTransform rectTransform;
    private NameDisplayController tooltip;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        tooltip = GameObject.FindGameObjectWithTag("TooltipBox").GetComponent<NameDisplayController>();
        iconImage = GetComponentsInChildren<Image>()[1];
        gameObject.SetActive(false);
    }
    
    public Item GetContent() {
        return item;
    }

    public void SetContent(Item content) {
        item = content;
        Refresh();
    }

    public bool HasContent(Item contentCandidate) {
        return contentCandidate.Equals(item);
    }
    
    public bool IsEmpty() {
        return !item;
    }

    public void Clear() {
        item = null;
        Disable();
    }

    public void Enable() {
        gameObject.SetActive(true);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }
    
    private void Refresh() {
        iconImage.sprite = item.icon;
        Enable();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.DisplayText(item.itemName);
        rectTransform.DOScale(new Vector3(scaleOnHover.x, scaleOnHover.y, 0), scaleDurationOnHover).SetAutoKill(false);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Hide();
        rectTransform.DOPlayBackwards();
    }
}