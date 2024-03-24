using UnityEngine;

namespace Platformer {
  public class GroundChecker : MonoBehaviour {
    [SerializeField] float groundDistance = 0.08f;
    [SerializeField] float checkerRadius = 0.25f;
    [SerializeField] LayerMask groundLayers;

    Vector3 groundPoint;

    public bool IsGrounded {  get; private set; }

    private void Update() {
      RaycastHit hit;
      var delta = Vector3.down * groundDistance;
      IsGrounded = Physics.SphereCast(transform.position - delta, checkerRadius, Vector3.down, out hit, groundDistance * 2f, groundLayers);
      if (IsGrounded) {
        groundPoint = hit.point;
      }
    }

    private void OnDrawGizmos() {
      var delta = Vector3.down * groundDistance;
      Gizmos.DrawWireSphere(transform.position - delta, checkerRadius);
      Gizmos.color = Color.green;
      Gizmos.DrawWireSphere(transform.position + delta, checkerRadius);

      if (IsGrounded) {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPoint, checkerRadius);
      }
    }
  }

}
