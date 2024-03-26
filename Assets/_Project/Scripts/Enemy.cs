using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer {

  [RequireComponent(typeof(NavMeshAgent))]
  [RequireComponent(typeof(PlayerDetector))]
  public class Enemy : Entity {

    [SerializeField, Self] NavMeshAgent agent;
    [SerializeField, Self] PlayerDetector playerDetector;
    [SerializeField, Child] Animator animator;

    [SerializeField] float wandedRadius = 10f;
    StateMachine stateMachine;

    private void OnValidate() => this.ValidateRefs();

    private void Start() {
      stateMachine = new StateMachine();

      var wanderState = new EnemyWanderState(this, animator, agent, wandedRadius);
      var chaceState = new EnemyChaceState(this, animator, agent, playerDetector.Player);

      At(wanderState, chaceState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
      At(chaceState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
      //Any(wanderState, new FuncPredicate(() => true)); // always trrue

      stateMachine.SetState(wanderState);
    }

    void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
    void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

    private void Update() {
      stateMachine.Update();
    }

    private void FixedUpdate() {
      stateMachine.FixedUpdate();
    }
  }
}
