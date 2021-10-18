using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    public class StoreManager : MonoBehaviour
    {
        public void On100coinsPurchaseComplete() {
            PlayerManager.NumberOfCoins += 100;
        }

        public void On200coinsPurchaseComplete() {
            PlayerManager.NumberOfCoins += 200;
        }

        public void OnRemoveAdsPurchaseComplete() {

        }
    }
}
