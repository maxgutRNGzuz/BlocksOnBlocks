using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour {

    public enum ButtonStates{notPurchased = 0, notSelected = 1, selected = 2};
    public static ButtonStates currentState;
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

    Renderer prefabRenderer;
    Renderer instanceRenderer;
    [SerializeField] Renderer deathFXRenderer;
    int coins;

    void Start() {
        prefabRenderer = playerPrefab.GetComponentInChildren<Renderer>();
        instanceRenderer = playerInstance.GetComponentInChildren<Renderer>();
        coins = PlayerPrefs.GetInt("Coins", 0);
        nameText.text = name;
        if(PlayerItems.ShopItems[name] == null){
            PlayerItems.ShopItems.Add(name, (int)currentState);
        }
        CheckButtonState();
        
    }

    void CheckButtonState(){
        print(PlayerItems.ShopItems[name]);
        if(PlayerItems.ShopItems[name] == (int)ButtonStates.notPurchased){
            if(purchase){
                purchase.SetActive(true);
            }
            select.SetActive(false);
            selected.SetActive(false);
        }
        else if (PlayerItems.ShopItems[name] == (int)ButtonStates.notSelected){
            if(purchase){
                purchase.SetActive(false);
            }
            select.SetActive(true);
            selected.SetActive(false);
        }
        else if(PlayerItems.ShopItems[name] == (int)ButtonStates.selected){
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
        ChangeColor();
        UnselectOtherItems();

        if(purchase){
            purchase.SetActive(false);
        }
        select.SetActive(false);
        selected.SetActive(true);
        currentState = ButtonStates.selected;
    }

    void ChangeColor(){
        prefabRenderer.material = this.material;
        instanceRenderer.material = this.material;
        deathFXRenderer.material = this.material;
    }

    void UnselectOtherItems(){
        foreach (KeyValuePair <string, int> item in PlayerItems.ShopItems)
            {
                if(item.Value == (int)ButtonStates.selected){
                    item.Value = (int)ButtonStates.notSelected;
                    if(item.purchase){
                        item.purchase.SetActive(false);
                    }
                    item.select.SetActive(true);
                    item.selected.SetActive(false);
                }
            }   
    }
}