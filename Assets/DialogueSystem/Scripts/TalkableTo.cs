using Dialogue;
using UnityEngine;

public class TalkableTo : MonoBehaviour {
    
    public DialogueGraph conversation;
    
    private void StartConversation() {
        DialogueManager.Instance.StartDialogue(conversation);
    }

    private void OnMouseUpAsButton() {
        StartConversation();
    }
}
