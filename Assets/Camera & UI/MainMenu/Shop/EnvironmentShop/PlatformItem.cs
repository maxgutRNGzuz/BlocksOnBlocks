using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlatformItem : MonoBehaviour {

    public enum ButtonStates{notPurchased = 0, notSelected = 1, selected = 2};
    public ButtonStates currentState;
    public GameObject purchase;
    public GameObject select;
    public GameObject selected;

    [Header("Materials")]
    string[] keys = new string[] {"Ground", "Lanes", "Obstacle", "Arrow", "Stopper", "EndGrid", "EndArrow"};
    [SerializeField] Material[] _myMaterials;
    public Dictionary<string, Material> myMaterials = new Dictionary<string, Material>();
    [SerializeField] Material[] _platformMaterials;
    public Dictionary<string, Material> platformMaterials = new Dictionary<string, Material>();
    [SerializeField] Texture endGridTexture;

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
        index = shop.platformItems.IndexOf(this);

        if(isDefault && shop.purchasedPlatformItems.Contains(index) == false){
            shop.purchasedPlatformItems.Add(index);
        }
        //CheckButtonState();  
        SetUpDictionary();
    }

    void SetUpDictionary(){
        if(keys.Length != _myMaterials.Length || keys.Length != _platformMaterials.Length){
            Debug.LogError("keys.Length != _myMaterials.Length || keys.Length != _platformMaterials.Length");
            return;
        }
        for (int i = 0; i < _myMaterials.Length; i++)
        {
            myMaterials.Add(keys[i], _myMaterials[i]);
            platformMaterials.Add(keys[i], _platformMaterials[i]);
        }
    }

    void CheckButtonState(){
        if(shop.purchasedPlatformItems.Contains(index) == true){    
            // if(this.material.color == instanceRenderer.material.color){
            //     currentState = ButtonStates.selected;
            //     if(purchase){
            //         purchase.SetActive(false);
            //     }
            //     select.SetActive(false);
            //     selected.SetActive(true);
            // }
            if(index == -1){

            }
            else{
                if(shop.purchasedPlatformItems.Contains(index)){
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
        shop.purchasedPlatformItems.Add(index);
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
        foreach (string key in keys)
        {
            if(key != "EndGrid"){
                platformMaterials[key].color = myMaterials[key].color;
            }
            else{
                platformMaterials[key].SetTexture("_MainTex", endGridTexture);
            }
            platformMaterials[key].SetFloat("_Metallic", myMaterials[key].GetFloat("_Metallic"));
            platformMaterials[key].SetFloat("_Glossiness", myMaterials[key].GetFloat("_Glossiness"));
        }
    }

    void UnselectOtherItems(){
        foreach (PlatformItem item in shop.platformItems)
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