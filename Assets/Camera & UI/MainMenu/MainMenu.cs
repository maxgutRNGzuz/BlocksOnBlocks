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
    [SerializeField] TextMeshProUGUI highscoreText;
    int highscore;

    void Start()
    {        
        AuthenticatePlayer();
        HandleHighscore();
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
                return;
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
}
