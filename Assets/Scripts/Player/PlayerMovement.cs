﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Pathfinding;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour {
    
    public float speed;
    public GameObject rootLocation;
    
    private Animator animator;
    private bool faceLeft;
    private Camera mainCamera;
    private IEnumerable<Transform> locations;
    private Transform target;
    private AIDestinationSetter destinationSetter;
    private AIPath aiPath;
    
    private void Start() {
        animator = GetComponent<Animator>();
        locations = rootLocation.transform.Cast<Transform>();
        target = transform;
        mainCamera = Camera.main;

        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = speed;
        destinationSetter = GetComponent<AIDestinationSetter>();
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
            SelectClosestLocation(clickedPosition);
        }
    }

    private void SelectClosestLocation(Vector3 pos) {
        var closestLocation = locations.OrderBy(t => (t.position - pos).sqrMagnitude)
            .First();
        target = closestLocation;
    }

    private void Animate() {
        animator.SetBool("isRunning", !aiPath.reachedEndOfPath);
    }
    
    private void Move() {
        destinationSetter.target = target;
    }

    private void Turn() {
        if (aiPath.desiredVelocity.x <= -0.01f) {
            faceLeft = true;
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (aiPath.desiredVelocity.x >= 0.01f){
            faceLeft = false;
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}
