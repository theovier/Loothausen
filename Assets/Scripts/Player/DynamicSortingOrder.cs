using UnityEngine;

public class DynamicSortingOrder : MonoBehaviour {
    
    private SpriteRenderer[] spriteRenderers;
    
    private void Awake() {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    
    private void LateUpdate() {
        foreach (var o in spriteRenderers) {
            o.sortingOrder = (int) (transform.position.y * -100f);
        }
    }
}
