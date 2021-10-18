using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    public class Coin : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            // Rotate the coin by 20 cycles
            transform.Rotate(new Vector3(20.0f * Time.deltaTime, 0, 0));
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player") {
                PlayerManager.NumberOfCoins++;
                Debug.Log("Coins" + PlayerManager.NumberOfCoins);
                //FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
                gameObject.SetActive(false);
            }
        }

        private void OnDisable() {
            
        }


    }
}
