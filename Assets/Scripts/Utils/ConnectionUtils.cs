using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GoogleMobileAds.Api;
using UnityEngine.SocialPlatforms;

public class ConnectionUtils {
	private static ConnectionUtils instance;

	private BannerView bannerView;
	private AdRequest bannerRequest;

	private InterstitialAd fullscreen;
	private AdRequest fullRequest;

	private const string ADMOB_BANNER = "ca-app-pub-8693762148432125/1454771899";
	private const string ADMOB_FULL = "ca-app-pub-8693762148432125/2931505094";
	private const string LEADERBOARD = "CgkI5KH84f0YEAIQBg";

	private ConnectionUtils(){}

	public static ConnectionUtils Instance {
		get{
			if(instance == null){
				instance = new ConnectionUtils();
			}

			return instance;
		}
	}

	public void InitBanner(){
		if(IsAndroid()){
			bannerView = new BannerView(ADMOB_BANNER, AdSize.Banner, AdPosition.Top);
			
			bannerRequest = new AdRequest.Builder().Build();

			bannerView.LoadAd(bannerRequest);

			HideBanner();
		}
	}

	public void InitInterstitial(){
		if(IsAndroid()){
			fullscreen = new InterstitialAd(ADMOB_FULL);
			
			fullRequest = new AdRequest.Builder().Build();

			fullscreen.LoadAd(fullRequest);
		}
	}

	public void ShowBanner(){
		if(IsAndroid()){
			bannerView.Show();
		}
	}

	public void HideBanner(){
		if(IsAndroid()){
			bannerView.Hide();
		}
	}

	public void ShowFullScreen(){
		if(IsAndroid()){
			int chance = Random.Range(0, 100);
			
			if(chance <= 50){
				fullscreen.Show();
			}
		}
	}

	public void ConnectToGoogleServices(){
		if(IsAndroid()){
			PlayGamesPlatform.Activate();

			Social.localUser.Authenticate((bool success) => {});
		}
	}

	public void ShareScoreToLeaderBoard(int score){
		if(IsAndroid()){
			Social.ReportScore(score, LEADERBOARD, (bool success) => {});
		}
	}

	public void ShowLeaderBoard(){
		if(IsAndroid() && Social.Active.localUser.authenticated){
			PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARD);
		}
	}

	private bool IsAndroid(){
		return Application.platform.Equals(RuntimePlatform.Android);
	}
}