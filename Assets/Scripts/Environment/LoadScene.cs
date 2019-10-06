using UnityEngine;
using UnityEngine.EventSystems;

public class LoadScene : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler  {

    public string target;

    private SceneTransitionManager transitionManager;

    private void Start() {
        transitionManager = FindObjectOfType<SceneTransitionManager>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        transitionManager.StartTransition(target);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Player.Instance.Freeze();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Player.Instance.Unfreeze();
    }
}
