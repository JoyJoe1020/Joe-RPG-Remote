using System;
using System.Collections.Generic;
using RPG.Attributes;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // ����ֵЧ����
    [CreateAssetMenu(fileName = "Health Effect", menuName = "Abilities/Effects/Health", order = 0)]
    public class HealthEffect : EffectStrategy
    {
        [SerializeField] float healthChange; // ����ֵ�仯������������ֵ��ֵ

        // ��ʼЧ���ķ���������һ����д�ķ��������ڿ�ʼִ��Ч��
        public override void StartEffect(AbilityData data, Action finished)
        {
            foreach (var target in data.GetTargets())
            {
                var health = target.GetComponent<Health>(); // ��Ŀ������л�ȡHealth���
                if (health) // �������Health���
                {
                    if (healthChange < 0) // �������ֵ�仯��С��0����ʾ��Ѫ
                    {
                        health.TakeDamage(data.GetUser(), -healthChange); // ��Ŀ������˺�
                    }
                    else // �������ֵ�仯������0����ʾ�ָ�����ֵ
                    {
                        health.Heal(healthChange); // ��Ŀ���������
                    }
                }
            }
            finished(); // ִ�����
        }
    }
}
