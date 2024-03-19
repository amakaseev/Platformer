﻿using UnityEngine;

namespace Platformer {
  public class LinearSpawnPointStrategy : ISpawnPointStrategy {
    int index = 0;
    Transform[] spawnPoints;

    public LinearSpawnPointStrategy(Transform[] spawnPoints) {
      this.spawnPoints = spawnPoints;
    }

    public Transform NextSpawnPoint() {
      Transform point = spawnPoints[index];
      index = (index + 1) % spawnPoints.Length;
      return point;
    }
  }
}
