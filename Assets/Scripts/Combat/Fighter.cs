using UnityEngine;
using RPG.Movement;

// 在RPG.combat命名空间下定义一个名为Fighter的类，继承自MonoBehaviour，用于处理战斗逻辑
namespace RPG.combat
{
    public class Fighter : MonoBehaviour
    {
        // 使用[SerializeField]属性，将武器范围暴露到Unity编辑器中
        [SerializeField] float weaponRange = 2f;

        // 定义一个Transform类型的变量，用于存储攻击目标
        Transform target;

        // 在每一帧更新时调用
        private void Update()
        {
            // 判断目标是否在武器范围内
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;

            // 如果目标存在且不在武器范围内
            if (target != null && !isInRange)
            {
                // 获取Mover组件，调用MoveTo方法，使角色移动到目标位置
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                // 如果在武器范围内，停止移动
                GetComponent<Mover>().Stop();
            }
        }

        // 定义一个公共方法Attack，用于对战斗目标进行攻击
        public void Attack(CombatTarget combatTarget)
        {
            // 将传入的combatTarget的Transform组件赋值给target变量
            target = combatTarget.transform;
        }

        public void ClearTarget()
        {
            target = null;
        }
    }
}
