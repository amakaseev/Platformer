﻿using UnityEngine;

namespace Platformer {
  public abstract class BaseState : IState {
    protected readonly PlayerController player;
    protected readonly Animator animator;

    protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    protected static readonly int JumpHash = Animator.StringToHash("Jump");
    protected static readonly int DashHash = Animator.StringToHash("Dash");

    protected const float crossFadeDuration = 0.1f;

    protected BaseState(PlayerController player, Animator animator) {
      this.player = player;
      this.animator = animator;
    }

    public virtual void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
    }

    public virtual void OnExit() {
      DebugUtils.LogFSM(this, ".OnExit");
    }

    public virtual void OnFixedUpdate() {
    }

    public virtual void OnUpdate() {
    }
  }
}
