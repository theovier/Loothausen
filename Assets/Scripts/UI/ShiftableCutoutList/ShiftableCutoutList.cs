using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShiftableCutoutList<T> : IShiftableCutoutList<T> {

    private readonly List<T> items;
    private readonly List<IWrapper<T>> wrappers;
    private readonly GameObject controlNext;
    private readonly GameObject controlPrevious;
    private int timesNextPressed;

    public ShiftableCutoutList(List<IWrapper<T>> wrappers, GameObject controlNext, GameObject controlPrevious) {
        this.wrappers = wrappers;
        this.items = new List<T>();
        this.controlNext = controlNext;
        this.controlPrevious = controlPrevious;
    }

    public void Init() {
        DisableControls();
    }

    private void DisableControls() {
        DisableControlNext();
        DisableControlPrevious();
    }

    public void Add(T item) {
        items.Add(item);
        TryWrapping(item);
        HandleControlEnabling();
    }

    private void TryWrapping(T item) {
        foreach (var wrapper in wrappers) {
            if (wrapper.IsEmpty()) {
                wrapper.SetContent(item);
                break;
            }
        }
    }

    private void HandleControlEnabling() {
        if (items.Count > wrappers.Count) {
            EnableControlNext();
        }
        if (items.Count - timesNextPressed <= wrappers.Count) {
            DisableControlNext();
        }
        if (IsFirstItemWrapped()) {
            DisableControlPrevious();
        }
        if (IsLastItemWrapped()) {
            DisableControlNext();
        }
    }

    public bool Remove(T item) {
        bool hadItem = items.Remove(item);
        if (hadItem && CurrentlyWrapped(item)) {
            var clearedWrapperIndex = ClearCorrespondingWrapper(item);
            FillEmptyWrapperWithSuccessor(clearedWrapperIndex);
        }
        HandleControlEnabling();
        return hadItem;
    }

    public bool CurrentlyWrapped(T item) {
        return wrappers.Any(wrapper => wrapper.HasContent(item));
    }
    
    private int ClearCorrespondingWrapper(T item) {
        var wrapperToClearIndex = GetWrapperIndex(item);
        wrappers[wrapperToClearIndex].Clear();
        return wrapperToClearIndex;
    }

    public int GetWrapperIndex(T item) {
        for (var i = 0; i < wrappers.Count; i++) {
            var wrapper = wrappers[i];
            if (wrapper.HasContent(item)) {
                return i;
            }
        }
        return -1;
    }

    private void FillEmptyWrapperWithSuccessor(int emptyWrapperIndex) {
        ShiftAllSuccessorsByOne(emptyWrapperIndex);
        FillLastWrapperAgain();
    }

    //the last wrapper will be empty after this operation
    private void ShiftAllSuccessorsByOne(int emptyWrapperIndex) {
        for (var i = emptyWrapperIndex; i < LastWrapperIndex(); i++) {
            var currentWrapper = wrappers[i];
            var nextWrapper = wrappers[i + 1];
            if (nextWrapper.IsEmpty()) {
                return; //if all following wrappers are empty, there is no need to shift their contents.
            }
            currentWrapper.SetContent(nextWrapper.GetContent());
            nextWrapper.Clear();
        }
    }

    private void FillLastWrapperAgain() {
        if (HasAtLeastAsManyItemsAsWrappers()) {
            FillLastWrapperWithNextItem();
        }
        else if (NoNextItemsLeft() && timesNextPressed > 0) {
            OnPrevious();
        }
    }
    private bool HasAtLeastAsManyItemsAsWrappers() {
        return items.Count >= wrappers.Count;
    }
    
    private void FillLastWrapperWithNextItem() {
        var indexOfNextItemToDisplay = LastWrapperIndex() + timesNextPressed;
        if (indexOfNextItemToDisplay < items.Count) {
            var nextItemToDisplay = items[indexOfNextItemToDisplay];
            LastWrapper().SetContent(nextItemToDisplay);
        }
    }
    
    private bool NoNextItemsLeft() {
        return items.Count < timesNextPressed + wrappers.Count;
    }

    private int LastWrapperIndex() {
        return wrappers.Count - 1;
    }
    
    public void Clear() {
        wrappers.ForEach(wrapper => wrapper.Clear());
        items.Clear();
        timesNextPressed = 0;
        DisableControls();
    }

    public void OnNext() {
        EnableControlPrevious();
        timesNextPressed++;
        ShiftAllItemsForwardByOne();
        FillLastWrapperWithNextItem();
        HandleControlEnabling();
    }

    private void ShiftAllItemsForwardByOne() {
        for (var i = 0; i < wrappers.Count - 1; i++) {
            var currentWrapper = wrappers[i];
            var nextWrapper = wrappers[i + 1];
            currentWrapper.SetContent(nextWrapper.GetContent());
        }
    }
    
    public void OnPrevious() {
        EnableControlPrevious();
        timesNextPressed--;
        ShiftAllItemsBackwardsByOne();
        FillFirstWrapperWithPreviousItem();
        HandleControlEnabling();
    }

    private void ShiftAllItemsBackwardsByOne() {
        for (var i = wrappers.Count - 1; i > 0; i--) {
            var wrapper = wrappers[i - 1];
            wrappers[i].SetContent(wrapper.GetContent());
        }
    }

    private void FillFirstWrapperWithPreviousItem() {
        var nextItemToDisplay = items[timesNextPressed];
        FirstWrapper().SetContent(nextItemToDisplay);
    }

    private bool IsFirstItemWrapped() {
        return CurrentlyWrapped(items[0]);
    }

    private bool IsLastItemWrapped() {
        return CurrentlyWrapped(items.Last());
    }

    private IWrapper<T> LastWrapper() {
        return wrappers.Last();
    }

    private IWrapper<T> FirstWrapper() {
        return wrappers[0];
    }

    private void EnableControlNext() {
        controlNext.SetActive(true);
    }

    private void EnableControlPrevious() {
        controlPrevious.SetActive(true);
    }

    private void DisableControlNext() {
        controlNext.SetActive(false);
    }

    private void DisableControlPrevious() {
        controlPrevious.SetActive(false);
    }
}
