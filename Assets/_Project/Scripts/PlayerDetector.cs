
using UnityEngine;
using Utilites;

namespace Platformer {

  public partial class PlayerDetector : MonoBehaviour {
    [SerializeField] float detectionAngle = 60f;
    [SerializeField] float detectionRadius = 10;
    [SerializeField] float innerDetectionRadius = 5;
    [SerializeField] float detectionCooldown = 1f;

    public Transform Player {  get; private set; }
    CountdownTimer detectionTimer;

    IDetectionStarategy detectionStarategy;

    void Start() {
      detectionTimer = new CountdownTimer(detectionCooldown);
      Player = GameObject.FindGameObjectWithTag("Player").transform;
      detectionStarategy = new ConeDetectionStarategy(detectionAngle, detectionRadius, innerDetectionRadius);
    }

    void Update() => detectionTimer.Tick(Time.deltaTime);

    public bool CanDetectPlayer() {
      return detectionTimer.IsRunning || detectionStarategy.Exclude(Player, transform, detectionTimer);
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
