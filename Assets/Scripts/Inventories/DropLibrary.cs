using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Inventories
{
    // 掉落库脚本，继承自ScriptableObject
    [CreateAssetMenu(menuName = ("RPG/Inventory/Drop Library"))]
    public class DropLibrary : ScriptableObject
    {
        [SerializeField]
        DropConfig[] potentialDrops;  // 潜在掉落物品数组
        [SerializeField] float[] dropChancePercentage;  // 掉落概率百分比数组
        [SerializeField] int[] minDrops;  // 最少掉落数量数组
        [SerializeField] int[] maxDrops;  // 最多掉落数量数组

        [System.Serializable]
        class DropConfig
        {
            public InventoryItem item;  // 掉落物品
            public float[] relativeChance;  // 相对概率数组
            public int[] minNumber;  // 最小数量数组
            public int[] maxNumber;  // 最大数量数组
            public int GetRandomNumber(int level)
            {
                if (!item.IsStackable())  // 如果物品不可堆叠，返回1
                {
                    return 1;
                }
                int min = GetByLevel(minNumber, level);  // 获取对应等级的最小数量
                int max = GetByLevel(maxNumber, level);  // 获取对应等级的最大数量
                return UnityEngine.Random.Range(min, max + 1);  // 在最小数量和最大数量之间随机选择一个数量返回
            }
        }

        public struct Dropped
        {
            public InventoryItem item;  // 掉落物品
            public int number;  // 数量
        }

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level))  // 根据等级判断是否应该随机掉落
            {
                yield break;
            }
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        bool ShouldRandomDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChancePercentage, level);  // 根据等级和掉落概率百分比判断是否应该进行随机掉落
        }

        int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minDrops, level);  // 获取对应等级的最小掉落数量
            int max = GetByLevel(maxDrops, level);  // 获取对应等级的最大掉落数量
            return Random.Range(min, max);  // 在最小数量和最大数量之间随机选择一个数量返回
        }

        Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);  // 随机选择一个掉落物品
            var result = new Dropped();
            result.item = drop.item;  // 设置掉落物品
            result.number = drop.GetRandomNumber(level);  // 设置掉落数量
            return result;
        }

        DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);  // 获取总概率
            float randomRoll = Random.Range(0, totalChance);  // 在0到总概率之间随机选择一个值
            float chanceTotal = 0;
            foreach (var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeChance, level);  // 获取对应等级的相对概率
                if (chanceTotal > randomRoll)  // 如果累计概率超过随机值，选择该掉落物品
                {
                    return drop;
                }
            }
            return null;
        }

        float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance, level);  // 获取对应等级的相对概率，并累加
            }
            return total;
        }

        static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length == 0)  // 如果数组长度为0，返回默认值
            {
                return default;
            }
            if (level > values.Length)  // 如果等级大于数组长度，返回数组最后一个元素
            {
                return values[values.Length - 1];
            }
            if (level <= 0)  // 如果等级小于等于0，返回默认值
            {
                return default;
            }
            return values[level - 1];  // 返回对应等级的元素
        }
    }
}