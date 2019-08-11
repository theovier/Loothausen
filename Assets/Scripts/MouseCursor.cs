using UnityEngine;

public class MouseCursor : MonoBehaviour {
    
    void Start() {
        Cursor.visible = false;
    }
    
    void Update() {
        transform.position = Input.mousePosition;
    }
    
    void OnApplicationFocus( bool focusStatus ) {
        if (focusStatus) {
            Cursor.visible = false;
        }
    }
}
