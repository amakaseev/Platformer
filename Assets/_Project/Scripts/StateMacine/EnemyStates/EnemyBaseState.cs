
using UnityEngine;

namespace Platformer {
  public abstract class EnemyBaseState : IState {
    protected readonly Enemy enemy;
    protected readonly Animator animator;

    // Get animation hashes
    protected static readonly int IdleHash = Animator.StringToHash("IdleNormal");
    protected static readonly int RunHash = Animator.StringToHash("RunFWD");
    protected static readonly int WalkHash = Animator.StringToHash("WalkFWD");
    protected static readonly int AttackHash = Animator.StringToHash("Attack01");
    protected static readonly int DieHash = Animator.StringToHash("Die");

    protected readonly float crossFadeDuration = 0.1f;

    protected EnemyBaseState(Enemy enemy, Animator animator) {
      this.enemy = enemy;
      this.animator = animator;
    }

    public virtual void OnEnter() {
      DebugUtils.LogFSM(this, ".OnEnter");
    }

    public virtual void OnExit() {
      DebugUtils.LogFSM(this, ".OnExit");
    }

    public virtual void OnFixedUpdate() {
    }

    public virtual void OnUpdate() {
    }
  }

}
