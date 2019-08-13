using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class StreetLampFlickerLight : MonoBehaviour {
    
    public SpriteRenderer lightComponent;
    public float minTimeLightsOff = 5;
    public float maxTimeLightsOff = 25;
    public float minTimeLightsOn = 5;
    public float maxTimeLightsOn = 25;
    public SpriteRenderer lightsOnHead;

    private Animator animator;
    private bool isLightOn;

    private void Awake() {
        Init();
    }

    private void Init() {
        animator = lightComponent.GetComponent<Animator>();
    }
    
    private void Start() {
        StartCoroutine(LoopLightChange());
    }

    private IEnumerator LoopLightChange() {
        while (true) {
            if (isLightOn) {
                TurnOffLights();
                yield return new WaitForSeconds(LightsOffTime());
            }
            else {
                TurnOnLights();
                yield return new WaitForSeconds(LightsOnTime());
            }
        }
    }

    private void TurnOnLights() {
        isLightOn = true;
        animator.SetBool("isOn", isLightOn);
    }

    private void TurnOffLights() {
        isLightOn = false;
        animator.SetBool("isOn", isLightOn);
    }
    
    private float LightsOnTime() {
        return Random.Range(minTimeLightsOn, maxTimeLightsOn);
    }

    private float LightsOffTime() {
        return Random.Range(minTimeLightsOff, maxTimeLightsOff);
    }

    //set via animation
    public void HideLightsOnHead() {
        lightsOnHead.enabled = false;
    }

    //set via animation
    public void ShowLightsOnHead() {
        lightsOnHead.enabled = true;
    }
}
