using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;

    int coins;

    ShopItem[] shopItems;

    void Awake() {
        PlayerPrefs.SetInt("Coins", 5000);
    }

    void Start(){
        shopItems = GetComponentsInChildren<ShopItem>();
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }

    public void UpdateCoins(){
        coins = PlayerPrefs.GetInt("Coins", 0);
        coinsText.text = coins.ToString();
    }
}
