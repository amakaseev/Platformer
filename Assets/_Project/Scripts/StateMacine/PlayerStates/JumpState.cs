using UnityEngine;

namespace Platformer {
  public class JumpState : PlayerBaseState {
    public JumpState(PlayerController player, Animator animator) : base(player, animator) {
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(JumpHash, crossFadeDuration);
    }

    public override void OnFixedUpdate() {
      player.HandleJump();
      player.HandleMovement();
    }
  }
}
