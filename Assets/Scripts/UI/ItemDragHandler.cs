using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler {

    public RectTransform icon;
    
    public void OnDrag(PointerEventData eventData) {
        icon.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        icon.localPosition = Vector3.zero;
    }
}
