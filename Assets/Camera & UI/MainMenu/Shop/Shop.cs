using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public List<ShopItem> playerItems = new List<ShopItem>();
    public List<int> purchasedPlayerItems = new List<int>();

    [SerializeField] TextMeshProUGUI coinsText;
    
    int coins;

    void Awake() {
        PlayerPrefs.SetInt("Coins", 5000);
    }

    void Start(){
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
        //LoadPlayer();
    }

    public void UpdateCoins(int amount){
        PlayerPrefs.SetInt("Coins", coins+amount);
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(){ //make private when not used as a button event
        PlayerData data = SaveSystem.LoadPlayer();
        purchasedPlayerItems = data.purchasedPlayerItems.ToList();
    }
}
