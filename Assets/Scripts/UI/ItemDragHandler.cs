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

    private ShowInventoryOnHover inventoryShowInventoryOnHover;
    private ScaleOnHover itemSlotScaler;
    
    private void Awake() {
        itemSlot = GetComponent<ItemSlot>();
        inventoryShowInventoryOnHover = GetComponentInParent<ShowInventoryOnHover>();
        itemSlotScaler = itemSlot.GetComponent<ScaleOnHover>();
        cursor = FindObjectOfType<MouseCursor>();
        ghost.raycastTarget = false;
        ghost.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        ghost.sprite = itemSlot.GetContent().icon;
        ghost.transform.position = transform.position;
        ghost.rectTransform.localScale = Vector3.one;
        itemSlotPosition = itemSlot.transform.position;
        ghost.enabled = true;
        cursor.ChangeStyle(CursorStyle.Grab);
        cursor.lockStyle = true;
        inventoryShowInventoryOnHover.Lock();
    }

    public void OnDrag(PointerEventData eventData) {
        ghost.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        cursor.lockStyle = false;
        ReturnToOrigin();
        ReturnToOriginalSize();
    }

    private void ReturnToOrigin() { 
        var ghostRect = ghost.rectTransform;
        ghostRect.DOMove(itemSlotPosition, returnAnimDuration, true).OnComplete(HideGhost);
    }

    private void ReturnToOriginalSize() {
        /* the item dragged is always increased in size because of the itemSlotScaler
         if we snap the item back into its original position we have to make sure that it is not scaled as if
         we would hover it. so we have to scale it down by the amount the itemSlotScaler did scale it up
         */
        var ghostRect = ghost.rectTransform;
        var ghostScale = ghostRect.localScale;
        var increasedSize = itemSlotScaler.scaleOnHover;
        var originalSize = new Vector2(ghostScale.x / increasedSize.x, ghostScale.y / increasedSize.y);
        ghostRect.DOScale(originalSize, returnAnimDuration);
    }

    public void HideGhost() {
        ghost.enabled = false;
        inventoryShowInventoryOnHover.Unlock();
    }
}
