using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
  [Tooltip("Scalar value that controls the max value of the magnitude of the character")]
  public float MaxSpeed = 10f;

  [Tooltip("How long will it take to reach the desired turn angle in seconds")]
  public float TurnRate = 0.1f;

  [Tooltip("Minimum value of normalized input to cause change in direction")]
  public float MinInputThreshold = 0.01f;

  [Tooltip("Look at transform controls which way is forward. Usually the main camera.")]
  public Transform LookAtTransform;

  private CharacterController controller;
  private Vector3 velocity;
  private Vector2 directionInput;
  private float turnSmoothVelocity;

  void Start()
  {
    controller = GetComponent<CharacterController>();
    velocity = Vector3.zero;
  }

  void Update()
  {
    UpdateVelocity();
    controller.SimpleMove(velocity);
  }

  private void UpdateVelocity()
  {
    UpdateDirection();
  }

  void OnMove(InputValue value)
  {
    directionInput = value.Get<Vector2>();
  }

  private void UpdateDirection()
  {
    if(directionInput.magnitude >= MinInputThreshold)
    {
      // Thanks to Brackeys
      var targetAngle = Mathf.Atan2(directionInput.x, directionInput.y) * Mathf.Rad2Deg + LookAtTransform.eulerAngles.y;
      var dampedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TurnRate);
      transform.rotation = Quaternion.Euler(0f, dampedAngle, 0f);

      var moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
      moveDirection.Normalize();

      velocity.x = moveDirection.x * MaxSpeed;
      velocity.z = moveDirection.z * MaxSpeed;
    }
    else
    {
      velocity = Vector3.zero;
    }
  }
}
