using System;
using UnityEngine;
using Lean.Common;


namespace CrazyHub.Hyderabad.Assignment
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed;
        [Range(0, 1)]      
        public float forwardVelocity = 0.2f;
        public float rotationSpeed;
        public float jumpSpeed;
        public float jumpButtonGracePeriod;
        public bool isJump;

        public PlayerMovementData _playerData;
        private CharacterController characterController;

        private float ySpeed;
        private float originalStepOffset;
        private float? lastGroundedTime;
        private float? jumpButtonPressedTime;

        public Animator _animator;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDIdle;
        private int _animIDJump;
        private int _animIDRun;
        private int _animIDDance;

        private bool _hasAnimator;


        //Events
     
        LeanRoll leanRoll;
       
        private void OnEnable() {
            PlayerManager.OnTap_SetForwardVelocity += PlayerManager_OnTapDisableStartScreen;
           
        }      

        private void OnDisable() {
            PlayerManager.OnTap_SetForwardVelocity -= PlayerManager_OnTapDisableStartScreen;
           
        }

        private void Awake() {
            leanRoll = GameObject.FindGameObjectWithTag("LeanRoll").GetComponent<LeanRoll>();
        }

        // Start is called before the first frame update
        void Start() {

           // AssignPlayerData();

            _hasAnimator = TryGetComponent(out _animator);

            AssignAnimationIDs();
      
            characterController = GetComponent<CharacterController>();
            originalStepOffset = characterController.stepOffset;
        }

        private void PlayerManager_OnTapDisableStartScreen() {
            forwardVelocity = 1;
            PlayerManager.OnTap_SetForwardVelocity -= PlayerManager_OnTapDisableStartScreen;
        }

        private void AssignPlayerData() {
          speed = _playerData.speed;      
          forwardVelocity = _playerData.forwardVelocity;
          rotationSpeed = _playerData.rotationSpeed;
          jumpSpeed = _playerData.jumpSpeed;
          jumpButtonGracePeriod = _playerData.jumpButtonGracePeriod;
        }

        private void AssignAnimationIDs() {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDIdle = Animator.StringToHash("Idle");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDRun = Animator.StringToHash("Run");
            _animIDDance = Animator.StringToHash("Dance");
        }


        // Update is called once per frame
        void Update() {

            if (!PlayerManager.isGameStarted)
                return;

            // Input from touch from -90 to 90. To Make it between -1 to 1 divide by 90.
            float horizontalInput = leanRoll.Angle/90.0f;  //Input.GetAxis("Horizontal");
            float verticalInput = forwardVelocity; //Input.GetAxis("Vertical")

            Debug.Log("Horizontal Input"+ horizontalInput);

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded) {
                lastGroundedTime = Time.time;
                _animator.SetBool(_animIDJump, false);
               
            }

            if (isJump) {
                isJump = false; // Input.GetButtonDown("Jump")
                jumpButtonPressedTime = Time.time;
                _animator.SetBool(_animIDJump, true);
            }

            // Is Ground 
            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) {   // if (characterController.isGrounded)
               // characterController.stepOffset = originalStepOffset;
                ySpeed = -0.5f;                

                if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod) {
                    ySpeed = jumpSpeed;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            } else {  // Jumping
                characterController.stepOffset = 0;              
            }

            Vector3 velocity = movementDirection * magnitude;
            velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);

            if (movementDirection != Vector3.zero) {

                _animator.SetBool(_animIDRun, true);
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            } else {
                _animator.SetBool(_animIDRun, false);
            }          
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            if (hit.transform.tag == "Obstacle") {
                PlayerManager.GameOver = true;
            }

            if (hit.transform.tag == "Win") {
                PlayerManager.GameWin = true;
                _animator.SetBool(_animIDDance, true);

            }
        }
        
        public void IsJumping() {          
            isJump = true;
        }
    }
}


