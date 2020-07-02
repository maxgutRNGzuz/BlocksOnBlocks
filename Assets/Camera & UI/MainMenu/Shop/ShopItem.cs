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

         if(name == "Alpinewhite" && PlayerItems.PurchasedItems.Contains(this) == false){
             PlayerItems.PurchasedItems.Add(this);
             print(PlayerItems.PurchasedItems.IndexOf(this));
         }
        CheckButtonState();     
    }

    void CheckButtonState(){
        if(PlayerItems.PurchasedItems.Contains(this) == false){
            return;
        }
        print(instanceRenderer.material);
        int i = PlayerItems.PurchasedItems.IndexOf(this);
        if(this.material.color == instanceRenderer.material.color){
            PlayerItems.PurchasedItems[i].currentState = ButtonStates.selected;
            if(purchase){
                purchase.SetActive(false);
            }
            select.SetActive(false);
            selected.SetActive(true);
        }
        else{
            PlayerItems.PurchasedItems[i].currentState = ButtonStates.notSelected;
            print(PlayerItems.PurchasedItems[i].gameObject.name);
            if(purchase){
                purchase.SetActive(false);
            }
            select.SetActive(true);
            selected.SetActive(false);
        }
        
        // foreach (ShopItem item in PlayerItems.PurchasedItems)
        // {
        //     if(item.material == material)
        //     if(item.material == instanceRenderer.material){
        //         item.currentState = ButtonStates.selected;
        //         if(purchase){
        //             purchase.SetActive(false);
        //         }
        //         select.SetActive(false);
        //         selected.SetActive(true);
        //     }
        //     else{
        //         item.currentState = ButtonStates.notSelected;
        //         print(item.gameObject.name);
        //         if(purchase){
        //             purchase.SetActive(false);
        //         }
        //         select.SetActive(true);
        //         selected.SetActive(false);
        //     }
        // }
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
            PlayerItems.PurchasedItems.Add(this);
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
        foreach (ShopItem item in PlayerItems.PurchasedItems)
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
    }
}