using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour {

    public Sprite defaultCursor;
    public Sprite talkCursor;
    public Sprite inspectCursor;
    
    public Vector2 defaultCursorScale = new Vector2(13, 17);
    public Vector2 defaultCursorRotation = new Vector2(0, 20);
    
    public Vector2 talkCursorScale = new Vector2(17, 15);
    public Vector2 talkCursorRotation;
    
    public Vector2 inspectCursorScale = new Vector2(13, 13);
    public Vector2 inspectCursorRotation;
    
    public LayerMask layerMask;
    
    private Image current;
    private RectTransform rectTransform;
    private CursorStyle currentStyle;
    private Camera mainCamera;

    private void Awake() {
        current = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void Start() {
        Cursor.visible = false;
        ChangeStyle(CursorStyle.Normal);
    }
    
    private void Update() {
        UpdateCursorPosition();
        UpdateCursorStyle();
    }

    private void UpdateCursorPosition() {
        transform.position = Input.mousePosition;
    }

    private void UpdateCursorStyle() {
        var hit = RaycastFromMouse();
        if (hit) {
            ChangeStyle(hit.collider.tag);
        } else {
            ChangeStyle(CursorStyle.Normal);
        }
    }

    private RaycastHit2D RaycastFromMouse() {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition); 
        return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
    }
    
    private void OnApplicationFocus(bool focusStatus) {
        if (focusStatus) {
            Cursor.visible = false;
        }
    }

    private void ChangeStyle(string raycastHitTag) {
        switch (raycastHitTag) {
            case "Talk": {
                ChangeStyle(CursorStyle.Talk);
                break;
            }
            case "Inspect": {
                ChangeStyle(CursorStyle.Inspect);
                break;
            }
            default: {
                ChangeStyle(CursorStyle.Normal);
                break;
            }
        }
    }
    
    private void ChangeStyle(CursorStyle style) {
        if (currentStyle == style) {
            return;
        }
        switch (style) {
            case CursorStyle.Normal:
                SetNormalStyle();
                break;
            case CursorStyle.Talk:
                SetTalkStyle();
                break;
            case CursorStyle.Inspect:
                SetInspectStyle();
                break;
            default:
                SetNormalStyle();
                break;
        }
    }

    private void SetNormalStyle() {
        currentStyle = CursorStyle.Normal;
        current.sprite = defaultCursor;
        rectTransform.localRotation = Quaternion.Euler(0, defaultCursorRotation.x, defaultCursorRotation.y);
        rectTransform.localScale = new Vector3(defaultCursorScale.x, defaultCursorScale.y, 0);
    }

    private void SetTalkStyle() {
        currentStyle = CursorStyle.Talk;
        current.sprite = talkCursor;
        rectTransform.localRotation = Quaternion.Euler(0, talkCursorRotation.x, talkCursorRotation.y);
        rectTransform.localScale = new Vector3(talkCursorScale.x, talkCursorScale.y, 0);
    }

    private void SetInspectStyle() {
        currentStyle = CursorStyle.Inspect;
        current.sprite = inspectCursor;
        rectTransform.localRotation = Quaternion.Euler(0, inspectCursorRotation.x, inspectCursorRotation.y);
        rectTransform.localScale = new Vector3(inspectCursorScale.x, inspectCursorScale.y, 0);
    }
    
}

public enum CursorStyle {
    Normal,
    Talk,
    Inspect
}
