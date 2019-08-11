﻿using System.Collections.Generic;
using Dialogue;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public ChatBox chatbox;
    public ChoiceBox choicebox;
    
    private DialogueGraph dialogueGraph;
    private Chat currentChat;
    private bool active;
    
    
    private static DialogueManager instance;
    public static DialogueManager Instance => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Debug.LogWarning("Another instance of DialogueManager was created and will be destroyed.");
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    public void StartDialogue(DialogueGraph dialogue) {
        active = true;
        dialogueGraph = dialogue;
        dialogueGraph.Restart();
        choicebox.Reset();
        ContinueDialogue();
    }
    
    public void EndDialogue() {
        active = false;
        chatbox.Hide();
        choicebox.Reset();
    }
    
    private void ContinueDialogue() {
        if (!active) return;
        //display the next chat
        currentChat = dialogueGraph.current; // while giving answer the current of the graph is updated
        DisplayChat(currentChat.text);
    }
    
    private void DisplayChat(string text) {
        choicebox.Hide();
        chatbox.Show(text);
    }

    private void DisplayChoices(List<Chat.Choice> choices) {
        chatbox.Hide();
        choicebox.DisplayChoices(choices.ToArray(), OnChoiceSelected);
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (chatbox.IsVisible() && active) {
                chatbox.Hide();
                if (currentChat.OffersChoices()){
                    DisplayChoices(currentChat.choices);
                }
                else {
                    if (currentChat.HasSuccessors()) {
                        currentChat.TriggerNext();
                        ContinueDialogue();
                    }
                    else {
                        chatbox.Hide();
                    }
                }
            }
        }
    }
    
    private void OnChoiceSelected(int index) {
        if (currentChat.GiveAnswer(index)) {
            ContinueDialogue();
        }
        else {
            EndDialogue();
        }
    }
}
