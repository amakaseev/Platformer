using UnityEngine;
using UnityEngine.AI;

namespace Platformer {
  public class EnemyWanderState : EnemyBaseState {
    private readonly NavMeshAgent agent;
    private readonly Vector3 startPoint;
    private readonly float wanderRadius;

    public EnemyWanderState(Enemy enemy, Animator animator, NavMeshAgent agent, float wanderRadius) : base(enemy, animator) {
      this.agent = agent;
      this.startPoint = enemy.transform.position;
      this.wanderRadius = wanderRadius;
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(WalkHash, crossFadeDuration);
    }

    public override void OnUpdate() {
      if (HasRechedDistanation()) {
        // find a new distination
        var randomDirtection = Random.insideUnitSphere * wanderRadius;
        randomDirtection += startPoint;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirtection, out hit, wanderRadius, 1);
        var finalPosition = hit.position;

        agent.SetDestination(finalPosition);
      }
    }
    bool HasRechedDistanation() {
      return !agent.pathPending
             && (agent.remainingDistance <= agent.stoppingDistance)
             && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }
  }

}
