using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

// ��RPG.Control�����ռ��¶���һ����ΪPlayerController���࣬�̳���MonoBehaviour�����ڴ�����ҿ���
namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        // ����һ��Health���͵ı��������ڴ洢��Ϸ���������ֵ���
        Health health;

        // ��Start�����г�ʼ��Health���
        private void Start()
        {
            // ��ȡ��ǰ��Ϸ�����ϵ�Health���
            health = GetComponent<Health>();
        }

        // ��ÿһ֡����ʱ����
        private void Update()
        {
            // �����ɫ����������ִ�к����߼�
            if (health.IsDead()) { return; }

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

                // ���Ŀ�������û��CombatTarget�������������ǰ���������������һ������
                if (target == null) { continue; }

                // ��ȡĿ����Ϸ����
                GameObject targetGameObject = target.gameObject;

                // �жϵ�ǰ��Ϸ�����Ƿ���Թ���Ŀ����Ϸ����������ܹ�������������ǰ���������������һ������
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                // ���������������
                if (Input.GetMouseButton(0))
                {
                    // ��ȡ��ǰ��Ϸ�����ϵ�Fighter���������Attack������Ŀ����Ϸ�����𹥻�
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
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
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
