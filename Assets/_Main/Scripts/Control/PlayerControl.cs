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
    private Animator animator;

    [Space(10f)] [Header(" GravityComponents")]
    [SerializeField] private float allowPlayerRotation = 0.1f;
    [SerializeField] private float gravity;
    [SerializeField] private float gravityMultiplier;

    [Header("Animation Smoothing")]
    [Range(0, 1f)] [SerializeField] private float horizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)] [SerializeField] private float verticalAnimSmoothTime = 0.2f;
    [Range(0, 1f)] [SerializeField] private float startAnimTime = 0.3f;
    [Range(0, 1f)] [SerializeField] private float stopAnimTime = 0.15f;

    bool isGrounded, isRuning, isCrouching;
    #endregion

    #region Initialization
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        cam = Camera.main;

        if (animator == null)
            Debug.LogError("Require" + transform.name + "GameObject to have an animator");
    }

    private void Update()
    {
        InputMagnitude();
        MovementManager();
    }
    #endregion

    #region PlayerMovement

    void MovementManager()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

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

    void InputMagnitude()
    {
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        animator.SetFloat("Vertical", InputZ,horizontalAnimSmoothTime, Time.deltaTime * 2f);
        animator.SetFloat("Horizontal", InputX,verticalAnimSmoothTime, Time.deltaTime * 2f);

        if(Speed > allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", Speed, startAnimTime, Time.deltaTime);
            RotationManager();
        }
        else if (Speed < allowPlayerRotation)
        {
            animator.SetFloat("InputMagnitude", Speed, stopAnimTime, Time.deltaTime);
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

    #endregion
}