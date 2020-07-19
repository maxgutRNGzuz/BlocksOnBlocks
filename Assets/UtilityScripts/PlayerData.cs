using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int[] purchasedPlayerItems;
    public int[] purchasedPlatformItems;
    public int[] purchasedBottomItems;

    public PlayerData(Shop shop){
        purchasedPlayerItems = shop.purchasedPlayerItems.ToArray();
        purchasedPlatformItems = shop.purchasedPlatformItems.ToArray();
        purchasedBottomItems = shop.purchasedBottomItems.ToArray();
    }
}
