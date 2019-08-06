using System.Collections;
using UnityEngine;

public class HideAndShowAnimation : MonoBehaviour {

    public bool hiddenOnStart = true;
    public float minTimeShown = 8;
    public float maxTimeShown = 30;
    public float minTimeHidden = 8;
    public float maxTimHidden = 30;

    private SpriteRenderer sprite;

    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        if (hiddenOnStart) {
            sprite.enabled = false;
        }
        StartCoroutine(EndlessToggleVisiblity());
    }

    IEnumerator EndlessToggleVisiblity() {
        while (true) {
            var wait = sprite.enabled ? RandomTimeHidden() : RandomTimeShown();
            yield return new WaitForSeconds(wait);
            toggleVisibility();
        }
    }

    private void toggleVisibility() {
        sprite.enabled = !sprite.enabled;
    }
    
    private float RandomTimeShown() {
        return Random.Range(minTimeShown, maxTimeShown);
    }
    
    private float RandomTimeHidden() {
        return Random.Range(minTimeHidden, maxTimHidden);
    }
    
}
