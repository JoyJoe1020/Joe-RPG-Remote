using System;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 生命值效果类
    [CreateAssetMenu(fileName = "Health Effect", menuName = "Abilities/Effects/Health", order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float healthChange; // 生命值变化量，可以是正值或负值

        // 开始效果的方法，这是一个重写的方法，用于开始执行效果
        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.GetTargets())
            {
                var health = target.GetComponent<Health>(); // 从目标对象中获取Health组件
                if (health) // 如果存在Health组件
                {
                    if (healthChange < 0) // 如果生命值变化量小于0，表示扣血
                    {
                        health.TakeDamage(data.GetUser(), -healthChange); // 对目标进行伤害
                    }
                    else // 如果生命值变化量大于0，表示恢复生命值
                    {
                        health.Heal(healthChange); // 对目标进行治疗
                    }
                }
            }
            finished(); // 执行完成
        }
    }
}
