using System;
using Dialogue;
using UnityEngine;

public class TalkableTo : MonoBehaviour {
    
    public DialogueGraph conversation;
    
    public void StartConversation() {
        DialogueManager.Instance.StartDialogue(conversation);
    }

    private void OnMouseUp() {
        StartConversation();
    }
}
