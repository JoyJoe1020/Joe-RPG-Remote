using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    // ս��Ŀ���࣬ʵ����IRaycastable�ӿڣ����ڴ���ս��Ŀ������߽���
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        // ��ȡ�������
        public CursorType GetCursorType()
        {
            return CursorType.Combat; // ����ս���������
        }

        // �������߽���
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false; // �����ǰ���δ���ã��򷵻�false����ʾ�޷��������߽���
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject)) // �����Ҳ��ܹ�����Ŀ��
            {
                return false; // ����false����ʾ�޷��������߽���
            }

            if (Input.GetMouseButton(0)) // �������������
            {
                callingController.GetComponent<Fighter>().Attack(gameObject); // ����ҹ�����Ŀ��
            }

            return true; // ����true����ʾ�ɹ����������߽���
        }
    }
}
