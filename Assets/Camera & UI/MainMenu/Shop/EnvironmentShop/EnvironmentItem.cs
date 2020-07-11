using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnvironmentItem : MonoBehaviour {

    public enum ButtonStates{notPurchased = 0, notSelected = 1, selected = 2};
    public ButtonStates currentState;
    public GameObject purchase;
    public GameObject select;
    public GameObject selected;
    public Material material;

    [SerializeField] Shop shop;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] string name;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] int cost;

    int coins;
    int index;

    void Start() {
        coins = PlayerPrefs.GetInt("Coins", 0);
        nameText.text = name;
        index = shop.environmentItems.IndexOf(this);

        if(name == "Alpinewhite" && shop.purchasedEnvironmentItems.Contains(index) == false){
            shop.purchasedEnvironmentItems.Add(index);
        }
        CheckButtonState();  
    }

    void CheckButtonState(){
        if(shop.purchasedEnvironmentItems.Contains(index) == true){    
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
                if(shop.purchasedEnvironmentItems.Contains(index)){
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
            shop.purchasedEnvironmentItems.Add(index);
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
    }

    void UnselectOtherItems(){
        foreach (EnvironmentItem item in shop.environmentItems)
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