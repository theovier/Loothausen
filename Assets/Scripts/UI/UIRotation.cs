using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIRotation : MonoBehaviour {

    public float durationForOneRotation = 30;
    
    private RectTransform rect;
    private Sequence run;
    
    private void Start() {
        rect = GetComponent<RectTransform>();
        run = DOTween.Sequence();
        Tween rot = rect.transform.DORotate(new Vector3(0, 0, 360), durationForOneRotation, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        run.Append(rot).SetLoops(-1);
    }

    public void Play() {
        run.Play();
    }

    public void Stop() {
        run.Pause();
    }
}
