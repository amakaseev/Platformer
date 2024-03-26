using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer {

  [RequireComponent(typeof(NavMeshAgent))]
  public class Enemy : Entity {
    [SerializeField, Self] NavMeshAgent agent;
    [SerializeField, Child] Animator animator;

    StateMachine stateMachine;

    private void OnValidate() => this.ValidateRefs();

  }

}
