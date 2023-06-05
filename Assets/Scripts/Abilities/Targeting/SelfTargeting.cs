using System;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    // ����Ŀ��ѡ����ԣ����ڴ������ڱ༭����ʹ�õ��µĽű�����
    [CreateAssetMenu(fileName = "Self Targeting", menuName = "Abilities/Targeting/Self", order = 0)]
    public class SelfTargeting : TargetingStrategy
    {
        // ��ʼĿ��ѡ��
        public override void StartTargeting(AbilityData data, Action finished)
        {
            // ��Ŀ������Ϊʹ�ü��ܵ���Ϸ������
            data.SetTargets(new GameObject[] { data.GetUser() });
            // ��Ŀ�������Ϊʹ�ü��ܵ���Ϸ�����λ��
            data.SetTargetedPoint(data.GetUser().transform.position);
            finished(); // Ŀ��ѡ�����
        }
    }
}