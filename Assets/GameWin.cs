using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    public class GameWin : MonoBehaviour
    {
        public ParticleSystem particleSystem_1;
        public ParticleSystem particleSystem_2;


        private void OnEnable() {
            PlayerManager.OnWih += PlayerManager_OnWih;
        }

        // Start is called before the first frame update
        void Start()
        {
           
            particleSystem_1.Stop();
            particleSystem_2.Stop();
        }

        private void PlayerManager_OnWih() {
            particleSystem_1.Play();
            particleSystem_2.Play();
           
        }

        private void OnCollisionEnter(Collision collision) {
            collision.gameObject.GetComponent<PlayerMovement>().forwardVelocity = 0;
        }

        private void OnDisable() {
            PlayerManager.OnWih -= PlayerManager_OnWih;
        }
    }
}
