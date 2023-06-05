using System;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities
{
    // 技能冷却存储类
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<InventoryItem, float> cooldownTimers = new Dictionary<InventoryItem, float>(); // 技能冷却计时器字典
        Dictionary<InventoryItem, float> initialCooldownTimes = new Dictionary<InventoryItem, float>(); // 技能初始冷却时间字典

        void Update()
        {
            var keys = new List<InventoryItem>(cooldownTimers.Keys);
            foreach (InventoryItem ability in keys)
            {
                cooldownTimers[ability] -= Time.deltaTime;
                if (cooldownTimers[ability] < 0)
                {
                    cooldownTimers.Remove(ability);
                    initialCooldownTimes.Remove(ability);
                }
            }
        }

        // 开始技能冷却
        public void StartCooldown(InventoryItem ability, float cooldownTime)
        {
            cooldownTimers[ability] = cooldownTime;
            initialCooldownTimes[ability] = cooldownTime;
        }

        // 获取技能剩余冷却时间
        public float GetTimeRemaining(InventoryItem ability)
        {
            if (!cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return cooldownTimers[ability];
        }

        // 获取技能剩余冷却时间的百分比
        public float GetFractionRemaining(InventoryItem ability)
        {
            if (ability == null)
            {
                return 0;
            }

            if (!cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return cooldownTimers[ability] / initialCooldownTimes[ability];
        }
    }
}