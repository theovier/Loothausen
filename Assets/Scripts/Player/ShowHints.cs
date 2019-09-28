using System;
using UnityEngine;

public class ShowHints : MonoBehaviour {

    public float timeTilFullyVisible = 5.0f;
    private Hint[] hints;

    private float timeButtonPressed;
    private float startButtonPressedTime;
    private bool buttonPressed;
    
    private void Start() {
        hints = FindObjectsOfType<Hint>();
        HideHints();
    }

    private void Update() {
        if (Input.GetButtonDown("ShowHints") && !buttonPressed) {
            startButtonPressedTime = Time.time;
            HighlightHints();
            buttonPressed = true;
        }
        else if (Input.GetButtonUp("ShowHints")) {
            timeButtonPressed = Math.Min(Time.time - startButtonPressedTime, timeTilFullyVisible);
            FadeOutHints();
            buttonPressed = false;
        }
    }
    
    private void HighlightHints() {
        foreach (var hint in hints) {
            hint.FadeIn(timeTilFullyVisible);
        }
    }
    
    private void FadeOutHints() {
        foreach (var hint in hints) {
            hint.FadeOut(timeButtonPressed);
        }
    }
    
    private void HideHints() {
        foreach (var hint in hints) {
            hint.Hide();
        }
    }
}
