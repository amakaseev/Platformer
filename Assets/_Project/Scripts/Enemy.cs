using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;
using Utilites;

namespace Platformer {

  [RequireComponent(typeof(NavMeshAgent))]
  [RequireComponent(typeof(PlayerDetector))]
  public class Enemy : Entity {

    [SerializeField, Self] NavMeshAgent agent;
    [SerializeField, Self] PlayerDetector playerDetector;
    [SerializeField, Child] Animator animator;

    [SerializeField] float wandedRadius = 10f;
    [SerializeField] float timeBetweenAttack = 1f;


    StateMachine stateMachine;

    CountdownTimer attackTimer;

    private void OnValidate() => this.ValidateRefs();

    private void Start() {
      attackTimer = new CountdownTimer(timeBetweenAttack);
      stateMachine = new StateMachine();

      var wanderState = new EnemyWanderState(this, animator, agent, wandedRadius);
      var chaceState = new EnemyChaceState(this, animator, agent, playerDetector.Player);
      var attackState = new EnemyAttackState(this, animator, agent, playerDetector.Player);

      At(wanderState, chaceState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
      At(chaceState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
      At(chaceState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
      At(attackState, chaceState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));

      stateMachine.SetState(wanderState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Update() {
      stateMachine.Update();
      attackTimer.Tick(Time.deltaTime);
    }

    private void FixedUpdate() {
      stateMachine.FixedUpdate();
    }

    public void Attack() {
      if (attackTimer.IsRunning) return;

      attackTimer.Start();
      playerDetector.PlayerHealth.TakeDamage(10);
    }
  }
}
