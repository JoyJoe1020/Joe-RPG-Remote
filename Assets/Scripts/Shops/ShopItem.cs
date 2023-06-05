using System;
using GameDevTV.Inventories;
using UnityEngine;

// RPG.Shops命名空间
namespace RPG.Shops
{
    // 商店物品类
    public class ShopItem
    {
        // 物品
        InventoryItem item;
        // 可用数量
        int availability;
        // 价格
        float price;
        // 交易数量
        int quantityInTransaction;

        // 构造函数
        public ShopItem(InventoryItem item, int availability, float price, int quantityInTransaction)
        {
            // 初始化成员变量
            this.item = item;
            this.availability = availability;
            this.price = price;
            this.quantityInTransaction = quantityInTransaction;
        }

        // 获取物品图标
        public Sprite GetIcon()
        {
            return item.GetIcon();
        }

        // 获取可用数量
        public int GetAvailability()
        {
            return availability;
        }

        // 获取物品名
        public string GetName()
        {
            return item.GetDisplayName();
        }

        // 获取价格
        public float GetPrice()
        {
            return price;
        }

        // 获取库存物品
        public InventoryItem GetInventoryItem()
        {
            return item;
        }

        // 获取交易数量
        public int GetQuantityInTransaction()
        {
            return quantityInTransaction;
        }
    }
}
