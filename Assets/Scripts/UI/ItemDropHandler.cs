using UnityEngine;
using UnityEngine.EventSystems;

//credit https://stackoverflow.com/questions/37473802/unity3d-ui-calculation-for-position-dragging-an-item/37473953#37473953
public class ItemDropHandler : MonoBehaviour, IDropHandler {
    
    public void OnDrop(PointerEventData eventData) {
        GameObject from = eventData.pointerDrag;
        if (eventData.pointerDrag == null) return; // (will never happen)
        
        ItemDragHandler d = from.GetComponent<ItemDragHandler>();
        if (d == null)  {
            // means something unrelated to our system was dragged from.
            // for example, just an unrelated scrolling area, etc.
            // simply completely ignore these.
            return;
        }

        //Hide the ghost from the drag handler, so we don't see the animation of it returning to the inventory
        d.HideGhost();
        
        ItemSlot fromItemSlot = from.GetComponent<ItemSlot>();
        Item item = fromItemSlot.GetContent();
        Debug.Log ("dropped  " + item.name +" onto " +gameObject.name);
    }
}
