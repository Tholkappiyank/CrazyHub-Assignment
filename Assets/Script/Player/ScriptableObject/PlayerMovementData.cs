using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    [CreateAssetMenu(menuName ="Player/PlayerMovementData", fileName ="PlayerMovementData")]
    public class PlayerMovementData : ScriptableObject
    {
        public float speed;
        [Range(0, 1)]
        public float forwardVelocity = 0.0f;
        public float rotationSpeed = 720f;
        public float JumpHeight = 0;
        public float jumpSpeed = 5;
        public float jumpButtonGracePeriod = 0.3f;        
    }
}
