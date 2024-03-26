using UnityEngine;

namespace Platformer {
  public class DashState : PlayerBaseState {
    public DashState(PlayerController player, Animator animator) : base (player, animator) {
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(DashHash, crossFadeDuration);
    }

    public override void OnFixedUpdate() {
      player.HandleMovement();
    }
  }
}
