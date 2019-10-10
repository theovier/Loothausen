using UnityEngine;

public class PlayerMovementGamepad : MonoBehaviour {
    
    public float maxSpeed = 7;
    public float acceleration = 110;
    public float deceleration = 110;

    private Vector2 velocity;
    private Vector2 movementDirection;

    private Animator animator;
    private bool faceLeft;
    
    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    private void Update() {
        HandleInput();
        Animate();
        Move();
        Turn();
    }

    private void HandleInput() {
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    
    private void Animate() {
        animator.SetBool("isRunning", movementDirection != Vector2.zero);
    }

    private void Move() {
        if (movementDirection == Vector2.zero) {
            velocity = Vector2.MoveTowards(velocity, Vector2.zero, deceleration * Time.deltaTime);
        }
        else {
            velocity = Vector2.MoveTowards(velocity, maxSpeed * movementDirection.normalized, acceleration * Time.deltaTime);
        }
        transform.position += (Vector3) velocity * Time.deltaTime;
    }

    private void Turn() {
        if (velocity.x <= -0.01f) {
            TurnLeft();
        } else if (velocity.x >= 0.01f){
            TurnRight();
        }
    }
    
    private void TurnLeft() {
        faceLeft = true;
        transform.eulerAngles = new Vector3(0, 180, 0);
    }
    
    private void TurnRight() {
        faceLeft = false;
        transform.eulerAngles = new Vector3(0,0,0);
    }

    public void Turn(MovementDirection direction) {
        switch (direction) {
            case MovementDirection.LEFT: {
                TurnLeft();
                break;
            }
            case MovementDirection.RIGHT: {
                TurnRight();
                break;
            }
        }
    }
}