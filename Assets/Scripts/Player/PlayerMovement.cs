using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour {

    public float speed;
  
    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 movementDirection;
    private Vector2 velocity;
    private Vector2 additionalVelocity; //set e.g. by attacking / dashing.
    private bool faceLeft;
    
    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() {
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
        velocity = (movementDirection.normalized + additionalVelocity) * speed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void Turn() {
        if (movementDirection.x < 0) {
            faceLeft = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movementDirection.x > 0){
            faceLeft = false;
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
     
    public void AddVelocity(Vector2 addition, float delay) {
        additionalVelocity = addition;
        if (faceLeft) {
            additionalVelocity *= -1;
        }
        StartCoroutine(ResetAdditionVelocity(delay));
    }

    private IEnumerator ResetAdditionVelocity(float delay) {
        yield return new WaitForSeconds(delay);
        additionalVelocity = new Vector2(0, 0);
    }    
}
