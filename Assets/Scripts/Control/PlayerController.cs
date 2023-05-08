using UnityEngine;
using RPG.Movement;
using System;
using RPG.combat;

// �����ռ�RPG.Control�¶���һ����ΪPlayerController���࣬�̳���MonoBehaviour�����ڴ�����ҿ���
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        // ��ÿһ֡����ʱ����
        private void Update()
        {
            // ��ս�����н���
            InteractWithCombat();
            // ���ƶ����н���
            InteractWithMovement();
        }

        // ������ս���Ľ���
        private void InteractWithCombat()
        {
            // ��ȡ�����������������ײ������
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            // ����������ײ����
            foreach (RaycastHit hit in hits)
            {
                // ���Ի�ȡ�����ϵ�CombatTarget���
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                // ���û��CombatTarget������������˴�ѭ��
                if (target == null) continue;

                // ���������������
                if (Input.GetMouseButtonDown(0))
                {
                    // ��ȡ��ǰ�����Fighter�������Ŀ�귢�𹥻�
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        // �������ƶ��Ľ���
        private void InteractWithMovement()
        {
            // ���������������
            if (Input.GetMouseButton(0))
            {
                // �ƶ����������λ��
                MoveToCursor();
            }
        }

        // �ƶ����������λ��
        private void MoveToCursor()
        {
            // ����һ��������ײ��Ϣ����
            RaycastHit hit;
            // �������ߣ�����Ƿ��볡���е�������ײ
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            // ��������巢����ײ
            if (hasHit)
            {
                // ��ȡ��ǰ�����Mover���������MoveTo��������Ŀ��λ������Ϊ����������Ľ���
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        // ��ȡ���������������
        private static Ray GetMouseRay()
        {
            // ���ش�����������������ߣ����߷�������������Ļ�ϵ�λ��
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}