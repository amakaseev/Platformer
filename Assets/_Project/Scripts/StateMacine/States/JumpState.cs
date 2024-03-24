using UnityEngine;

namespace Platformer {
  public class JumpState : BaseState {
    public JumpState(PlayerController player, Animator animator) : base(player, animator) {
    }

    public override void OnEnter() {
      Debug.Log("JumpState.OnEnter");
      animator.CrossFade(JumpHash, crossFadeDuration);
    }

    public override void OnFixedUpdate() {
      player.HandleJump();
      player.HandleMovement();
    }
  }
}
