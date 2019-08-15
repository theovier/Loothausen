using Dialogue;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {
    
    public DialogueGraph conversation;
    
    private void StartConversation() {
        DialogueManager.Instance.StartDialogue(conversation);
    }

    private void OnMouseUpAsButton() {
        StartConversation();
    }
}
