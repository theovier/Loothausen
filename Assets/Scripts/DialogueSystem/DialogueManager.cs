﻿using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using XNode;

public class DialogueManager : MonoBehaviour {

    public ChatBox chatbox;
    public ChoiceBox choicebox;
    public NameDisplayController displayName;
    
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
        if (active) return;
        active = true;
        dialogueGraph = dialogue;
        dialogueGraph.Restart();
        choicebox.Reset();
        displayName.Hide();
        ContinueDialogue();
    }
    
    private void EndDialogue() {
        active = false;
        chatbox.Hide();
        choicebox.Reset();
    }
    
    private void ContinueDialogue() {
        if (!active) return;
        //display the next chat; while giving answer the current of the graph is updated
        currentChat = dialogueGraph.current;
        DisplayChat();
    }
    
    private void DisplayChat() {
        choicebox.Hide();
        chatbox.Show(currentChat);
    }

    private void DisplayChoices(List<Chat.Choice> choices) {
        chatbox.Hide();
        choicebox.DisplayChoices(choices.ToArray(), OnChoiceSelected);
    }
    
    private void Update() {
        if (!active) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            HandleDialogueInteraction();
        }
    }

    private void HandleDialogueInteraction() {
        if (currentChat.OffersChoices()){
            DisplayChoices(currentChat.choices);
        }
        else if (currentChat.HasSuccessors()) {
            currentChat.TriggerNext();
            ContinueDialogue();
        }
        else {
            EndDialogue();
        }
    }

    private void OnChoiceSelected(int index) {
        currentChat.SelectChoice(index);
        ContinueDialogue();
    }
}