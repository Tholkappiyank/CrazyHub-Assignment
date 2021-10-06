using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BennyKok.RuntimeDebug.Components;
using BennyKok.RuntimeDebug.Attributes;

namespace CrazyHub.Hyderabad.Assignment
{
    public class Test_PlayerController : RuntimeDebugBehaviour
    {
        PlayerController playerController;

        private void Start() {
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        [DebugAction]
        public void laneSpacing(int laneSpace) {
            playerController.laneSpace = laneSpace;
        }

        [DebugAction]
        public void ForwardVelocity(int forwardvelocity) {
            playerController.forwaredSpeed = forwardvelocity;
        }

        [DebugAction]
        public void JumpVelocity(int jumpvelocity) {
            playerController.jumpVelocity = jumpvelocity;
        }


    }
}
