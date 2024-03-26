﻿
using UnityEngine;
using UnityEngine.AI;

namespace Platformer {

  public class EnemyAttackState : EnemyBaseState {
    private readonly NavMeshAgent agent;
    private readonly Transform player;

    public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator) {
      this.agent = agent;
      this.player = player;
    }

    public override void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
      animator.CrossFade(AttackHash, crossFadeDuration);
    }

    public override void OnUpdate() {
      agent.SetDestination(player.position);
      enemy.Attack();
    }
  }

}
