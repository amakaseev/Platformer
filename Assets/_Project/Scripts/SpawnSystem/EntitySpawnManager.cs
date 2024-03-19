using UnityEngine;

namespace Platformer {
  public abstract class EntitySpawnManager : MonoBehaviour {
    protected enum SpawnPointStrategyType {
      Linear,
      Random
    }

    [SerializeField] protected SpawnPointStrategyType spawnPointStrategyType = SpawnPointStrategyType.Linear;
    [SerializeField] protected Transform[] spawnPoints;

    protected ISpawnPointStrategy spawnPointStrategy;

    protected virtual void Awake() {
      spawnPointStrategy = spawnPointStrategyType switch {
        SpawnPointStrategyType.Linear => spawnPointStrategy = new LinearSpawnPointStrategy(spawnPoints),
        SpawnPointStrategyType.Random => spawnPointStrategy = new RandomSpawnPointStrategy(spawnPoints),
        _ => spawnPointStrategy
      };
    }

    public abstract void Spawn();
  }
}
