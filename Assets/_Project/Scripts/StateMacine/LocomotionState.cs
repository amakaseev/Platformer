﻿using UnityEngine;

namespace Platformer {
  public class LocomotionState : BaseState {
    public LocomotionState(PlayerController player, Animator animator) : base(player, animator) {
    }

    public override void OnEnter() {
      Debug.Log("LocomotionState.OnEnter");
      animator.CrossFade(LocomotionHash, crossFadeDuration);
    }

    public override void OnFixedUpdate() {
      player.HandleMovement();
    }
  }
}