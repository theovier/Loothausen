using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour {

    public Transform groundPosition;
    private SpriteRenderer[] spriteRenderers;

    private void Awake() {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    
    private void LateUpdate() {
        foreach (var o in spriteRenderers) {
            o.sortingOrder = (int) (groundPosition.position.y * -100f);
        }
    }
}
