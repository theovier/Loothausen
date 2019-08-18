using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public NameDisplayController tooltip;
    public string itemName;
    public Vector2 scaleOnHover = new Vector2(1.2f, 1.2f);
    public float scaleDurationOnHover = 0.25f;
    
    private RectTransform self;

    private void Start() {
        self = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        ScaleUp();
        tooltip.DisplayText(itemName);
    }

    public void OnPointerExit(PointerEventData eventData) {
        ScaleDown();
        tooltip.Hide();
    }

    private void ScaleUp() {
        self.DOScale(new Vector3(scaleOnHover.x, scaleOnHover.y, 0), scaleDurationOnHover);
    }

    private void ScaleDown() {
        self.DOScale(new Vector3(1, 1, 0), scaleDurationOnHover);
    }
}
