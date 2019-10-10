using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour {

    public GraphicRaycaster gr;

    public Sprite defaultCursor;
    public Sprite talkCursor;
    public Sprite inspectCursor;
    public Sprite grabCursor;
    public Sprite goCursor;

    public Vector2 defaultCursorScale = new Vector2(13, 17);
    public Vector2 defaultCursorRotation = new Vector2(0, 20);

    public Vector2 talkCursorScale = new Vector2(17, 15);
    public Vector2 talkCursorRotation;

    public Vector2 inspectCursorScale = new Vector2(13, 13);
    public Vector2 inspectCursorRotation;

    public Vector2 grabCursorScale = new Vector2(13, 13);
    public Vector2 grabCursorRotation;
    
    public Vector2 goCursorScale = new Vector2(13, 13);
    public Vector2 goCursorRotation;

    public LayerMask layerMask;

    public bool lockStyle;

    private Image current;
    private RectTransform rectTransform;
    private CursorStyle currentStyle;
    private Camera mainCamera;

    //moving the cursor with the analog stick
    public float speed = 700;
    private Vector2 aimDirection;

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
        //todo check for gamepad usage
        aimDirection = new Vector2(Input.GetAxisRaw("RightAnalogStickX"), Input.GetAxisRaw("RightAnalogStickY"));
        transform.position += (Vector3)aimDirection * speed * Time.deltaTime;
        //transform.position = Input.mousePosition;
    }

    private void UpdateCursorStyle() {
        var hit = PhysicsRaycastFromMouse();
        //let the raycast happen first because it may trigger events
        if (lockStyle) return;
        if (IsCursorOverChatUI()) {
            ChangeStyle(CursorStyle.Normal);
        }
        else if (hit) {
            ChangeStyle(hit.collider.tag);
        } else {
            ChangeStyle(CursorStyle.Normal);
        }
    }

    private RaycastHit2D PhysicsRaycastFromMouse() {
        var ray = mainCamera.ScreenPointToRay(transform.position); 
        return Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, layerMask);
    }
    
    private bool IsCursorOverChatUI() {
        return GraphicRaycast().Any(hit => hit.gameObject.tag.Equals("Chat"));
    }

    private List<RaycastResult> GraphicRaycast() {
        var ped = new PointerEventData(null);
        ped.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        return results;
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
            case "Go": {
                ChangeStyle(CursorStyle.Go);
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
            case CursorStyle.Go:
                SetGoStyle();
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

    private void SetGoStyle() {
        currentStyle = CursorStyle.Go;
        current.sprite = goCursor;
        rectTransform.localRotation = Quaternion.Euler(0, goCursorRotation.x, goCursorRotation.y);
        rectTransform.localScale = new Vector3(goCursorScale.x, goCursorScale.y, 0);
    }
    
}

public enum CursorStyle {
    Normal,
    Talk,
    Inspect,
    Grab,
    Go
}
