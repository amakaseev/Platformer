
using System.Net.WebSockets;
using UnityEngine;
using Utilites;

namespace Platformer {

  public partial class PlayerDetector : MonoBehaviour {
    [SerializeField] float detectionAngle = 60f;
    [SerializeField] float detectionRadius = 10;
    [SerializeField] float innerDetectionRadius = 5;
    [SerializeField] float detectionCooldown = 1f;
    [SerializeField] float attackRange = 2;

    public Transform Player {  get; private set; }
    public Health PlayerHealth { get; private set; }

    CountdownTimer detectionTimer;

    IDetectionStarategy detectionStarategy;

    private void Awake() {
      Player = GameObject.FindGameObjectWithTag("Player").transform;
      PlayerHealth = Player.GetComponent<Health>();
    }


    void Start() {
      detectionTimer = new CountdownTimer(detectionCooldown);
      detectionStarategy = new ConeDetectionStarategy(detectionAngle, detectionRadius, innerDetectionRadius);
    }

    void Update() => detectionTimer.Tick(Time.deltaTime);

    public bool CanDetectPlayer() {
      return detectionTimer.IsRunning || detectionStarategy.Exclude(Player, transform, detectionTimer);
    }

    public bool CanAttackPlayer() {
      var directionToPlayer = Player.position - transform.position;
      return directionToPlayer.magnitude <= attackRange;
    }

    public void SetDetectionStrategy(IDetectionStarategy detectionStarategy) => this.detectionStarategy = detectionStarategy;

    private void OnDrawGizmos() {
      Gizmos.color = Color.red;

      Gizmos.DrawWireSphere(transform.position, detectionRadius);
      Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);

      Vector3 f = Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRadius;
      Vector3 b = Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRadius;

      Gizmos.DrawLine(transform.position, transform.position + f);
      Gizmos.DrawLine(transform.position, transform.position + b);
    }
  }

}
