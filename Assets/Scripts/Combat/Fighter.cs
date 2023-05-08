using UnityEngine;

// 在RPG.combat命名空间下定义一个名为Fighter的类，继承自MonoBehaviour，用于处理战斗逻辑
namespace RPG.combat
{
    public class Fighter : MonoBehaviour
    {
        // 定义一个公共方法Attack，用于对战斗目标进行攻击
        public void Attack(CombatTarget target)
        {
            // 输出提示信息，表示攻击行为发生
            print("放马过来吧！");
        }
    }
}