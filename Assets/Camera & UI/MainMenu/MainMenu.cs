using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;
    int highscore;

    void Start()
    {        
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        if(PlayerStats.Score > highscore){
            highscore = PlayerStats.Score;
            PlayerPrefs.SetInt("Highscore", highscore);
        }
        highscoreText.text = highscore.ToString("000 000 000");
        Leaderboard.UploadToLeaderboard(highscore);
    }

    public void ShowLeaderboard(){
        Leaderboard.ShowLeaderboard(); //needs to be done via script that is attached to a GO, because button event wont just take a script
    }
}
