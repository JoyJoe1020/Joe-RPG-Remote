using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Shops
{
    // 购物者类
    public class Shopper : MonoBehaviour
    {
        // 当前活跃商店
        Shop activeShop = null;

        // 活跃商店更改事件
        public event Action activeShopChange;

        // 设置当前活跃商店
        public void SetActiveShop(Shop shop)
        {
            // 如果存在活跃商店，将购物者设置为null
            if (activeShop != null)
            {
                activeShop.SetShopper(null);
            }
            // 设置新的活跃商店
            activeShop = shop;
            // 如果活跃商店存在，将购物者设置为当前实例
            if (activeShop != null)
            {
                activeShop.SetShopper(this);
            }
            // 触发活跃商店更改事件
            if (activeShopChange != null)
            {
                activeShopChange();
            }
        }

        // 获取当前活跃商店
        public Shop GetActiveShop()
        {
            return activeShop;
        }
    }
}
