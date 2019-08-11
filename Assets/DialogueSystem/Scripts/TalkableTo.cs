using System;
using Dialogue;
using UnityEngine;

public class TalkableTo : MonoBehaviour {
    
    public DialogueGraph conversation;
    
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartConversation();
        }
    }

    public void StartConversation() {
        DialogueManager.Instance.StartDialogue(conversation);
    }
}
