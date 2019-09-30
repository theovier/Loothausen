using System.Collections;
using Dialogue;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour {
    
    public DialogueGraph conversation;
    public Transform standpoint;
    public MovementDirection faceDirection;
    
    private void OnMouseUpAsButton() {
        Player.Instance.movement.MoveTo(standpoint);
        StartCoroutine(StartConversation());
    }
    
    private IEnumerator StartConversation() {
        yield return new WaitUntil(() => Player.Instance.movement.HasReachedPosition(standpoint));
        Player.Instance.movement.Turn(faceDirection);
        DialogueManager.Instance.StartDialogue(conversation);
    }
}
