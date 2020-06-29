using System.Collections.Generic;

public static class PlayerItems
{
    //static List<ShopItem> shopItems = new List<ShopItem>();
    static Dictionary<string, int> shopItems = new Dictionary<string, int>();

    public static Dictionary<string, int> ShopItems{
        get{
            return shopItems;
        }
        set{
            shopItems = value;
        }
    }

}
