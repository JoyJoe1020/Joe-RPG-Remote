using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

// ��RPG.Control�����ռ��¶���һ����ΪPlayerController���࣬�̳���MonoBehaviour�����ڴ�����ҿ���
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // ��ÿһ֡����ʱ����
        private void Update()
        {
            // ��ս�����н���������н���������
            if (InteractWithCombat()) return;
            // ���ƶ����н���������н���������
            if (InteractWithMovement()) return;
        }

        // ������ս���Ľ���
        private bool InteractWithCombat()
        {
            // ��ȡ�����������������ײ������
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            // ����������ײ����
            foreach (RaycastHit hit in hits)
            {
                // ���Ի�ȡ�����ϵ�CombatTarget���
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) { continue; }

                GameObject targetGameObject = target.gameObject;
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                // ���������������
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                // ��������������true
                return true;
            }
            // δ��������������false
            return false;
        }

        // �������ƶ��Ľ���
        private bool InteractWithMovement()
        {
            // ����һ��������ײ��Ϣ����
            RaycastHit hit;
            // �������ߣ�����Ƿ��볡���е�������ײ
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            // ��������巢����ײ
            if (hasHit)
            {
                // ���������������
                if (Input.GetMouseButton(0))
                {
                    // ��ȡ��ǰ�����Mover���������MoveTo��������Ŀ��λ������Ϊ����������Ľ���
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                // ��������������true
                return true;
            }
            // δ��������������false
            return false;
        }

        // ��ȡ���������������
        private static Ray GetMouseRay()
        {
            // ���ش�����������������ߣ����߷�������������Ļ�ϵ�λ��
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
