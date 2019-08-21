using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : CutoutListWrapper<Item>, IPointerEnterHandler, IPointerExitHandler {
    
    public Vector2 scaleOnHover = new Vector2(1.2f, 1.2f);
    public float scaleDurationOnHover = 0.25f;
    
    private Image iconImage;
    private RectTransform rectTransform;
    private NameDisplayController tooltip;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        tooltip = GameObject.FindGameObjectWithTag("TooltipBox").GetComponent<NameDisplayController>();
        iconImage = GetComponentsInChildren<Image>()[1];
        gameObject.SetActive(false);
    }
    
    protected override void Refresh() {
        iconImage.sprite = content.icon;
        Enable();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.DisplayText(content.itemName);
        rectTransform.DOScale(new Vector3(scaleOnHover.x, scaleOnHover.y, 0), scaleDurationOnHover).SetAutoKill(false);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Hide();
        rectTransform.DOPlayBackwards();
    }
}