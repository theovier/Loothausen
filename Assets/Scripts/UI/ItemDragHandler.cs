using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//credit https://stackoverflow.com/questions/37473802/unity3d-ui-calculation-for-position-dragging-an-item/37473953#37473953
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    
    public Image ghost;
    [Tooltip("Animation duration of the item returning to the inventory when the drag did not end up in a drop zone.")]
    public float returnAnimDuration = 0.5f;
    
    private ItemSlot itemSlot;
    private Vector3 itemSlotPosition;
    private RectTransform rect;
    private MouseCursor cursor;

    private ShowOnHover inventoryShowOnHover;
    
    private void Awake() {
        itemSlot = GetComponent<ItemSlot>();
        inventoryShowOnHover = GetComponentInParent<ShowOnHover>();
        itemSlotPosition = itemSlot.transform.position;
        cursor = GameObject.FindObjectOfType<MouseCursor>();
        ghost.raycastTarget = false;
        ghost.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        ghost.sprite = itemSlot.GetContent().icon;
        ghost.transform.position = transform.position;
        ghost.enabled = true;
        cursor.ChangeStyle(CursorStyle.Grab);
        cursor.lockStyle = true;
        inventoryShowOnHover.Lock();
    }

    public void OnDrag(PointerEventData eventData) {
        ghost.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        cursor.lockStyle = false;
        var ghostRect = ghost.GetComponent<RectTransform>();
        ghostRect.DOMove(itemSlotPosition, returnAnimDuration, true).OnComplete(HideGhost);
    }

    public void HideGhost() {
        ghost.enabled = false;
        inventoryShowOnHover.Unlock();
    }
}
