using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public bool isTutorial = true;

    [SerializeField] Animator playerCanvasAnim;
    [SerializeField] ParticleSystem warpSpeed;

    [Header("Normal UI")]
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI phaseText;
    [SerializeField] TextMeshProUGUI scoreText;
    //[SerializeField] TextMeshProUGUI moneyText;

    [Header("Tutorial")]
    [SerializeField] TextMeshProUGUI swipeText;
    [SerializeField] string[] tutorialDisplayText;

    [Header("Dead")]
    [SerializeField] TextMeshProUGUI deathScoreText;

    [Header("Pause")]
    [SerializeField] TextMeshProUGUI continueCountdown;
    [SerializeField] Button pauseButton;
    [SerializeField] TextMeshProUGUI pauseScoreText;

    PlayerMovement playerMovement;
    Rigidbody rb;
    int tutorialIndex;
    //int moneyCurrentRound;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        if(PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) > 1){
            isTutorial = false;
        }
        else if(isTutorial){
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 1);
            pauseButton.gameObject.SetActive(false);
            swipeText.gameObject.SetActive(true);
            playerCanvasAnim.SetBool("isBlinking", true);
        }
    }

    // void OnCollisionEnter(Collision collider) {
    //     if(collider.gameObject.tag == "Coin"){
    //         collider.gameObject.SetActive(false);
    //         moneyCurrentRound += 1;
    //         moneyText.text = moneyCurrentRound.ToString();
    //     }      
    // }

    void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.tag == "UpdateTutorial"){
            swipeText.text = tutorialDisplayText[tutorialIndex];
            tutorialIndex++;
            if(swipeText.text == "good"){
                playerCanvasAnim.SetBool("isBlinking", false);
            }
            else{
                playerCanvasAnim.SetBool("isBlinking", true);
            }
        }
        else if(collider.gameObject.tag == "TutorialEnd"){
            isTutorial = false;
            swipeText.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 100);
        }
    }

    public void UpdateSpeedText(float speed, bool isMaxSpeed){
        if(isMaxSpeed){
            speedText.text = "speed max";
            warpSpeed.Play();
        }
        else{
            speedText.text = "speed " + ((speed/4) * 100f).ToString();
        }
    }

    public void FadeInDeadScreen(){
        playerCanvasAnim.SetTrigger("Die");
        deathScoreText.text = PlayerStats.Score.ToString("000 000 000");
    }

    public void Pause(){
        PlayerStats.Score = playerMovement.score;
        pauseScoreText.text = PlayerStats.Score.ToString("000 000 000");
        rb.isKinematic = true;
        playerMovement.enabled = false;
        pauseButton.gameObject.SetActive(false);
        playerMovement.PauseAnim();
    }

    public void Continue(){
        StartCoroutine(ContinueCountdown());
    }

    IEnumerator ContinueCountdown(){
        int i = 3;
        while(i>0){
            continueCountdown.enabled = true;
            continueCountdown.text = i.ToString();
            yield return new WaitForSeconds(1f);
            i--;
        }
        rb.isKinematic = false;
        continueCountdown.enabled = false;
        playerMovement.enabled = true;
        pauseButton.gameObject.SetActive(true);
        playerMovement.ContinueAnim();
    }

    public void UpdatePhase(int phase){
        phaseText.text = "Phase " + phase.ToString();
    }

    public void UpdateScore(int score){
        scoreText.text = score.ToString("000 000 000");
    }
}
