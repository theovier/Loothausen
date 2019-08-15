using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    public ChatBox chatbox;
    public ChoiceBox choicebox;
    public NameDisplayController displayName;
    public float fallbackChatDisplayTime = 3.0f; //when no audio clip is set, the chat will be displayed for these seconds
    
    private DialogueGraph dialogueGraph;
    private Chat currentChat;
    private bool active;
    
    private Coroutine scheduledChatSkip;
    private int audioID; //needed to stop dialogue audio when skipping dialogues
    
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
        DisplayCurrentChat();
    }
    
    private void EndDialogue() {
        active = false;
        chatbox.Hide();
        choicebox.Reset();
    }
    
    private void DisplayCurrentChat() {
        if (!active) return;
        currentChat = dialogueGraph.current;
        choicebox.Hide();
        chatbox.Show(currentChat);

        float audioDuration;
        var voiceClip = currentChat.voiceClip;
        if (voiceClip) {
            audioDuration = voiceClip.length;
            audioID = AudioManager.PlaySound(voiceClip);
        }
        else {
            audioDuration = fallbackChatDisplayTime;
        }
        
        scheduledChatSkip = StartCoroutine(ContinueDialogueAfterWait(audioDuration));
    }

    private IEnumerator ContinueDialogueAfterWait(float duration) {
        yield return new WaitForSeconds(duration);
        ContinueDialogue();
    } 

    private void DisplayChoices(List<Chat.Choice> choices) {
        chatbox.Hide();
        choicebox.DisplayChoices(choices.ToArray(), OnChoiceSelected);
    }
    
    private void Update() {
        if (!active) return;
        if (choicebox.IsVisible()) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            ContinueDialogue();
        }
    }

    private void ContinueDialogue() {
        StopCoroutine(scheduledChatSkip);
        StopCurrentDialogueAudio();
        
        if (currentChat.OffersChoices()){
            DisplayChoices(currentChat.choices);
        }
        else if (currentChat.HasSuccessors()) {
            currentChat.TriggerSuccessors();
            DisplayCurrentChat();
        }
        else {
            EndDialogue();
        }
    }

    private void StopCurrentDialogueAudio() {
        AudioManager.StopSound(audioID);
    }

    private void OnChoiceSelected(int index) {
        currentChat.SelectChoice(index);
        DisplayCurrentChat();
    }
}
