// 使用Unity命名空间，此命名空间是Unity引擎的核心API
using UnityEngine;
// 使用RPG.Attributes命名空间，此命名空间定义了RPG游戏的一些属性
using RPG.Attributes;
// 使用RPG.Control命名空间，此命名空间定义了RPG游戏的一些控制逻辑
using RPG.Control;

// 定义RPG.Combat命名空间
namespace RPG.Combat
{
    // 用RequireComponent属性要求在同一个GameObject上必须有一个Health组件，如果没有会自动添加一个
    [RequireComponent(typeof(Health))]
    // 定义一个公共类CombatTarget，它继承自Unity的MonoBehaviour基类，并且实现了IRaycastable接口
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        // 实现了IRaycastable接口的GetCursorType方法，该方法返回游戏光标的类型，这里设置为Combat，表示战斗光标
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        // 实现了IRaycastable接口的HandleRaycast方法，该方法用于处理光线投射事件
        public bool HandleRaycast(PlayerController callingController)
        {
            // 如果CombatTarget组件被禁用，则返回false，表示不处理光线投射事件
            if (!enabled) return false;
            // 如果调用者（玩家控制器）无法攻击当前的GameObject，也返回false
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                return false;
            }

            // 如果鼠标左键被按下
            if (Input.GetMouseButton(0))
            {
                // 调用玩家控制器中的Fighter组件的Attack方法，攻击当前的GameObject
                callingController.GetComponent<Fighter>().Attack(gameObject);
            }

            // 表示光线投射事件已经被处理
            return true;
        }
    }
}
