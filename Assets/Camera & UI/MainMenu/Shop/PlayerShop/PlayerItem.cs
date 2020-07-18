using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItem : MonoBehaviour {

    public enum ButtonStates{notPurchased = 0, notSelected = 1, selected = 2};
    public ButtonStates currentState;
    [SerializeField] GameObject purchase;
    [SerializeField] GameObject select;
    [SerializeField] GameObject selected;
    
    [SerializeField] Material myMaterial;
    [SerializeField] Material playerMaterial;

    [Header("StandartForAllItems")]
    [SerializeField] bool isDefault = false;
    [SerializeField] Shop shop;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] string name;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] int cost;

    int coins;
    int index;

    void Start() {
        nameText.text = name;
        if(!isDefault){
            costText.text = cost.ToString();
        }
        index = shop.playerItems.IndexOf(this);
        if(index == 0)
        print(index);

        if(isDefault && shop.purchasedPlayerItems.Contains(index) == false){
            shop.purchasedPlayerItems.Add(index);
        }
        CheckButtonState();  
    }

    void CheckButtonState(){
        if(shop.purchasedPlayerItems.Contains(index) == true){      
            if(myMaterial.color == playerMaterial.color){
                print("no");
                currentState = ButtonStates.selected;
                if(purchase){
                    purchase.SetActive(false);
                }
                select.SetActive(false);
                selected.SetActive(true);
            }
            else{
                currentState = ButtonStates.notSelected;
                if(purchase){
                    purchase.SetActive(false);
                }
                select.SetActive(true);
                selected.SetActive(false);
            }
        }
    }

    public void OnButtonPress(){
        if(currentState == ButtonStates.notPurchased){
            coins = PlayerPrefs.GetInt("Coins", 0);
            if(coins >= cost){
                PurchaseItem();
                SelectItem();
            }
            else{
                print("Not enough coins for this purchase");
            }
        }
        else if (currentState == ButtonStates.notSelected){
            SelectItem();
        }
        else if(currentState == ButtonStates.selected){
            print("Already selected");
        }
    }

    void PurchaseItem(){
        shop.UpdateCoins(-cost);
        shop.purchasedPlayerItems.Add(index);
        shop.SavePlayer();
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
        playerMaterial.color = myMaterial.color;
        playerMaterial.SetFloat("_Metallic", myMaterial.GetFloat("_Metallic"));
        playerMaterial.SetFloat("_Glossiness", myMaterial.GetFloat("_Glossiness"));
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