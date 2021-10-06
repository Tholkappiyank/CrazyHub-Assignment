using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    [CreateAssetMenu(fileName ="PlayerControllerData", menuName ="Player/PlayerControllerData")]
    public class PlayerControllerData : ScriptableObject
    {
        public float laneSpacing = 2;
        public int defaultLane = 1;
        public float forwardSpeed = 0;
    }
}
