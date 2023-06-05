using System;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Abilities
{
    // ������ȴ�洢��
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<InventoryItem, float> cooldownTimers = new Dictionary<InventoryItem, float>(); // ������ȴ��ʱ���ֵ�
        Dictionary<InventoryItem, float> initialCooldownTimes = new Dictionary<InventoryItem, float>(); // ���ܳ�ʼ��ȴʱ���ֵ�

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

        // ��ʼ������ȴ
        public void StartCooldown(InventoryItem ability, float cooldownTime)
        {
            cooldownTimers[ability] = cooldownTime;
            initialCooldownTimes[ability] = cooldownTime;
        }

        // ��ȡ����ʣ����ȴʱ��
        public float GetTimeRemaining(InventoryItem ability)
        {
            if (!cooldownTimers.ContainsKey(ability))
            {
                return 0;
            }

            return cooldownTimers[ability];
        }

        // ��ȡ����ʣ����ȴʱ��İٷֱ�
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