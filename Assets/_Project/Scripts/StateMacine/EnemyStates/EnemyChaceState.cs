using UnityEngine;
using UnityEngine.AI;

namespace Platformer {

  public class EnemyChaceState : EnemyBaseState {
    private readonly NavMeshAgent agent;
    private readonly Transform player;

    public EnemyChaceState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator) {
      this.agent = agent;
      this.player = player;
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(RunHash, crossFadeDuration);
    }

    public override void OnUpdate() {
      agent.SetDestination(player.position);
    }
  }

}
