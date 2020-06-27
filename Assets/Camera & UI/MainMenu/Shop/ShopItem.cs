using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour {

    public enum ButtonStates{notPurchased, notSelected, selected};
    public ButtonStates currentState;
    public GameObject purchase;
    public GameObject select;
    public GameObject selected;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerInstance;
    [SerializeField] Shop shop;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] string name;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] int cost;
    [SerializeField] Material material;

    ShopItem[] shopItems;
    Renderer prefabRenderer;
    Renderer instanceRenderer;
    [SerializeField] Renderer deathFXRenderer;
    int coins;

    void Start() {
        shopItems = FindObjectsOfType<ShopItem>();
        prefabRenderer = playerPrefab.GetComponentInChildren<Renderer>();
        instanceRenderer = playerInstance.GetComponentInChildren<Renderer>();
        coins = PlayerPrefs.GetInt("Coins", 0);
        nameText.text = name;
        CheckButtonState();
    }

    void CheckButtonState(){
        if(PlayerPrefs.GetInt("ButtonState") == 0){
            purchase.SetActive(true);
            select.SetActive(false);
            selected.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("ButtonState") == 1){
            if(purchase){
                purchase.SetActive(false);
            }
            select.SetActive(true);
            selected.SetActive(false);
        }
        else if(PlayerPrefs.GetInt("ButtonState") == 2){
            if(purchase){
                purchase.SetActive(false);
            }
            select.SetActive(false);
            selected.SetActive(true);
        }
    }

    public void OnButtonPress(){
        if(currentState == ButtonStates.notPurchased){
            PurchaseItem();
            SelectItem();
        }
        else if (currentState == ButtonStates.notSelected){
            SelectItem();
        }
        else if(currentState == ButtonStates.selected){
            print("Already selected");
        }
    }

    void PurchaseItem(){
        if(coins >= cost){
            coins -= cost;
            PlayerPrefs.SetInt("Coins", coins);
            shop.UpdateCoins();
        }
        else{
            print("Not enough coins for this purchase");
        }
    }

    void SelectItem(){
        prefabRenderer.material = this.material;
        instanceRenderer.material = this.material;
        deathFXRenderer.material = this.material;
        foreach (ShopItem item in shopItems)
            {
                if(item.currentState == ButtonStates.selected){
                    item.currentState = ButtonStates.notSelected;
                    if(item.purchase){
                        item.purchase.SetActive(false);
                    }
                    item.select.SetActive(true);
                    item.selected.SetActive(false);
                }
            }
        if(purchase){
            purchase.SetActive(false);
        }
        select.SetActive(false);
        selected.SetActive(true);
        currentState = ButtonStates.selected;
    }
}