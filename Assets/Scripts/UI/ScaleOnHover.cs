using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Vector2 scaleOnHover = new Vector2(1.2f, 1.2f);
    public float scaleDurationOnHover = 0.25f;
    private RectTransform rectTransform;
    
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        rectTransform.DOScale(new Vector3(scaleOnHover.x, scaleOnHover.y, 0), scaleDurationOnHover).SetAutoKill(false);
    }

    public void OnPointerExit(PointerEventData eventData) {
        rectTransform.DOPlayBackwards();
    }
}
