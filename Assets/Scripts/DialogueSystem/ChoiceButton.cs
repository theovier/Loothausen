using TMPro;
using UnityEngine.UI;

public class ChoiceButton: CutoutListWrapper<DialogueChoice> {

    public delegate void OnClickDelegate(int index);
    private OnClickDelegate onClick;
    private TextMeshProUGUI gui;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
        gui = button.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void SetOnClickDelegate(OnClickDelegate clickDelegate) {
        onClick = clickDelegate;
    }
    
    protected override void Refresh() {
        ReplaceDisplayedText(content.choice.text);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { onClick(content.index); });
        Enable();
    }
    
    private void ReplaceDisplayedText(string replacement) {
        gui.text = replacement;
    }
}
