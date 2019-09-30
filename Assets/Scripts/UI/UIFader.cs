using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//taken from https://www.youtube.com/watch?v=92Fz3BjjPL8 by Omnirift
public class UIFader : MonoBehaviour {

    public CanvasGroup uiElement;
    public delegate void OnCompletion();
    
    public void FadeIn(float duration) {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, duration));
    }
    
    public void FadeIn(float duration, OnCompletion onCompletion) {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, duration, onCompletion));
    }
    
    public void FadeOut(float duration) {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, duration));
    }
    
    public void FadeOut(float duration, OnCompletion onCompletion) {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, duration, onCompletion));
    }
    
    public void StopCurrentFade() {
        StopAllCoroutines();
    }
    
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime) {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime, OnCompletion onCompletion) {
        yield return FadeCanvasGroup(cg, start, end, lerpTime);
        onCompletion();
    }
}