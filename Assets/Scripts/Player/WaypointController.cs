using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointController : MonoBehaviour {

    public PlayerMovement playerMovement;
    
    private IEnumerable<Transform> waypoints;
    private Camera mainCamera;

    private void Awake() {
        mainCamera = Camera.main;
    }

    private void Start() {
        waypoints = transform.Cast<Transform>();
    }

    private void Update() {
        HandleInput();
    }

    private void HandleInput() {
        if (Input.GetMouseButtonDown(0)) {
            var clickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var clostestWaypoint = waypoints.OrderBy(t => (t.position - clickedPosition).sqrMagnitude)
                .First();
            playerMovement.current_waypoint = clostestWaypoint;
        }
    }

}
