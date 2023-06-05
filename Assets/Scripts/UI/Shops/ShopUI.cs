using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI shopName;  // 商店名称
        [SerializeField] Transform listRoot;  // 列表根节点
        [SerializeField] RowUI rowPrefab;  // 行预制体
        [SerializeField] TextMeshProUGUI totalField;  // 总价文本
        [SerializeField] Button confirmButton;  // 确认按钮
        [SerializeField] Button switchButton;  // 切换按钮

        Shopper shopper = null;  // 购物者
        Shop currentShop = null;  // 当前商店

        Color originalTotalTextColor;

        // Start is called before the first frame update
        void Start()
        {
            originalTotalTextColor = totalField.color;  // 记录原始的总价文本颜色

            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();  // 获取购物者组件
            if (shopper == null) return;

            shopper.activeShopChange += ShopChanged;  // 注册购物者的活动商店改变事件
            confirmButton.onClick.AddListener(ConfirmTransaction);  // 注册确认按钮的点击事件
            switchButton.onClick.AddListener(SwitchMode);  // 注册切换按钮的点击事件

            ShopChanged();  // 商店改变时进行UI刷新
        }

        private void ShopChanged()
        {
            if (currentShop != null)
            {
                currentShop.onChange -= RefreshUI;  // 取消之前商店的刷新事件
            }
            currentShop = shopper.GetActiveShop();  // 获取购物者的活动商店
            gameObject.SetActive(currentShop != null);  // 设置UI对象的活跃状态为是否有活动商店

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.SetShop(currentShop);  // 设置过滤按钮的商店
            }

            if (currentShop == null) return;
            shopName.text = currentShop.GetShopName();  // 设置商店名称为当前商店的名称

            currentShop.onChange += RefreshUI;  // 注册商店刷新事件

            RefreshUI();  // 刷新商店UI
        }

        private void RefreshUI()
        {
            foreach (Transform child in listRoot)
            {
                Destroy(child.gameObject);  // 清空列表根节点下的子物体
            }

            foreach (ShopItem item in currentShop.GetFilteredItems())  // 遍历当前商店的过滤后的商品
            {
                RowUI row = Instantiate<RowUI>(rowPrefab, listRoot);  // 实例化行UI，并将其设置为列表根节点的子物体
                row.Setup(currentShop, item);  // 设置行UI的信息
            }

            totalField.text = $"Total: ${currentShop.TransactionTotal():N2}";  // 设置总价文本为当前交易的总价
            totalField.color = currentShop.HasSufficientFunds() ? originalTotalTextColor : Color.red;  // 根据商店的资金是否足够设置总价文本的颜色
            confirmButton.interactable = currentShop.CanTransact();  // 根据商店是否可以交易设置确认按钮的可交互性
            TextMeshProUGUI switchText = switchButton.GetComponentInChildren<TextMeshProUGUI>();  // 获取切换按钮子物体中的文本组件
            TextMeshProUGUI confirmText = confirmButton.GetComponentInChildren<TextMeshProUGUI>();  // 获取确认按钮子物体中的文本组件
            if (currentShop.IsBuyingMode())
            {
                switchText.text = "Switch To Selling";  // 如果当前为购买模式，将切换按钮的文本设置为"Switch To Selling"
                confirmText.text = "Buy";  // 将确认按钮的文本设置为"Buy"
            }
            else
            {
                switchText.text = "Switch To Buying";  // 如果当前为出售模式，将切换按钮的文本设置为"Switch To Buying"
                confirmText.text = "Sell";  // 将确认按钮的文本设置为"Sell"
            }

            foreach (FilterButtonUI button in GetComponentsInChildren<FilterButtonUI>())
            {
                button.RefreshUI();  // 刷新过滤按钮的UI
            }
        }

        public void Close()
        {
            shopper.SetActiveShop(null);  // 关闭商店
        }

        public void ConfirmTransaction()
        {
            currentShop.ConfirmTransaction();  // 确认交易
        }

        public void SwitchMode()
        {
            currentShop.SelectMode(!currentShop.IsBuyingMode());  // 切换商店模式
        }
    }
}