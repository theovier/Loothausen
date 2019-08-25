using UnityEngine;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour {

    public Sprite defaultCursor;
    public Sprite talkCursor;
    public Sprite inspectCursor;
    public Sprite grabCursor;
    
    public Vector2 defaultCursorScale = new Vector2(13, 17);
    public Vector2 defaultCursorRotation = new Vector2(0, 20);
    
    public Vector2 talkCursorScale = new Vector2(17, 15);
    public Vector2 talkCursorRotation;
    
    public Vector2 inspectCursorScale = new Vector2(13, 13);
    public Vector2 inspectCursorRotation;
    
    public Vector2 grabCursorScale = new Vector2(13, 13);
    public Vector2 grabCursorRotation;
    
    public LayerMask layerMask;

    public bool lockStyle;
    
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
        //let the raycast happen first because it may trigger events
        if (lockStyle) return; 
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
            case "Grab": {
                ChangeStyle(CursorStyle.Grab);
                break;
            }
            default: {
                ChangeStyle(CursorStyle.Normal);
                break;
            }
        }
    }
    
    public void ChangeStyle(CursorStyle style) {
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
            case CursorStyle.Grab:
                SetGrabStyle();
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
    
    private void SetGrabStyle() {
        currentStyle = CursorStyle.Grab;
        current.sprite = grabCursor;
        rectTransform.localRotation = Quaternion.Euler(0, grabCursorRotation.x, grabCursorRotation.y);
        rectTransform.localScale = new Vector3(grabCursorScale.x, grabCursorScale.y, 0);
    }
    
}

public enum CursorStyle {
    Normal,
    Talk,
    Inspect,
    Grab
}
