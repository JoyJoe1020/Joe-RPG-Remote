using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    // ս��Ŀ���࣬ʵ����IRaycastable�ӿڣ����ڴ���ս��Ŀ������߽���
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat; // ����ս���������
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false; // ���δ����򷵻�false
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject)) // ������ܹ�����Ŀ��
            {
                return false; // ����false
            }

            if (Input.GetMouseButton(0)) // �������������
            {
                callingController.GetComponent<Fighter>().Attack(gameObject); // ������Ŀ��
            }

            return true; // ����true����ʾ�ɹ��������߽���
        }
    }
}