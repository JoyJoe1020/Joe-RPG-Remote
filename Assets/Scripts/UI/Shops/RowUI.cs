using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class RowUI : MonoBehaviour
    {
        [SerializeField]
        Image iconField;  // 图标
        [SerializeField]
        TextMeshProUGUI nameField;  // 名称
        [SerializeField]
        TextMeshProUGUI availabilityField;  // 可用性
        [SerializeField]
        TextMeshProUGUI priceField;  // 价格
        [SerializeField]
        TextMeshProUGUI quantityField;  // 数量

        Shop currentShop = null;  // 当前商店
        ShopItem item = null;  // 商品

        public void Setup(Shop currentShop, ShopItem item)
        {
            this.currentShop = currentShop;
            this.item = item;
            iconField.sprite = item.GetIcon();  // 设置图标为商品的图标
            nameField.text = item.GetName();  // 设置名称为商品的名称
            availabilityField.text = $"{item.GetAvailability()}";  // 设置可用性为商品的可用性
            priceField.text = $"${item.GetPrice():N2}";  // 设置价格为商品的价格
            quantityField.text = $"{item.GetQuantityInTransaction()}";  // 设置数量为商品的交易数量
        }

        public void Add()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), 1);  // 将商品添加到交易中
        }

        public void Remove()
        {
            currentShop.AddToTransaction(item.GetInventoryItem(), -1);  // 从交易中移除商品
        }
    }
}