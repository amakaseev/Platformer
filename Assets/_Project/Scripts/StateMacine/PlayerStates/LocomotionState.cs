using UnityEngine;

namespace Platformer {
  public class LocomotionState : PlayerBaseState {
    public LocomotionState(PlayerController player, Animator animator) : base(player, animator) {
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(LocomotionHash, crossFadeDuration);
    }

    public override void OnFixedUpdate() {
      player.HandleMovement();
    }
  }
}
