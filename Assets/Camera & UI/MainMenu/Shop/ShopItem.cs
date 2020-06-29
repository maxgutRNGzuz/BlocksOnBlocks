using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour {

    public enum ButtonStates{notPurchased = 0, notSelected = 1, selected = 2};
    public ButtonStates currentState;
    public GameObject purchase;
    public GameObject select;
    public GameObject selected;
    public Material material;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerInstance;
    [SerializeField] Renderer deathFXRenderer;
    [SerializeField] Shop shop;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] string name;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] int cost;

    ShopItem[] shopItems;
    Renderer prefabRenderer;
    Renderer instanceRenderer;
    int coins;

    void Start() {
        shopItems = FindObjectsOfType<ShopItem>();
        prefabRenderer = playerPrefab.GetComponentInChildren<Renderer>();
        instanceRenderer = playerInstance.GetComponentInChildren<Renderer>();
        coins = PlayerPrefs.GetInt("Coins", 0);
        nameText.text = name;
        if(!PlayerItems.ShopItems.ContainsKey(name)){
            PlayerItems.ShopItems.Add(name, (int)currentState);
        }
        CheckButtonState();
        
    }

    void CheckButtonState(){
        // for (int i = 0; i < shopItems.Length; i++)
        // {
        //     if(shopItems[i].material == playerInstance.renderer){
        //         shopItems[i].currentState = ButtonStates.selected;

        //         if(purchase){
        //             purchase.SetActive(false);
        //         }
        //         select.SetActive(false);
        //         selected.SetActive(true);
        //     }
        // }
        if(instanceRenderer.material)
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
        PlayerItems.ShopItems[name] = (int)ButtonStates.selected;
    }

    void ChangeColor(){
        prefabRenderer.material = this.material;
        instanceRenderer.material = this.material;
        deathFXRenderer.material = this.material;
    }

    void UnselectOtherItems(){
        for (int i = 0; i < shopItems.Length; i++)
        {
            if(shopItems[i].currentState == ButtonStates.selected){
                shopItems[i].currentState = ButtonStates.notSelected;
                PlayerItems.ShopItems[shopItems[i].name] = (int)ButtonStates.notSelected;
                if(shopItems[i].purchase){
                    shopItems[i].purchase.SetActive(false);
                }
                shopItems[i].select.SetActive(true);
                shopItems[i].selected.SetActive(false);
            }

        }  
    }
}