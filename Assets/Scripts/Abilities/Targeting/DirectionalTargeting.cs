using System;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Abilities.Targeting
{
    // ������Ŀ��ѡ����ԣ����ڴ������ڱ༭����ʹ�õ��µĽű�����
    [CreateAssetMenu(fileName = "Directional Targeting", menuName = "Abilities/Targeting/Directional", order = 0)]
    public class DirectionalTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask layerMask; // ��������Ͷ���ͼ������
        [SerializeField] float groundOffset = 1; // �����ƫ����

        // ��ʼĿ��ѡ��
        public override void StartTargeting(AbilityData data, Action finished)
        {
            RaycastHit raycastHit; // ����Ͷ����
            Ray ray = PlayerController.GetMouseRay(); // ��ȡ�����λ�õ����λ�õ�����
            if (Physics.Raycast(ray, out raycastHit, 1000, layerMask)) // ��ָ����ͼ���Ͻ�������Ͷ��
            {
                // ����Ŀ���Ϊ���ߺ͵��潻���λ�ã����ǵ����ƫ��
                data.SetTargetedPoint(raycastHit.point + ray.direction * groundOffset / ray.direction.y);
            }
            finished(); // Ŀ��ѡ�����
        }
    }
}