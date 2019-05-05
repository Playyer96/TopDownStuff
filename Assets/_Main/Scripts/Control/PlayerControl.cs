using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour {

    [SerializeField] private PlayerStats playerStats;

    private float InputX, InputZ;
    private Camera cam;
    private CharacterController characterController;

    private Vector3 desiredMoveDIrection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    private void Update()
    {
        
    }

    void MovementManager()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desiredMoveDIrection = forward * InputZ + right * InputX;
    }
}