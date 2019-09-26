using System.Collections;
using Dialogue;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {
    
    public DialogueGraph conversation;
    public Transform standpoint;
    
    private IEnumerator StartConversation() {
        yield return new WaitUntil(() => Player.Instance.movement.HasReachedPosition(standpoint));
        DialogueManager.Instance.StartDialogue(conversation);
    }

    private void OnMouseUpAsButton() {
        Player.Instance.movement.MoveTo(standpoint);
        StartCoroutine(StartConversation());
    }
}
