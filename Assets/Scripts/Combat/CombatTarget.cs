using UnityEngine;
using RPG.Attributes;
using RPG.Control;

namespace RPG.Combat
{
    // 战斗目标类，实现了IRaycastable接口，用于处理战斗目标的射线交互
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        // 获取光标类型
        public CursorType GetCursorType()
        {
            return CursorType.Combat; // 返回战斗光标类型
        }

        // 处理射线交互
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false; // 如果当前组件未启用，则返回false，表示无法处理射线交互
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject)) // 如果玩家不能攻击此目标
            {
                return false; // 返回false，表示无法处理射线交互
            }

            if (Input.GetMouseButton(0)) // 如果按下鼠标左键
            {
                callingController.GetComponent<Fighter>().Attack(gameObject); // 让玩家攻击此目标
            }

            return true; // 返回true，表示成功处理了射线交互
        }
    }
}
