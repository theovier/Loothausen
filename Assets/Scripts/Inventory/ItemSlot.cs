using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : CutoutListWrapper<Item>, IPointerEnterHandler, IPointerExitHandler {
    
    private Image iconImage;
    private Image seenIndicatorImage;
    private NameDisplayController tooltip;

    private void Awake() {
        tooltip = GameObject.FindGameObjectWithTag("TooltipBox").GetComponent<NameDisplayController>();
        iconImage = GetComponentsInChildren<Image>()[1];
        seenIndicatorImage = GetComponentsInChildren<Image>()[2];
        gameObject.SetActive(false);
    }
    
    protected override void Refresh() {
        iconImage.sprite = content.icon;
        seenIndicatorImage.enabled = content.Unseen;
        Enable();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.DisplayText(content.itemName);
        content.Unseen = false;
        seenIndicatorImage.enabled = false;
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Hide();
    }
}