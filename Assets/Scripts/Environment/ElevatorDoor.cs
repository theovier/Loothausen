using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoor : MonoBehaviour {

    private Animator animator;
    private bool open;
    private SpriteRenderer[] doorSpriteRenderer;

    private void Start() {
        animator = GetComponent<Animator>();
        doorSpriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        Open();
    }

    private void Open() {
        open = true;
        animator.SetBool("open", open);
    }

    private void Close() {
        open = false;
        animator.SetBool("open", open);
    }
    
    private void ToggleDoor() {
        if (open) {
            Close();
        }
        else {
            Open();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            ToggleDoor();
        }
    }
}
