using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 延迟复合效果类
    [CreateAssetMenu(fileName = "Delay Composite Effect", menuName = "Abilities/Effects/Delay Composite", order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] float delay = 0; // 设定的延迟时间，单位为秒
        [SerializeField] EffectStrategy[] delayedEffects; // 需要延迟执行的效果数组
        [SerializeField] bool abortIfCancelled = false; // 是否设置为当效果被取消时就终止执行

        // 开始效果的方法，这是一个重写的方法，用于开始执行效果
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffect(data, finished)); // 开始协程，执行延迟效果
        }

        // 延迟效果的协程
        private IEnumerator DelayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay); // 等待设定的延迟时间
            if (abortIfCancelled && data.IsCancelled()) yield break; // 如果设置了终止且效果被取消，则跳出协程
            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished); // 启动每个延迟效果
            }
        }
    }
}
