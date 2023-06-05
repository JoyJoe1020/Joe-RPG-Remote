using System;
using GameDevTV.Inventories;
using GameDevTV.Saving;
using UnityEngine;

namespace RPG.Inventories
{
    // 钱袋类，实现了ISaveable和IItemStore接口
    public class Purse : MonoBehaviour, ISaveable, IItemStore
    {
        [SerializeField] float startingBalance = 400f;  // 初始余额

        float balance = 0;  // 当前余额

        public event Action onChange;

        private void Awake()
        {
            balance = startingBalance;  // 设置当前余额为初始余额
        }

        public float GetBalance()
        {
            return balance;  // 返回当前余额
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;  // 更新当前余额
            if (onChange != null)
            {
                onChange();  // 触发余额变化事件
            }
        }

        public object CaptureState()
        {
            return balance;  // 返回当前余额作为状态
        }

        public void RestoreState(object state)
        {
            balance = (float)state;  // 恢复当前余额为状态值
        }

        public int AddItems(InventoryItem item, int number)
        {
            if (item is CurrencyItem)  // 如果物品是货币物品
            {
                UpdateBalance(item.GetPrice() * number);  // 根据物品价格和数量更新当前余额
                return number;  // 返回数量
            }
            return 0;  // 返回0表示添加物品失败
        }
    }
}