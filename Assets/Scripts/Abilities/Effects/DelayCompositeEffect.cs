using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // �ӳٸ���Ч����
    [CreateAssetMenu(fileName = "Delay Composite Effect", menuName = "Abilities/Effects/Delay Composite", order = 0)]
    public class DelayCompositeEffect : EffectStrategy
    {
        [SerializeField] float delay = 0; // �趨���ӳ�ʱ�䣬��λΪ��
        [SerializeField] EffectStrategy[] delayedEffects; // ��Ҫ�ӳ�ִ�е�Ч������
        [SerializeField] bool abortIfCancelled = false; // �Ƿ�����Ϊ��Ч����ȡ��ʱ����ִֹ��

        // ��ʼЧ���ķ���������һ����д�ķ��������ڿ�ʼִ��Ч��
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(DelayedEffect(data, finished)); // ��ʼЭ�̣�ִ���ӳ�Ч��
        }

        // �ӳ�Ч����Э��
        private IEnumerator DelayedEffect(AbilityData data, Action finished)
        {
            yield return new WaitForSeconds(delay); // �ȴ��趨���ӳ�ʱ��
            if (abortIfCancelled && data.IsCancelled()) yield break; // �����������ֹ��Ч����ȡ����������Э��
            foreach (var effect in delayedEffects)
            {
                effect.StartEffect(data, finished); // ����ÿ���ӳ�Ч��
            }
        }
    }
}
