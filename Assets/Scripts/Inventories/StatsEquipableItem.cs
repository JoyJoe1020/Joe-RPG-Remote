using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    // 可装备物品的类，实现了IModifierProvider接口
    [CreateAssetMenu(menuName = ("RPG/Inventory/Equipable Item"))]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField]
        Modifier[] additiveModifiers;  // 加法修正器数组
        [SerializeField]
        Modifier[] percentageModifiers;  // 百分比修正器数组

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;  // 属性
            public float value;  // 修正值
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;  // 返回对应属性的加法修正值
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;  // 返回对应属性的百分比修正值
                }
            }
        }
    }
}