using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton: MonoBehaviour, IWrapper<DialogueChoice> {

    public delegate void OnClickDelegate(int index);
    private OnClickDelegate onClick;
    private TextMeshProUGUI gui;
    private Button button;
    private DialogueChoice dialogueChoice;

    private void Awake() {
        button = GetComponent<Button>();
        gui = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetOnClickDelegate(OnClickDelegate clickDelegate) {
        onClick = clickDelegate;
    }
    
    public DialogueChoice GetContent() {
        return dialogueChoice;
    }

    public void SetContent(DialogueChoice content) {
        dialogueChoice = content;
        Refresh();
    }

    public bool HasContent(DialogueChoice contentCandidate) {
        return contentCandidate.Equals(dialogueChoice);
    }

    public bool IsEmpty() {
        return dialogueChoice == null;
    }

    public void Clear() {
        dialogueChoice = null;
        Disable();
    }

    private void Refresh() {
        ReplaceDisplayedText(dialogueChoice.choice.text);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { onClick(dialogueChoice.index); });
        Enable();
    }
    
    private void ReplaceDisplayedText(string replacement) {
        gui.text = replacement;
    }
    
    public void Enable() {
        button.gameObject.SetActive(true);
    }

    public void Disable() {
        button.gameObject.SetActive(false);
    }
}
