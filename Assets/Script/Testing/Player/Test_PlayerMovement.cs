namespace CrazyHub.Hyderabad.Assignment
{
    using UnityEngine;
    using BennyKok.RuntimeDebug.Components;
    using BennyKok.RuntimeDebug.Attributes;
    public class Test_PlayerMovement : RuntimeDebugBehaviour {

        PlayerMovement playerMovement;

        // Start is called before the first frame update
        void Start()
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }      

        [DebugAction]
        public void Speed(int Speed) {
            playerMovement.speed = Speed;
        }

        [DebugAction]
        public void JumpVelocity(int jumpSpeed) {
            playerMovement.jumpSpeed = jumpSpeed;
        }

       
    }
}
