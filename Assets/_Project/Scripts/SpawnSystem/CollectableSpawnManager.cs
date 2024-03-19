using UnityEngine;
using Utilites;

namespace Platformer {
  public class CollectableSpawnManager : EntitySpawnManager {
    [SerializeField] CollectableData[] collectableData;
    [SerializeField] float spawnInterval = 1f;

    EntitySpawner<Collectable> spawner;

    CountdownTimer spawnTimer;
    int counter;

    protected override void Awake() {
      base.Awake();

      spawner = new EntitySpawner<Collectable>(
        new EntityFactory<Collectable>(collectableData),
        spawnPointStrategy);

      spawnTimer = new CountdownTimer(spawnInterval);
      spawnTimer.OnTimerStop += () => {
        if (counter++ >= spawnPoints.Length) {
          spawnTimer.Stop();
          return;
        }
        Spawn();
        spawnTimer.Start();
      };
    }

    private void Start() => spawnTimer.Start();
    private void Update() => spawnTimer.Tick(Time.deltaTime);

    public override void Spawn() => spawner.Spawn();
  }
}
