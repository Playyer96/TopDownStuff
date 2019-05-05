using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [SerializeField] private PlayerStats playerStats;

    private new Transform transform;
    private new Rigidbody rigidbody;

    private Vector3 moveDirection = Vector3.zero;

    private void Start () {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate () {
        Move();
    }

    private void Update () {
    }

    void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidbody.AddForce(movement * playerStats.walkingSpeed);
    }

    void Shoot () {
        print ("We shot the sherif!");
    }
}