using UnityEngine;

namespace Platformer {
  public class AttackState : PlayerBaseState {
    public AttackState(PlayerController player, Animator animator) : base(player, animator) {
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(AttackHash, crossFadeDuration);
      //player.Attack();
    }

    public override void OnFixedUpdate() {
      player.HandleMovement();
    }
  }
}
