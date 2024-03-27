using UnityEngine;

namespace Platformer {
  public class Collectable: Entity {
    [SerializeField] int score = 10; // FIXME set using factory
    [SerializeField] IntEventChanel scoreChanel;

    private void OnTriggerEnter(Collider other) {
      if (other.CompareTag("Player")) {
        scoreChanel.Invoke(score);
        Destroy(gameObject);
      }
    }
  }
}
