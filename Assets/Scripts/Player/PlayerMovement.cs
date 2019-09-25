using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
    
    //set by the waypoint controller
    [HideInInspector]public Transform current_waypoint;

    private Animator animator;
    private bool faceLeft;

    private void Awake() {
        animator = GetComponent<Animator>();
        current_waypoint = transform;
    }
    
    private void FixedUpdate() {
        Animate();
        Move();
        Turn();
    }

    private void Animate() {
        animator.SetBool("isRunning", !CurrentWaypointReached());
    }

    private bool CurrentWaypointReached() {
        return transform.position == current_waypoint.position;
    }
    
    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, current_waypoint.position, speed * Time.fixedDeltaTime);
    }

    private void Turn() {
        if (current_waypoint.position.x < transform.position.x) {
            faceLeft = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (current_waypoint.position.x > 0) {
            faceLeft = false;
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}
