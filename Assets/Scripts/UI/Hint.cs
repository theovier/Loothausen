using UnityEngine;

public class Hint : MonoBehaviour {

    private SpriteRenderer renderer;
    private SpriteFader fader;
    
    private void Start() {
        renderer = GetComponent<SpriteRenderer>();
        fader = gameObject.AddComponent<SpriteFader>();
        fader.renderer = renderer;
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
        var color = renderer.color;
        color.a = 0;
        renderer.color = color;
    }
}
