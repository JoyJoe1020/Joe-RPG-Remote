using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 延迟复合效果
    [CreateAssetMenu(fileName = "Delay Composite Effect", menuName = "Abilities/Effects/Delay Composite", order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] float delay = 0; // 延迟时间
        [SerializeField] EffectStrategy[] delayedEffects; // 延迟效果数组
        [SerializeField] bool abortIfCancelled = false; // 如果被取消则中止

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffect(data, finished));
        }

        private IEnumerator DelayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay); // 等待一段时间
            if (abortIfCancelled && data.IsCancelled()) yield break; // 如果被取消且中止标志为真则跳出循环
            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished); // 启动延迟效果
            }
        }
    }
}
