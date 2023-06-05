using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Attributes;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities
{
    // 技能类，继承自ActionItem（游戏中的行动物品）
    [CreateAssetMenu(fileName = "My Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy; // 目标选择策略
        [SerializeField] FilterStrategy[] filterStrategies; // 过滤策略数组
        [SerializeField] EffectStrategy[] effectStrategies; // 效果策略数组
        [SerializeField] float cooldownTime = 0; // 冷却时间
        [SerializeField] float manaCost = 0; // 消耗的法力值

        // 使用技能
        public override bool Use(GameObject user)
        {
            Mana mana = user.GetComponent<Mana>(); // 获取使用技能的游戏对象的Mana组件
            if (mana.GetMana() < manaCost) // 如果法力值不足以使用技能
            {
                return false; // 返回false，表示使用失败
            }

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>(); // 获取使用技能的游戏对象的CooldownStore组件
            if (cooldownStore.GetTimeRemaining(this) > 0) // 如果技能还在冷却中
            {
                return false; // 返回false，表示使用失败
            }

            AbilityData data = new AbilityData(user); // 创建一个新的AbilityData对象，传入使用技能的游戏对象

            ActionScheduler actionScheduler = user.GetComponent<ActionScheduler>(); // 获取使用技能的游戏对象的ActionScheduler组件
            actionScheduler.StartAction(data); // 启动ActionScheduler的动作，传入AbilityData对象

            // 开始目标选择，传入AbilityData对象和目标选择完成后的回调方法
            targetingStrategy.StartTargeting(data,
                () =>
                {
                    TargetAquired(data);
                });

            return true; // 返回true，表示使用成功
        }

        // 目标选择完成后的回调方法
        private void TargetAquired(AbilityData data)
        {
            if (data.IsCancelled()) return; // 如果目标选择被取消，则直接返回

            Mana mana = data.GetUser().GetComponent<Mana>(); // 获取使用技能的游戏对象的Mana组件
            if (!mana.UseMana(manaCost)) return; // 如果法力值不足以使用技能，直接返回

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>(); // 获取使用技能的游戏对象的CooldownStore组件
            cooldownStore.StartCooldown(this, cooldownTime); // 开始技能冷却

            foreach (var filterStrategy in filterStrategies)
            {
                // 对目标进行过滤，并更新AbilityData中的目标列表
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished); // 开始效果，并传入AbilityData对象和效果完成后的回调方法
            }
        }

        private void EffectFinished()
        {

        }
    }
}