using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    // 装备栏的类，实现了IModifierProvider接口
    public class StatsEquipment : Equipment, IModifierProvider
    {
        IEnumerable<float> IModifierProvider.GetAdditiveModifiers(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;  // 获取装备槽中的物品，并转为IModifierProvider接口类型
                if (item == null) continue;  // 如果物品为空，则继续循环

                foreach (float modifier in item.GetAdditiveModifiers(stat))
                {
                    yield return modifier;  // 返回对应属性的加法修正值
                }
            }
        }

        IEnumerable<float> IModifierProvider.GetPercentageModifiers(Stat stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;  // 获取装备槽中的物品，并转为IModifierProvider接口类型
                if (item == null) continue;  // 如果物品为空，则继续循环

                foreach (float modifier in item.GetPercentageModifiers(stat))
                {
                    yield return modifier;  // 返回对应属性的百分比修正值
                }
            }
        }

    }
}