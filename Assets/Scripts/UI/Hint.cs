using UnityEngine;

public class Hint : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private SpriteFader fader;
    
    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fader = gameObject.AddComponent<SpriteFader>();
        fader.spriteRenderer = spriteRenderer;
    }

    public void FadeIn(float duration) {
        fader.Stop();
        fader.FadeIn(duration);
    }

    public void FadeOut(float duration) {
        fader.Stop();
        fader.FadeOut(duration);
    }

    public void Hide() {
        fader.Stop();
        var color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
    }
}
