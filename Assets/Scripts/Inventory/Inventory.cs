using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//we can have infinite items but only display a finite number of item slots (9) at once
public class Inventory : MonoBehaviour, IItemContainer {

    public Item item1;
    public Item item2;
    
    private List<IWrapper<Item>> itemSlots = new List<IWrapper<Item>>();
    private ShiftableCutoutList<Item> cutoutList;
    private Button nextButton;
    private Button previousButton;

    private void OnValidate() {
        GetComponentsInChildren(true, itemSlots);
        InitButtons();
        InitShiftableCutoutList();
        AddActionListenersToButtons();
    }
    
    private void InitButtons() {
        var buttons = GetComponentsInChildren<Button>(true);
        previousButton = buttons[0];
        nextButton = buttons[1];
    }

    private void InitShiftableCutoutList() {
        cutoutList = new ShiftableCutoutList<Item>(itemSlots, nextButton.gameObject, previousButton.gameObject);
        cutoutList.Init();
    }

    private void AddActionListenersToButtons() {
        previousButton.onClick.AddListener(cutoutList.OnPrevious);
        nextButton.onClick.AddListener(cutoutList.OnNext);
    }
    
    public void AddItem(Item item) {
        cutoutList.Add(item);
    }

    public bool RemoveItem(Item item) {
        return cutoutList.Remove(item);
    }

    public void Clear() {
        cutoutList.Clear();
    }

    //Just for Debugging
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
