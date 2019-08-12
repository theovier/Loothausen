using System.Collections;
using UnityEngine;

public class StreetLampFlickerLight : MonoBehaviour {

    
    public Component lightComponent;
    public float minTimeBetweenFlickering = 5;
    public float maxTimeBetweenFlickering = 25;
    public SpriteRenderer head;
    
    private Animator animator;
    private RuntimeAnimatorController animController;
    private float flickeringAnimDuration;
    private float timeBetweenFlickering;
    
    
    void Start() {
        animator = lightComponent.GetComponent<Animator>();
        animController = animator.runtimeAnimatorController;
        flickeringAnimDuration = getAnimationDuration("street_lamp_flickering");
        timeBetweenFlickering = RandomFlickeringTime();
        StartCoroutine(scheduleFlickeringAnimation(timeBetweenFlickering));
    }

    private float getAnimationDuration(string name) {
        foreach (var clip in animController.animationClips) {
            if (clip.name.Equals(name)) {
                return clip.length;
            }
        }
        return -1;
    }
    
    IEnumerator scheduleFlickeringAnimation(float wait) {
        yield return new WaitForSeconds(wait);
        startFlickeringAnimation();
    }

    private void startFlickeringAnimation() {
        animator.SetTrigger("flickerTrigger");
        timeBetweenFlickering = RandomFlickeringTime();
        StartCoroutine(scheduleFlickeringAnimation(timeBetweenFlickering));
    }

    private float RandomFlickeringTime() {
        return Random.Range(flickeringAnimDuration + minTimeBetweenFlickering, maxTimeBetweenFlickering);
    }

    public void HideLampHead() {
        head.gameObject.SetActive(false);
    }

    public void ShowLampHead() {
        head.gameObject.SetActive(true);
    }
    
    
}
