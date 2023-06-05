using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // �ӳٸ���Ч��
    [CreateAssetMenu(fileName = "Delay Composite Effect", menuName = "Abilities/Effects/Delay Composite", order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] float delay = 0; // �ӳ�ʱ��
        [SerializeField] EffectStrategy[] delayedEffects; // �ӳ�Ч������
        [SerializeField] bool abortIfCancelled = false; // �����ȡ������ֹ

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffect(data, finished));
        }

        private IEnumerator DelayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay); // �ȴ�һ��ʱ��
            if (abortIfCancelled && data.IsCancelled()) yield break; // �����ȡ������ֹ��־Ϊ��������ѭ��
            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished); // �����ӳ�Ч��
            }
        }
    }
}
