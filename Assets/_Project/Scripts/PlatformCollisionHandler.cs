using UnityEngine;

namespace Platformer {
  public class PlatformCollisionHandler : MonoBehaviour {
    Transform platform; // The platform, if any we are on top of

    void OnCollisionEnter(Collision other) {
      if (other.gameObject.CompareTag("MovingPlatform")) {
        ContactPoint contact = other.GetContact(0);
        if (contact.normal.y < 0.9f)
          return;

        Sticks(other.transform);
      }
    }

    void OnCollisionExit(Collision other) {
      if (other.gameObject.CompareTag("MovingPlatform")) {
        transform.SetParent(null);
        platform = null;
      }
    }

    void Sticks(Transform platform) {
      this.platform = platform;
      transform.SetParent(platform);
    }
  }
}
