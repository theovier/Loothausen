using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
    public GameObject waypointsRoot;

    private Animator animator;
    private bool faceLeft;
    private IEnumerable<Transform> waypoints;
    private Transform currentWaypoint;
    private Camera mainCamera;
    
    
    private void Awake() {
        animator = GetComponent<Animator>();
        waypoints = waypointsRoot.transform.Cast<Transform>();
        currentWaypoint = transform;
        mainCamera = Camera.main;
    }
    
    private void FixedUpdate() {
        HandleInput();
        Animate();
        Move();
        Turn();
    }
    
    private void HandleInput() {
        if (Input.GetMouseButtonDown(0)) {
            var clickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var closestWaypoint = waypoints.OrderBy(t => (t.position - clickedPosition).sqrMagnitude)
                .First();
            currentWaypoint = closestWaypoint;
        }
    }

    private void Animate() {
        animator.SetBool("isRunning", !CurrentWaypointReached());
    }

    private bool CurrentWaypointReached() {
        return transform.position == currentWaypoint.position;
    }
    
    private void Move() {
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, speed * Time.fixedDeltaTime);
    }

    private void Turn() {
        if (currentWaypoint.position.x < transform.position.x) {
            faceLeft = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (currentWaypoint.position.x > 0) {
            faceLeft = false;
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}
