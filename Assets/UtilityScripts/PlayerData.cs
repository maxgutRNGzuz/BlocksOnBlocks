using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //public List<int> purchasedPlayerItems = new List<int>();
    public int[] purchasedPlayerItems;

    public PlayerData(Shop shop){
        purchasedPlayerItems = shop.purchasedPlayerItems.ToArray();
    }
}
