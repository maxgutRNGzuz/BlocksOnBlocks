using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneHandler : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    PlayerUI playerUI;
    Animator anim;
    Camera cutsceneCam;

    void Start(){
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player){
            playerMovement = player.GetComponent<PlayerMovement>();
            playerUI = player.GetComponent<PlayerUI>();
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene(){
        playerUI.UpdateSpeedText(4f, false);
        playerUI.UpdatePhase(PlayerStats.Phase);
        playerUI.UpdateScore(PlayerStats.Score); //doppelt gemobbelt aber muss sein
        //playerMovement.score = PlayerStats.Score;
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        playerMovement.enabled = true;
    }

    public void PlayAgain(){
        playerUI.UpdatePhase(1);
        StartCoroutine(LoadFirstPhase());
    }

    public void FirstPhase(){
        StartCoroutine(LoadFirstPhase());
    }

    IEnumerator LoadFirstPhase(){
        anim.SetTrigger("FadeIn");
        PlayerStats.Phase = 1;
        PlayerStats.Score = 0;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void NextPhase(){
        StartCoroutine(LoadNextPhase());
    }

    IEnumerator LoadNextPhase(){
        cutsceneCam = GameObject.FindGameObjectWithTag("Cutscene Cam").GetComponent<Camera>();
        PlayerStats.Phase += 1;
        PlayerStats.Score = playerMovement.score;
        playerMovement.enabled = false;
        Camera.main.enabled = false;
        cutsceneCam.enabled = true;
        PlayableDirector[] cutscenes = FindObjectsOfType<PlayableDirector>();
        cutscenes[0].Play();
        cutscenes[1].Play();
        float duration = (float)cutscenes[0].duration;
        yield return new WaitForSeconds(duration-1f); // so the movement doe not clamp at the end
        anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        // anim.SetTrigger("FadeInLoadingScreen");
        // yield return new WaitForSeconds(1f);
        // AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        // while(!operation.isDone){
        //     float progress = Mathf.Clamp01(operation.progress/0.9f);
        //     print(progress);
        //     yield return null;
        // }
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void MainMenu(){
        StartCoroutine(LoadMainMenu());
    } 

    IEnumerator LoadMainMenu(){
        anim.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    public bool IsLastPhase(){
        if(PlayerStats.Phase == 3){ // last phase
            return true;
        }
        else{
            return false;
        }
    }

    public void LoadLastPhase(){
        SceneManager.LoadScene(3);
    }
}
