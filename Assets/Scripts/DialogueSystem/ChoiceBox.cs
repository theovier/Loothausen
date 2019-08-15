using System;
using DG.Tweening;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class ChoiceBox : MonoBehaviour {
    
    public float slideAnimationTargetPosX;
    public float slideAnimationDuration = 0.02f;
    public delegate void OnChoiceSelected(int choiceIndex);

    private OnChoiceSelected onSelection;
    private Chat.Choice[] choices;
    private ChoiceButton[] buttons;
    private Button upButton;
    private Button downButton;
    private int pageIndex;

    //tweening
    private RectTransform rectTransform;

    private void Awake() {
        InitAnimationValues();
        InitButtons();
        AddListenersToControlButtons();
    }

    private void InitAnimationValues() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        HideUpButton();
        HideDownButton();
        gameObject.SetActive(false);
    }

    private void InitButtons() {
        var buttonComponents = GetComponentsInChildren<Button>();
        upButton = buttonComponents[0];
        downButton = buttonComponents[buttonComponents.Length - 1];
        
        buttons = new ChoiceButton[buttonComponents.Length - 2]; //-2 because up and down are defined separately
        for (var i = 0; i < buttonComponents.Length - 2; i++) {
            buttons[i] = new ChoiceButton(buttonComponents[i + 1], OnChoiceClicked);
        }
    }

    private void AddListenersToControlButtons() {
        upButton.onClick.AddListener(ScrollOneChoiceUp);
        downButton.onClick.AddListener(ScrollOneChoiceDown);
    }

    private void Show() {
        gameObject.SetActive(true);
        rectTransform.DOAnchorPosX(slideAnimationTargetPosX, slideAnimationDuration);
    }
    
    public void Hide() {
        rectTransform.DOPlayBackwards();
        gameObject.SetActive(false);
    }

    public void Reset() {
        Hide();
        pageIndex = 0;
    }
    
    public void DisplayChoices(Chat.Choice[] newChoices, OnChoiceSelected onSelection) {
        this.onSelection = onSelection;
        choices = newChoices;
        Show();
        UpdateButtons();
    }
    
    private void UpdateButtons() {
        if (choices.Length <= buttons.Length) {
            //all choices fit on the screen at once
            HideUpButton();
            FillButtonsWithChoices(choices.Length);
            DisableUnusedButtons();
            HideDownButton();
        }
        else {
            FillAllButtonsWithChoices();
            ShowDownButton();
        }
    }
    
    private void FillButtonsWithChoices(int buttonsToFill) {
        for (var i = 0; i < buttonsToFill; i++) {
            var button = buttons[i];
            var choice = choices[i];
            button.DisplayChoice(choice, i);
        }
    }

    private void DisableUnusedButtons() {
        for (var i = choices.Length; i < buttons.Length; i++) {
            var button = buttons[i];
            button.Disable();
        }
    }

    private void FillAllButtonsWithChoices() {
        FillButtonsWithChoices(buttons.Length);
    }
    
    private void ScrollOneChoiceUp() {
       ShowDownButton();
       
       for (var i = buttons.Length - 1; i > 0; i--) {
           var button = buttons[i - 1];
           var choice = button.choice;
           var choiceIndex = button.choiceIndex;
           buttons[i].DisplayChoice(choice, choiceIndex);
       }

       var index = --pageIndex;
       var newChoice = choices[index];
       buttons[0].DisplayChoice(newChoice, index);
       
       if (FirstChoiceDisplaying()) {
           HideUpButton();
       }
    }

    private bool FirstChoiceDisplaying() {
        return buttons[0].choiceIndex == 0;
    }
    
    private void ScrollOneChoiceDown() {
        ShowUpButton();
        
        for (var i = 0; i < buttons.Length - 1; i++) {
            var button = buttons[i + 1];
            var choice = button.choice;
            var choiceIndex = button.choiceIndex;
            buttons[i].DisplayChoice(choice, choiceIndex);
        }
        
        var index = ++pageIndex + buttons.Length - 1;
        var newChoice = choices[index];
        buttons[buttons.Length - 1].DisplayChoice(newChoice, index);
        
        if (LastChoiceDisplaying()) {
            HideDownButton();
        }
    }

    private bool LastChoiceDisplaying() {
        var maxChoiceIndex = choices.Length - 1;
        return buttons[buttons.Length - 1].choiceIndex == maxChoiceIndex;
    }

    private void OnChoiceClicked(int index) {
        onSelection(index);
    }
    
    private void HideUpButton() {
        HideButton(upButton);
    }

    private void HideDownButton() {
        HideButton(downButton);
    }

    private void HideButton(Component button) {
        button.gameObject.SetActive(false);
    }

    private void ShowDownButton() {
        ShowButton(downButton);
    }

    private void ShowUpButton() {
        ShowButton(upButton);
    }

    private void ShowButton(Component button) {
        button.gameObject.SetActive(true);
    }
    
    private class ChoiceButton {
        
        public delegate void OnClickDelegate(int index);
        private readonly TextMeshProUGUI gui;
        private readonly Button button;
        public Chat.Choice choice;
        public int choiceIndex;

        public ChoiceButton(Button button, OnClickDelegate clickDelegate) {
            this.button = button;
            gui = button.GetComponentInChildren<TextMeshProUGUI>();
            ReplaceOnClickEvent(clickDelegate);
        }

        public void Disable() {
            button.gameObject.SetActive(false);
        }

        public void Enable() {
            button.gameObject.SetActive(true);
        }
        
        public void DisplayChoice(Chat.Choice choice, int index) {
            this.choice = choice;
            choiceIndex = index;
            ReplaceDisplayedText(choice.text);
            Enable();
        }
        
        private void ReplaceDisplayedText(string replacement) {
            gui.text = replacement;
        }

        private void ReplaceOnClickEvent(OnClickDelegate onClick) {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(delegate {
                onClick(choiceIndex);
            });
        }
    }
}
