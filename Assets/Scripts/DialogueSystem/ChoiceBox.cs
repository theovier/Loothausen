using System.Collections.Generic;
using DG.Tweening;
using Dialogue;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class ChoiceBox : MonoBehaviour {
    
    public float slideAnimationTargetPosX;
    public float slideAnimationDuration = 0.02f;
    
    public delegate void OnChoiceSelected(int choiceIndex);
    private OnChoiceSelected onSelection;
    private RectTransform rectTransform;  //tweening
    
    
    private List<IWrapper<DialogueChoice>> choiceButtons = new List<IWrapper<DialogueChoice>>();
    private ShiftableCutoutList<DialogueChoice> cutoutList;
    private Button downButton;
    private Button upButton;
    
    
    private void Awake() {
        InitAnimationValues();
        InitButtons();
        InitShiftableCutoutList();
        AddListenersToControlButtons();
    }

    private void InitAnimationValues() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void InitButtons() {
       InitControlButtons();
       InitChoiceButtons();
    }

    private void InitControlButtons() {
        var buttonComponents = GetComponentsInChildren<Button>();
        upButton = buttonComponents[0];
        downButton = buttonComponents[buttonComponents.Length - 1];
    }

    private void InitChoiceButtons() {
        GetComponentsInChildren(true, choiceButtons);
        foreach (var choiceButton in choiceButtons) {
            (choiceButton as ChoiceButton).SetOnClickDelegate(OnChoiceClicked);
        }
    }

    private void AddListenersToControlButtons() {
        upButton.onClick.AddListener(cutoutList.OnPrevious);
        downButton.onClick.AddListener(cutoutList.OnNext);
    }
    
    private void InitShiftableCutoutList() {
        cutoutList = new ShiftableCutoutList<DialogueChoice>(choiceButtons, downButton.gameObject, upButton.gameObject);
        cutoutList.Init();
    }
    
    private void Start() {
        gameObject.SetActive(false);
    }
    
    private void Show() {
        gameObject.SetActive(true);
        rectTransform.DOAnchorPosX(slideAnimationTargetPosX, slideAnimationDuration);
    }
    
    public void Hide() {
        rectTransform.DOPlayBackwards();
        gameObject.SetActive(false);
    }
    
    public bool IsHidden() {
        return !gameObject.activeSelf;
    }

    public bool IsVisible() {
        return !IsHidden();
    }

    public void Reset() {
        Hide();
        cutoutList.Clear();
    }
    
    public void DisplayChoices(List<Chat.Choice> newChoices, OnChoiceSelected onSelection) {
        this.onSelection = onSelection;
        AddChoices(newChoices);
        Show();
    }

    private void AddChoices(List<Chat.Choice> choices) {
        for (var i = 0; i < choices.Count; i++) {
            var choice = choices[i];
            var dialogueChoice = new DialogueChoice(choice, i);
            cutoutList.Add(dialogueChoice);
        }
    }
    
    private void OnChoiceClicked(int index) {
        onSelection(index);
        cutoutList.Clear();
    }
}
