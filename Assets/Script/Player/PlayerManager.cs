using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CrazyHub.Hyderabad.Assignment
{
    public class PlayerManager : MonoBehaviour
    {
        public static bool GameOver;
        public static bool GameWin;
        public GameObject GameOverPanel;
        public GameObject StartPanel;
        public TextMeshProUGUI CoinPanel;
        public GameObject GameWinPanel;

        // Start is called before the first frame update

        public static bool isGameStarted;

        public static int NumberOfCoins { get; internal set; }

        AudioManager audioManager;

        bool tap = true;

        private void Awake() {
            audioManager = FindObjectOfType<AudioManager>();

        }

        void Start()
        {
            GameOver = false;
            Time.timeScale = 1;
            isGameStarted = false;
            GameWin = false;
            NumberOfCoins = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameOver) {
                Time.timeScale = 0;               
                audioManager.StopSound("MainTheme");
                GameOverPanel.SetActive(true);                
            }

            if (GameWin) {
                Time.timeScale = 0;
                audioManager.StopSound("MainTheme");
                GameWinPanel.SetActive(true);
            }

            CoinPanel.SetText("Coins: "+ NumberOfCoins);

            if (SwipeManager.tap && tap) {
                tap = false;
                isGameStarted = true;
                audioManager.PlaySound("MainTheme");
                Destroy(StartPanel);
            }
        }
    }
}
