using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour {

    #region Variables
    [SerializeField] private PlayerStats playerStats;

    private float InputX, InputZ, Speed;
    private Camera cam;
    private CharacterController characterController;

    private Vector3 desiredMoveDIrection;

    private new Transform transform;

    [Space(10f)] [Header(" GravityComponents")]
    [SerializeField] private float allowRotation = 0.1f;
    [SerializeField] private float gravity;
    [SerializeField] private float gravityMultiplier;

    bool isGrounded, isRuning, isCrouching;
    #endregion

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        cam = Camera.main;

    }

    private void Update()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        InputDecider();
        MovementManager();
    }

    void InputDecider()
    {
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        if(Speed > allowRotation)
        {
            RotationManager();
        }
        else
        {
            desiredMoveDIrection = Vector3.zero;
        }
    }

    void RotationManager()
    {

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        desiredMoveDIrection = forward * InputZ + right * InputX;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(desiredMoveDIrection), playerStats.rotationSpeed);
    }

    void MovementManager()
    {
        gravity -= 9.8f * Time.deltaTime;
        gravity = gravity * gravityMultiplier;

        Vector3 moveDirection = desiredMoveDIrection * (playerStats.walkingSpeed * Time.deltaTime);
        moveDirection = new Vector3(moveDirection.x, gravity, moveDirection.z);
        characterController.Move(moveDirection);

        if (characterController.isGrounded)
        {
            gravity = 0f;
        }
    }
}