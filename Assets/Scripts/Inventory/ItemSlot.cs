using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : CutoutListWrapper<Item>, IPointerEnterHandler, IPointerExitHandler {
    
    private Image iconImage;
    private NameDisplayController tooltip;

    private void Awake() {
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
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Hide();
    }
}