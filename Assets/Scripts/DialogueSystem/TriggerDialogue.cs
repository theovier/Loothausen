using System.Collections;
using Dialogue;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriggerDialogue : MonoBehaviour, IPointerClickHandler {
    
    public DialogueGraph conversation;
    public Transform standpoint;
    public MovementDirection faceDirection;
    
    public void OnPointerClick(PointerEventData eventData) {
        Player.Instance.movement.MoveTo(standpoint);
        StartCoroutine(StartConversation());
    }
    
    private IEnumerator StartConversation() {
        yield return new WaitUntil(() => Player.Instance.movement.HasReachedPosition(standpoint));
        Player.Instance.movement.Turn(faceDirection);
        DialogueManager.Instance.StartDialogue(conversation);
    }
}
