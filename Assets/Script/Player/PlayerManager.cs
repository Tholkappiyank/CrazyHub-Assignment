namespace CrazyHub.Hyderabad.Assignment
{
    using System;    
    using UnityEngine;
    using TMPro;
    using Lean.Touch;  

    public class PlayerManager : MonoBehaviour
    {
        public static bool GameOver;
        public static bool GameWin;
        public GameObject GameOverPanel;
        public GameObject StartPanel;
        public GameObject CoinPanel;
        public GameObject GameWinPanel;

        TextMeshProUGUI coinText;
        // Start is called before the first frame update

        public static bool isGameStarted;

        public static int NumberOfCoins { get; internal set; }

        AudioManager audioManager;      
       

        // Event
        public static event Action OnTap_SetForwardVelocity;
        // Event On Start
        public static event Action OnTap_Start;
        // Jump
        public static event Action OnTap_Jump;
        // Player Win
        public static event Action OnWih;

        private void OnEnable() {
            OnTap_Start += PlayerManager_OnTap_Start;
        }      

        private void OnDisable() {
            OnTap_Start -= PlayerManager_OnTap_Start;
        }

        private void Awake() {
            audioManager = FindObjectOfType<AudioManager>();
            coinText = CoinPanel.GetComponentInChildren<TextMeshProUGUI>();                 
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
                audioManager.StopSound("MainTheme");
                GameWinPanel.SetActive(true);
                OnWih?.Invoke();
            }

            coinText.SetText("Coins: "+ NumberOfCoins);

            if (SwipeManager.tap) {
                StartPanel.SetActive(false);
                GameStart();
            }
               
        }
        
        // On tap Disable the Start Screen.
        // Called from Event Handler.
        public void GameStart() {
            // OnTap_Start invoked only once.
            OnTap_Start?.Invoke();
            // Disable Tap On Screen and Invoke only once.
            OnTap_SetForwardVelocity?.Invoke();            
        }

        private void PlayerManager_OnTap_Start() {         

            Debug.Log("--- Game Start -----");
            isGameStarted = true;
            audioManager.PlaySound("MainTheme");

            OnTap_Start -= PlayerManager_OnTap_Start;

        }
    }
}
