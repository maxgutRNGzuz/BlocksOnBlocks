using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomItem : MonoBehaviour {

    public enum ButtonStates{notPurchased = 0, notSelected = 1, selected = 2};
    public ButtonStates currentState;
    [SerializeField] GameObject purchase;
    [SerializeField] GameObject select;
    [SerializeField] GameObject selected;

    [SerializeField] Material myMaterial;
    [SerializeField] Material bottomMaterial;

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
        index = shop.bottomItems.IndexOf(this);

        myMaterial.EnableKeyword("_METALLICGLOSSMAP");
        myMaterial.EnableKeyword("_NORMALMAP");
        myMaterial.EnableKeyword("_PARALLAXMAP");
        bottomMaterial.EnableKeyword("_METALLICGLOSSMAP");
        bottomMaterial.EnableKeyword("_NORMALMAP");
        bottomMaterial.EnableKeyword("_PARALLAXMAP");


        if(isDefault && shop.purchasedBottomItems.Contains(index) == false){
            shop.purchasedBottomItems.Add(index);
        }
        CheckButtonState();  
    }

    void CheckButtonState(){
        if(shop.purchasedPlatformItems.Contains(index) == true){   
            if(myMaterial.GetTexture("_MainTex") == bottomMaterial.GetTexture("_MainTex")){ // just checks albedo texture
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
        bottomMaterial.color = myMaterial.color;
        bottomMaterial.SetTexture("_MainTex", myMaterial.GetTexture("_MainTex"));
        if(myMaterial.GetTexture("_MetallicGlossMap")){
            bottomMaterial.SetTexture("_MetallicGlossMap", myMaterial.GetTexture("_MetallicGlossMap"));
        }
        else{
            bottomMaterial.SetTexture("_MetallicGlossMap", null);
            bottomMaterial.SetFloat("_Metallic", myMaterial.GetFloat("_Metallic"));
        }
        bottomMaterial.SetFloat("_Glossiness", myMaterial.GetFloat("_Glossiness"));

        if(myMaterial.GetTexture("_BumpMap")){
            bottomMaterial.SetTexture("_BumpMap", myMaterial.GetTexture("_BumpMap"));
            //bottomMaterial.SetTexture("_HeightMap", myMaterial.GetTexture("_HeightMap")); !! cannot set height , LR or HD render pipline required
            //bottomMaterial.SetFloat("_Height", myMaterial.GetFloat("_Height")); !! cannot set height nor occlusion slider
            bottomMaterial.SetTexture("_OcclusionMap", myMaterial.GetTexture("_OcclusionMap"));
        }
        else{
            bottomMaterial.SetTexture("_BumpMap", null);
            //bottomMaterial.SetTexture("_HeightMap", null);
            bottomMaterial.SetTexture("_OcclusionMap", null);
        }
        bottomMaterial.SetTextureScale("_MainTex", myMaterial.GetTextureScale("_MainTex"));
    }

    void UnselectOtherItems(){
        foreach (BottomItem item in shop.bottomItems)
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