using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//we can have infinite items but only display a finite number of item slots (9) at once
public class Inventory : MonoBehaviour, IItemContainer {

    public Item testItem;

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
        foreach (var slot in itemSlots) {
            if (slot.HasItem(item)) {
                slot.Clear();
                break;
            }
        }

        //fill all slots again if we have more items than slots, disable buttons if needed etc.
        return items.Remove(item);
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
            itemSlots[i].SetItem(item);
        }
        
        var nextHiddenItem = items[itemSlots.Count - 1 + timesNextPressed];
        LastSlot().SetItem(nextHiddenItem);

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
            itemSlots[i].SetItem(item);
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
            AddItem(Instantiate(testItem));
        }
    }
}
