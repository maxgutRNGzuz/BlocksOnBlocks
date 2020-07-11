using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItem : MonoBehaviour {

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

    MeshRenderer prefabRenderer;
    MeshRenderer instanceRenderer;
    int coins;
    int index;

    void Start() {
        prefabRenderer = playerPrefab.GetComponentInChildren<MeshRenderer>();
        instanceRenderer = playerInstance.GetComponentInChildren<MeshRenderer>();
        coins = PlayerPrefs.GetInt("Coins", 0);
        nameText.text = name;
        index = shop.playerItems.IndexOf(this);

        if(name == "Alpinewhite" && shop.purchasedPlayerItems.Contains(index) == false){
            shop.purchasedPlayerItems.Add(index);
        }
        CheckButtonState();  
    }

    void CheckButtonState(){
        if(shop.purchasedPlayerItems.Contains(index) == true){    
            print(index);     
            if(this.material.color == instanceRenderer.material.color){
                currentState = ButtonStates.selected;
                if(purchase){
                    purchase.SetActive(false);
                }
                select.SetActive(false);
                selected.SetActive(true);
            }
            else{
                if(shop.purchasedPlayerItems.Contains(index)){
                        currentState = ButtonStates.notSelected;
                        if(purchase){
                            purchase.SetActive(false);
                        }
                        select.SetActive(true);
                        selected.SetActive(false);
                }
            }
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
            shop.UpdateCoins(-cost);
            shop.purchasedPlayerItems.Add(index);
            shop.SavePlayer();
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
        foreach (PlayerItem item in shop.playerItems)
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