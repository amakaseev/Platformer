﻿using System;
using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using Utilites;

namespace Platformer {
  public class PlayerController : ValidatedMonoBehaviour {

    [Header("References")]
    [SerializeField, Self] Rigidbody rb;
    [SerializeField, Self] GroundChecker groundChecker;
    [SerializeField, Self] Animator animator;
    [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
    [SerializeField, Anywhere] InputReader input;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] float smoothTime = 0.2f;

    [Header("Jump Settings")]
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpDuration = 0.5f;
    [SerializeField] float jumpCooldown = 0f;
    [SerializeField] float gravityMultiplayer = 3f;

    [Header("Dash Settings")]
    [SerializeField] float dashForce = 5f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCooldown = 2f;

    Transform mainCam;

    const float ZeroF = 0f;

    float currentSpeed;
    float velocity;
    float jumpVelocity;
    float dashVelocity = 1;

    Vector3 movement;

    List<Timer> timers;
    CountdownTimer jumpTimer;
    CountdownTimer jumpCooldownTimer;
    CountdownTimer dashTimer;
    CountdownTimer dashCooldownTimer;

    StateMachine stateMachine;

    // Animator params
    static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake() {
      mainCam = Camera.main.transform;
      freeLookVCam.Follow = transform;
      freeLookVCam.LookAt = transform;
      freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);

      rb.freezeRotation = true;

      // Setup timers
      jumpTimer = new CountdownTimer(jumpDuration);
      jumpCooldownTimer = new CountdownTimer(jumpCooldown);
      jumpTimer.OnTimerStart += () => jumpVelocity = jumpForce;
      jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

      dashTimer = new CountdownTimer(dashDuration);
      dashCooldownTimer = new CountdownTimer(dashCooldown);
      dashTimer.OnTimerStart += () => dashVelocity = dashForce;
      dashTimer.OnTimerStop += () => {
        Debug.Log("!!dashTimer.OnTimerStop!!");
        dashVelocity = 1;
        dashCooldownTimer.Start();
      };

      timers = new List<Timer>(4) { jumpTimer, jumpCooldownTimer, dashTimer, dashCooldownTimer };

      // State Machine
      stateMachine = new StateMachine();

      // Declare states
      var locomotionState = new LocomotionState(this, animator);
      var jumpState = new JumpState(this, animator);
      var dashState = new DashState(this, animator);

      // Define transitions
      At(locomotionState, jumpState, new FuncPredicate(() => jumpTimer.IsRunning));
      At(locomotionState, dashState, new FuncPredicate(() => dashTimer.IsRunning));
      Any(locomotionState, new FuncPredicate(() => groundChecker.IsGrounded && !jumpTimer.IsRunning && !dashTimer.IsRunning));

      // Set initial state
      stateMachine.SetState(locomotionState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Start() {
      input.EnablePlayerActions();
    }

    private void OnEnable() {
      input.Jump += OnJump;
      input.Dash += OnDash;
    }

    private void OnDisable() {
      input.Jump -= OnJump;
      input.Jump -= OnDash;
    }

    void OnJump(bool performed) {
      if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded) {
        jumpTimer.Start();
      } else if (!performed && jumpTimer.IsRunning) {
        jumpTimer.Stop();
      }
    }

    void OnDash(bool performed) {
      if (performed && !dashTimer.IsRunning && !dashCooldownTimer.IsRunning) {
        dashTimer.Start();
      } else if (!performed && dashTimer.IsRunning) {
        dashTimer.Stop();
      }
    }

    void Update() {
      movement = new Vector3(input.Direction.x, 0f, input.Direction.y);

      HandleTimers();
      UpdateAnimator();

      stateMachine.Update();
    }

    private void FixedUpdate() {
      stateMachine.FixedUpdate();
    }

    private void UpdateAnimator() {
      animator.SetFloat(Speed, currentSpeed);
    }

    void HandleTimers() {
      foreach (var timer in timers) {
        timer.Tick(Time.deltaTime);
      }
    }

    public void HandleJump() {
      // If not jumping and grounded, keep jump velocity at 0
      if (!jumpTimer.IsRunning && groundChecker.IsGrounded) {
        jumpVelocity = ZeroF;
        jumpTimer.Stop();
      }

      if (!jumpTimer.IsRunning) {
        // Gravity takes over
        jumpVelocity += Physics.gravity.y * gravityMultiplayer * Time.fixedDeltaTime;
      }

      // Apply velocity
      rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
    }

    public void HandleMovement() {
      // Rotate movement direction to match camera rotation
      var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
      if (adjustedDirection.magnitude > ZeroF) {
        HandleRotation(adjustedDirection);
        HandleHorizontalMovement(adjustedDirection);
        SmoothSpeed(adjustedDirection.magnitude);
      } else {
        SmoothSpeed(ZeroF);

        // Reset horizontal velocity for a snappy stop
        rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
      }
    }

    private void HandleHorizontalMovement(Vector3 adjustedDirection) {
      // Move the player
      Vector3 velocity = adjustedDirection * moveSpeed * dashVelocity * Time.fixedDeltaTime;
      rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    void HandleRotation(Vector3 adjustedDirection) {
      // Adjust rotation to match movement direction
      var targetRotation = Quaternion.LookRotation(adjustedDirection);
      transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void SmoothSpeed(float value) {
      currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
    }

  }
}
