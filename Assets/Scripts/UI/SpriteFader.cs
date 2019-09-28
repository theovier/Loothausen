using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Make this more generic, so the UIFader can be merged with this.
public class SpriteFader : MonoBehaviour {

    public SpriteRenderer renderer;
    public delegate void OnCompletion();
    
    public void FadeIn(float duration) {
        StartCoroutine(FadeSprite(renderer, renderer.color.a, 1, duration));
    }
    
    public void FadeIn(float duration, OnCompletion onCompletion) {
        StartCoroutine(FadeSprite(renderer, renderer.color.a, 1, duration, onCompletion));
    }
    
    public void FadeOut(float duration) {
        StartCoroutine(FadeSprite(renderer, renderer.color.a, 0, duration));
    }
    
    public void FadeOut(float duration, OnCompletion onCompletion) {
        StartCoroutine(FadeSprite(renderer, renderer.color.a, 0, duration, onCompletion));
    }

    public void Stop() {
        StopAllCoroutines();
    }
    
    private IEnumerator FadeSprite(SpriteRenderer renderer, float start, float end, float lerpTime) {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        var color = renderer.color;
        
        while (true) {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);
            
            color.a = currentValue;
            renderer.color = color;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator FadeSprite(SpriteRenderer renderer, float start, float end, float lerpTime, OnCompletion onCompletion) {
        yield return FadeSprite(renderer, start, end, lerpTime);
        onCompletion();
    }
}