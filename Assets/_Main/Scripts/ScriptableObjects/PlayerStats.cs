using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerStats", menuName = "TopDownStuff/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject {

    [Header ("Movement")]
    public float walkingSpeed = 10f;
    public float sprintSpeed = 15f;
    public float jumpForce = 10f;
    public float gravity;
    public float rotationSpeed = 0.3f;

    [Header ("Other")][Space (10f)]
    public float damage = 10f;

}