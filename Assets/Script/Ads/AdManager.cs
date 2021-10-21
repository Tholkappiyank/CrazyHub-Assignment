namespace CrazyHub.Hyderabad.Assignment
{   
    using UnityEngine;
    using GoogleMobileAds;
    using GoogleMobileAds.Api;
    using System;

    public class AdManager : MonoBehaviour
    {
        private BannerView bannerAd;
        private InterstitialAd interstitialAd;


        private RewardedAd rewardedAd;
        private RewardedAd gameOverRewardedAd;
        private RewardedAd extraCoinsRewardedAd;


        // Start is called before the first frame update
        public static AdManager instance;

        string adUnitId;

        bool isRewarded = false;

        private void Awake() {
            if(instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
                return;
            }
        }

        void Start()
        {
            //same app key is enabled by default, but you can disable it with the following API:
            //RequestConfiguration requestConfiguration =
            //new RequestConfiguration.Builder().SetSameAppKeyEnabled(true).build();
            //MobileAds.SetRequestConfiguration(requestConfiguration);
            // Initialize the Google Mobile Ads SDK.
            //MobileAds.Initialize(HandleInitCompleteAction);

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(InitializationStatus => { });
            this.RequestBanner();

            //
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
            rewardedAd = new RewardedAd(adUnitId);
            this.gameOverRewardedAd = CreateAndLoadRewardedAd(adUnitId);
            this.extraCoinsRewardedAd = CreateAndLoadRewardedAd(adUnitId);

            // Called when an ad request has successfully loaded.
            this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            // Called when an ad request failed to load.
            this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
            // Called when an ad is shown.
            this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
            // Called when an ad request failed to show.
            this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            // Called when the user should be rewarded for interacting with the ad.
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            // Called when the ad is closed.
            this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            //// Create an empty ad request.
            //AdRequest request = new AdRequest.Builder().Build();
            // Implement below in CreateAsRequest function.
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(CreateAdRequest());

        }

        public RewardedAd CreateAndLoadRewardedAd(string adUnitId) {
            RewardedAd rewardedAd = new RewardedAd(adUnitId);

            rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
            rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
            rewardedAd.OnAdClosed += HandleRewardedAdClosed;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            rewardedAd.LoadAd(request);
            return rewardedAd;
        }

        private void RequestBanner() {
            string adUnitId = "ca-app-pub-3940256099942544/6300978111";
            this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);            
            this.bannerAd.LoadAd(CreateAdRequest());
        }

        #region Interstitial Ad 
        public void RequestInterstitial() {
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";

            // Clean up interstitial ad before creating a new one.
            if (this.interstitialAd != null)
                this.interstitialAd.Destroy();

            // Create an interstitial Ad
            this.interstitialAd = new InterstitialAd(adUnitId);

            // Load an interstitial ad.
            this.interstitialAd.LoadAd(this.CreateAdRequest());
        }

        public void ShowInterstitial() {
            if (this.interstitialAd.IsLoaded()) {
                interstitialAd.Show();
            } else {
                Debug.Log("Interstitial Ad is not ready.");
            }
        }

        #endregion

        #region Rewarded Ad      
        public void RequestRewardedAds() {
            string adUnitId = "ca - app - pub - 3940256099942544 / 5224354917";

            // Implement below in CreateAsRequest function.
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(CreateAdRequest());
        }

        public void ShowRewardedAds() {
            if (this.rewardedAd.IsLoaded()) {
                this.rewardedAd.Show();
            }
        }

        public void HandleRewardedAdLoaded(object sender, EventArgs args) {
            MonoBehaviour.print("HandleRewardedAdLoaded event received");
        }

        public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToLoad event received with message: "
                                 + args.LoadAdError.GetMessage());  // args.Message
        }

        public void HandleRewardedAdOpening(object sender, EventArgs args) {
            MonoBehaviour.print("HandleRewardedAdOpening event received");
        }

        public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args) {
            MonoBehaviour.print(
                "HandleRewardedAdFailedToShow event received with message: "
                                 + args.AdError.GetMessage());
        }

        public void HandleRewardedAdClosed(object sender, EventArgs args) {
            MonoBehaviour.print("HandleRewardedAdClosed event received");

            // When the ad is closed. Then request a new video.
           // RequestRewardedAds();

        }

        public void HandleUserEarnedReward(object sender, Reward args) {
            string type = args.Type;
            double amount = args.Amount;
            MonoBehaviour.print(
                "HandleRewardedAdRewarded event received for "
                            + amount.ToString() + " " + type);

            // If rewarded then make bool true.
            isRewarded = true;

        }


        #endregion

        private void Update() {
            if (isRewarded) {
                isRewarded = false;
                // Unlock features goes here.
            }
        }

        private AdRequest CreateAdRequest() {
            return new AdRequest.Builder().Build();
        }

       
    }
}
