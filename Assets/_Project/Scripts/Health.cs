using UnityEngine;

namespace Platformer {
  public class Health : MonoBehaviour {
    [SerializeField] int maxHealth = 100;
    [SerializeField] FloatEventChanel playerHealthChanel;

    int currentHealth;

    public bool IsDead => currentHealth <= 0;

    private void Awake() {
      currentHealth = maxHealth;
    }

    private void Start() {
      PublishHealthPercentage();
    }

    public void TakeDamage(int damage) {
      currentHealth -= damage;
      PublishHealthPercentage();
    }

    void PublishHealthPercentage() => playerHealthChanel?.Invoke(currentHealth / (float)maxHealth);
  }
}
