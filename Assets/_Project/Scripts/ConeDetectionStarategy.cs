﻿
using UnityEngine;
using Utilites;

namespace Platformer {

  public class ConeDetectionStarategy : IDetectionStarategy {
    float detectionAngle;
    float detectionRadius;
    float innerDetectionRadius;

    public ConeDetectionStarategy(float detectionAngle, float detectionRadius, float innerDetectionRadius) {
      this.detectionAngle = detectionAngle;
      this.detectionRadius = detectionRadius;
      this.innerDetectionRadius = innerDetectionRadius;
    }

    public bool Exclude(Transform player, Transform detector, CountdownTimer timer) {
      if (timer.IsRunning) return false;

      var directionToPlayer = player.position - detector.position;
      var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);

      if ((!(angleToPlayer < detectionAngle / 2f)
        || !(directionToPlayer.magnitude < detectionRadius))
        && !(directionToPlayer.magnitude < innerDetectionRadius))
        return false;

      timer.Start();
      return true;
    }

  }

}