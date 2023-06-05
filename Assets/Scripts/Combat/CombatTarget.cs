using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    // 战斗目标类，实现了IRaycastable接口，用于处理战斗目标的射线交互
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat; // 返回战斗光标类型
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false; // 如果未激活，则返回false
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject)) // 如果不能攻击该目标
            {
                return false; // 返回false
            }

            if (Input.GetMouseButton(0)) // 如果按下鼠标左键
            {
                callingController.GetComponent<Fighter>().Attack(gameObject); // 攻击该目标
            }

            return true; // 返回true，表示成功处理射线交互
        }
    }
}