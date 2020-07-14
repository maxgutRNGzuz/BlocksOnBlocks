using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public List<PlayerItem> playerItems = new List<PlayerItem>();
    [HideInInspector] public List<int> purchasedPlayerItems = new List<int>();

    public List<PlatformItem> platformItems = new List<PlatformItem>();
    [HideInInspector] public List<int> purchasedPlatformItems = new List<int>();

    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] GameObject[] shopCategories;
    [SerializeField] GameObject[] borders;
    
    int coins;

    void Awake() {
        PlayerPrefs.SetInt("Coins", 5000);
    }

    void Start(){
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
        if(shopCategories.Length != borders.Length){
            Debug.LogError("shopCategories.Length != borders.Length");
        }
        //LoadPlayer();
    }

    public void UpdateCoins(int amount){
        PlayerPrefs.SetInt("Coins", coins+amount);
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }

    public void SelectShopCategory(int i){
        for (int n = 0; n < shopCategories.Length; n++)
        {
            if(n == i){
                shopCategories[n].SetActive(true);
                borders[n].SetActive(false);
            }
            else{
                shopCategories[n].SetActive(false);
                borders[n].SetActive(true);
            }
        }
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(){ //make private when not used as a button event
        PlayerData data = SaveSystem.LoadPlayer();
        purchasedPlayerItems = data.purchasedPlayerItems.ToList();
        purchasedPlatformItems = data.purchasedPlatformItems.ToList();
    }
}
