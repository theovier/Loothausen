using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//we can have infinite items but only display a finite number of item slots (9) at once
public class Inventory : MonoBehaviour, IItemContainer {

    public Item item1;
    public Item item2;
    
    private List<Item> items = new List<Item>();
    private List<ItemSlot> itemSlots = new List<ItemSlot>();
    private Button nextButton;
    private Button previousButton;
    private int timesNextPressed;

    private void OnValidate() {
        GetComponentsInChildren(true, itemSlots);
        InitButtons();
        AddActionListenersToButtons();
    }

    private void InitButtons() {
        var buttons = GetComponentsInChildren<Button>(true);
        previousButton = buttons[0];
        nextButton = buttons[1];
    }

    private void AddActionListenersToButtons() {
        previousButton.onClick.AddListener(OnPreviousItem);
        nextButton.onClick.AddListener(OnNextItem);
    }

    private void Start() {
        HideNextButton();
        HidePreviousButton();
    }
    
    public void AddItem(Item item) {
        items.Add(item);

        foreach (var slot in itemSlots) {
            if (slot.IsEmpty()) {
                slot.Item = item;
                break;
            }
        }

        if (items.Count > itemSlots.Count) {
            ShowNextButton();
        }
    }

    public bool RemoveItem(Item item) {
        bool hadItem = items.Remove(item);
        if (hadItem && IsInAnySlot(item)) {
            var emptySlotIndex = RemoveItemFromAnySlot(item);
            ShiftRemainingSlotItems(emptySlotIndex);
        }
        
        if (items.Count - timesNextPressed <= itemSlots.Count) {
            HideNextButton();
        }
        return hadItem;
    }
    
    private bool IsInAnySlot(Item item) {
        return itemSlots.Any(x => x.HasItem(item));
    }

    private int RemoveItemFromAnySlot(Item item) {
        var emptySlotIndex = GetSlotIndexOfItem(item);
        itemSlots[emptySlotIndex].Clear();
        return emptySlotIndex;
    }
    
    private int GetSlotIndexOfItem(Item item) {
        for (var i = 0; i < itemSlots.Count; i++) {
            var slot = itemSlots[i];
            if (slot.HasItem(item)) {
                return i;
            }
        }
        return -1;
    }

    private void ShiftRemainingSlotItems(int emptySlotIndex) {
        for (var i = emptySlotIndex; i < itemSlots.Count - 1; i++) {
            var nextSlot = itemSlots[i + 1];
            if (nextSlot.IsEmpty()) {
                //if all following slots are empty, there is no need to shift.
                return;
            }
            var item = nextSlot.Item;
            nextSlot.Clear();
            itemSlots[i].Item = item;
        }
        
        if (LastSlot().IsEmpty()) {
            if (items.Count >= itemSlots.Count) {
                var nextItemIndex = itemSlots.Count - 1 + timesNextPressed;
                if (nextItemIndex < items.Count) {
                    var nextHiddenItem = items[nextItemIndex];
                    LastSlot().Item = nextHiddenItem;
                }
            }
            if (items.Count <= timesNextPressed + itemSlots.Count) {
                //this state cannot be reached normally without deletion. The last slot is always in use if items.count >= itemSlots.count
                OnPreviousItem();
            }
        }
    }
    
    public void Clear() {
        foreach (var slot in itemSlots) {
            slot.Clear();
        }
        items.Clear();
    }

    private void OnNextItem() {
        //todo make a generic shiftable list (ChoiceBox uses basically same methods)
        ShowPreviousButton();
        timesNextPressed++;
        
        for (var i = 0; i < itemSlots.Count - 1; i++) {
            var slot = itemSlots[i + 1];
            var item = slot.Item;
            itemSlots[i].Item = item;
        }
        
        var nextHiddenItem = items[itemSlots.Count - 1 + timesNextPressed];
        LastSlot().Item = nextHiddenItem;

        if (LastItemDisplaying()) {
            HideNextButton();
        }
    }

    private bool LastItemDisplaying() {
        var lastItem = items.Last();
        return LastSlot().Item.Equals(lastItem);
    }

    private void OnPreviousItem() {
        ShowNextButton();
        timesNextPressed--;
        
        for (var i = itemSlots.Count - 1; i > 0; i--) {
            var slot = itemSlots[i - 1];
            var item = slot.Item;
            itemSlots[i].Item = item;
        }
        
        var previousHiddenItem = items[timesNextPressed];
        FirstSlot().Item = previousHiddenItem;
        
        if (FirstItemDisplaying()) {
            HidePreviousButton();
        }
    }

    private bool FirstItemDisplaying() {
        if (items.Any()) {
            var firstItem = items[0];
            return FirstSlot().Item.Equals(firstItem);
        }
        return true;
    }

    private ItemSlot FirstSlot() {
        return itemSlots[0];
    }

    private ItemSlot LastSlot() {
        return itemSlots[itemSlots.Count - 1];
    }
    
    private void HideNextButton() {
        nextButton.gameObject.SetActive(false);
    }

    private void HidePreviousButton() {
        previousButton.gameObject.SetActive(false);
    }

    private void ShowNextButton() {
        nextButton.gameObject.SetActive(true);
    }

    private void ShowPreviousButton() {
        previousButton.gameObject.SetActive(true);
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AddItem(Instantiate(item1));
        }
        
        if (Input.GetKeyDown(KeyCode.A)) {
            AddItem(item2);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            RemoveItem(item2);
        }
    }
}
