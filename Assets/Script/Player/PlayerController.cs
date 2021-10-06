namespace CrazyHub.Hyderabad.Assignment {
    using System;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour {

        private CharacterController controller;
        Vector3 direction;


        public float forwaredSpeed = 0;
        private int defaultLane = 1; //0:left, 1: middle 2: right       
        public float laneSpace = 4; // the disance between two lanes.   

        public PlayerControllerData playerData;

        public float ySpeed;
        public float gravity = -9.81f;
        public float jumpVelocity = 20;

        private void OnEnable() {
            forwaredSpeed = playerData.forwardSpeed;
            laneSpace = playerData.laneSpacing;
            defaultLane = playerData.defaultLane;
        }



        // Start is called before the first frame update
        void Start() {
            controller = GetComponent<CharacterController>();
            direction = Vector3.zero;
        }

        // Update is called once per frame
        void Update() {

            if (!PlayerManager.isGameStarted)
                return;

            direction.z = forwaredSpeed;

            if (controller.isGrounded) {
                direction.y = -1;
                if (SwipeManager.swipeUp) {  // Input.GetKeyDown(KeyCode.Space)
                    Jump();
                }
            } else {
                direction.y += gravity * Time.deltaTime;
            }

            if (SwipeManager.swipeRight) {  //  // Input.GetKeyDown(KeyCode.RightArrow)
                defaultLane++;

                if (defaultLane == 3) {
                    defaultLane = 2;
                }
            }

            if (SwipeManager.swipeLeft) {  // Input.GetKeyDown(KeyCode.LeftArrow)
                defaultLane--;

                if (defaultLane == -1)
                    defaultLane = 0;
            }

            // calculate where we should be in the future;
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

            if (defaultLane == 0) {
                targetPosition += Vector3.left * laneSpace;
            } else if (defaultLane == 2) {
                targetPosition += Vector3.right * laneSpace;
            }

            //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 *  Time.deltaTime);
            //transform.position = targetPosition;

            if (transform.position == targetPosition) return;

            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

            if (moveDir.sqrMagnitude < diff.sqrMagnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        private void Jump() {
            direction.y = jumpVelocity;
        }

        private void FixedUpdate() {

            if (!PlayerManager.isGameStarted)
                return;

            controller.Move(direction * Time.fixedDeltaTime);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            if(hit.transform.tag == "Obstacle") {
                PlayerManager.GameOver = true;
            }

            if(hit.transform.tag == "Win") {
                PlayerManager.GameWin = true;
            }
        }
    }
}
