// ʹ��Unity�����ռ䣬�������ռ���Unity����ĺ���API
using UnityEngine;
// ʹ��RPG.Attributes�����ռ䣬�������ռ䶨����RPG��Ϸ��һЩ����
using RPG.Attributes;
// ʹ��RPG.Control�����ռ䣬�������ռ䶨����RPG��Ϸ��һЩ�����߼�
using RPG.Control;

// ����RPG.Combat�����ռ�
namespace RPG.Combat
{
    // ��RequireComponent����Ҫ����ͬһ��GameObject�ϱ�����һ��Health��������û�л��Զ����һ��
    [RequireComponent(typeof(Health))]
    // ����һ��������CombatTarget�����̳���Unity��MonoBehaviour���࣬����ʵ����IRaycastable�ӿ�
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        // ʵ����IRaycastable�ӿڵ�GetCursorType�������÷���������Ϸ�������ͣ���������ΪCombat����ʾս�����
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        // ʵ����IRaycastable�ӿڵ�HandleRaycast�������÷������ڴ������Ͷ���¼�
        public bool HandleRaycast(PlayerController callingController)
        {
            // ���CombatTarget��������ã��򷵻�false����ʾ���������Ͷ���¼�
            if (!enabled) return false;
            // ��������ߣ���ҿ��������޷�������ǰ��GameObject��Ҳ����false
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            // ���������������
            if (Input.GetMouseButton(0))
            {
                // ������ҿ������е�Fighter�����Attack������������ǰ��GameObject
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }

            // ��ʾ����Ͷ���¼��Ѿ�������
            return true;
        }
    }
}
