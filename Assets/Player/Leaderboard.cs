using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour
{
  
    void Start()
    {
        AuthenticatePlayer();
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
            }}
        );
    }

    public static void UploadToLeaderboard(int highscore){
        Social.ReportScore(highscore, GPGSIds.leaderboard_highscore, (bool success) => {
            if(success){
                print("Uploaded highscore to leaderboard");
            }
            else{
                Debug.LogError("Unable to upload highscore to leaderboard");
            }}
        );     
    }

    public static void ShowLeaderboard(){
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_highscore);
    }
}
