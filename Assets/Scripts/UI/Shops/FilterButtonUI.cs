using System;
using GameDevTV.Inventories;
using RPG.Shops;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Shops
{
    public class FilterButtonUI : MonoBehaviour
    {
        [SerializeField] ItemCategory category = ItemCategory.None;  // 物品类别

        Button button;
        Shop currentShop;

        private void Awake()
        {
            button = GetComponent<Button>();  // 获取按钮组件
            button.onClick.AddListener(SelectFilter);  // 注册按钮点击事件
        }

        public void SetShop(Shop currentShop)
        {
            this.currentShop = currentShop;  // 设置当前商店
        }

        public void RefreshUI()
        {
            button.interactable = currentShop.GetFilter() != category;  // 根据当前商店的过滤器设置按钮的可交互性
        }

        private void SelectFilter()
        {
            currentShop.SelectFilter(category);  // 选择过滤器
        }
    }
}