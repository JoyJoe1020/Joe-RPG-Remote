using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 触发动画效果
    [CreateAssetMenu(fileName = "Trigger Animation Effect", menuName = "Abilities/Effects/Trigger Animation", order = 0)]
    public class TriggerAnimationEffect : EffectStrategy
    {
        [SerializeField] string animationTrigger; // 动画触发器名称

        public override void StartEffect(AbilityData data, Action finished)
        {
            Animator animator = data.GetUser().GetComponent<Animator>(); // 获取使用者的动画器组件
            animator.SetTrigger(animationTrigger); // 设置触发动画触发器
            finished();
        }
    }
}
