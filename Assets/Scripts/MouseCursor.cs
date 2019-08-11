using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MouseCursor : MonoBehaviour {

    public Sprite defaultCursor;
    public Sprite talkCursor;
    
    public Vector2 defaultCursorScale = new Vector2(13, 17);
    public Vector2 defaultCursorRotation = new Vector2(0, 20);
    
    public Vector2 talkCursorScale = new Vector2(17, 15);
    public Vector2 talkCursorRotation;
    
    private Image current;
    private RectTransform rectTransform;
    private CursorStyle currentStyle;

    private void Awake() {
        current = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        Cursor.visible = false;
        ChangeStyle(CursorStyle.Normal);
    }
    
    private void Update() {
        transform.position = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //expensive call
        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null) {
            SetTalkStyle();
        }
        else if (currentStyle != CursorStyle.Normal) {
            SetNormalStyle();
        }
    }
    
    private void OnApplicationFocus( bool focusStatus ) {
        if (focusStatus) {
            Cursor.visible = false;
        }
    }
    
    private void ChangeStyle(CursorStyle style) {
        switch (style) {
            case CursorStyle.Normal:
                SetNormalStyle();
                break;
            case CursorStyle.Talk:
                SetTalkStyle();
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
    
}

public enum CursorStyle {
    Normal,
    Talk
}
