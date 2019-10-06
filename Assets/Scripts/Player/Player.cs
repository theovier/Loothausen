using UnityEngine;

public class Player : MonoBehaviour {

    public PlayerMovement movement;
    public Inventory inventory;

    public bool IsInteracting; //true if the player currently has an open dialogue
    public bool HasInventoryOpened; //true if the cursor is inside the inventory menu
    
    private static Player instance;
    private bool frozen;
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

    public bool IsAllowedToMove() {
        return !IsInteracting && !HasInventoryOpened && !frozen;
    }

    public void Freeze() {
        frozen = true;
    }

    public void Unfreeze() {
        frozen = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse2)) {
            inventory.ToggleUI();
        }
    }
    
    


}
