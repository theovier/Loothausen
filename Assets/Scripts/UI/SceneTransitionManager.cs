using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour {

    private Animator animator;
    private string targetScene;
    
    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void StartTransition(string to) {
        targetScene = to;
        animator.SetTrigger("triggerTransition");
    }

    public void OnEndAnimationPlayed() {
        SceneManager.LoadScene(targetScene);
    }
}
