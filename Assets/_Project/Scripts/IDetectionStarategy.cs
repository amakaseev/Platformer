
using UnityEngine;
using Utilites;

namespace Platformer {

  public interface IDetectionStarategy {
    public bool Exclude(Transform player, Transform detector, CountdownTimer timer);
  }

}
