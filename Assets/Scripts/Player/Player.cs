using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerMovement movement;
    public Inventory inventory;
    
    private static Player instance;
    public static Player Instance => instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Debug.LogWarning("Another instance of PlayerUtil was created and will be destroyed.");
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }


}
