using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MainMenu : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI coinsText;

    int highscore;

    void Start()
    {      
        if(platform == null){
            AuthenticatePlayer();
        }
        HandleHighscore();
        HandleCoins();
    }

    void AuthenticatePlayer(){
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>{
            if(success){
                print("Logged in to Google Play Services");
            }
            else{
                Debug.LogError("Unable to sign in to Google Play Services");
                //return; !!!!!!!!!!TURN ON AGAIN WHEN BUILDING
            }}
        );
    }

    void HandleHighscore(){
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        if(PlayerStats.Score > highscore){
            highscore = PlayerStats.Score;
            PlayerPrefs.SetInt("Highscore", highscore);
            UploadToLeaderboard();
        }
        highscoreText.text = highscore.ToString("000 000 000");
    }

    void UploadToLeaderboard(){
        Social.ReportScore(highscore, GPGSIds.leaderboard_highscore, (bool success) => {
            if(success){
                print("Uploaded highscore to leaderboard");
            }
            else{
                print("Unable to upload highscore to leaderboard");
            }}
        );     
    }

    void ShowLeaderboard(){
        //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highscore);
        Social.ShowLeaderboardUI();
    }

    void HandleCoins(){
        int oldCoins = PlayerPrefs.GetInt("Coins", 0);
        int collectedCoins = PlayerStats.Coins;

        PlayerPrefs.SetInt("Coins", oldCoins + collectedCoins);
        coinsText.text = (oldCoins+collectedCoins).ToString();
    }
}
