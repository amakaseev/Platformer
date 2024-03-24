using UnityEngine;

namespace Platformer {
  public class DashState : BaseState {
    public DashState(PlayerController player, Animator animator) : base (player, animator) {
    }

    public override void OnEnter() {
      Debug.Log("DashState.OnEnter");
      animator.CrossFade(DashHash, crossFadeDuration);
    }

    public override void OnFixedUpdate() {
      player.HandleMovement();
    }
  }
}
