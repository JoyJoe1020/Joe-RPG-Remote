// 引入Unity引擎命名空间
using UnityEngine;
// 引入RPG.Movement命名空间
using RPG.Movement;
// 引入RPG.Core命名空间
using RPG.Core;

// 在RPG.combat命名空间下定义Fighter类，继承自MonoBehaviour，并实现IAction接口，用于处理战斗逻辑
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // 使用[SerializeField]属性，将武器范围暴露到Unity编辑器中，便于调整
        [SerializeField] float weaponRange = 2f;

        // 定义一个Transform类型的变量，用于存储攻击目标
        Transform target;

        // 每帧更新时调用此方法
        private void Update()
        {
            // 如果没有攻击目标（target为null），则跳过后续逻辑
            if (target == null) return;

            // 如果目标不在攻击范围内
            if (!GetIsInRange())
            {
                // 获取Mover组件，调用MoveTo方法，使角色移动到目标位置
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                // 如果目标在攻击范围内，停止移动
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            // 设置Animator组件的"attack"触发器，使角色播放攻击动画
            GetComponent<Animator>().SetTrigger("attack");
        }

        // 判断目标是否在武器范围内的方法
        private bool GetIsInRange()
        {
            // 计算当前对象与目标对象之间的距离，如果小于武器范围则返回true，否则返回false
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        // 用于对战斗目标发起攻击的公共方法
        public void Attack(CombatTarget combatTarget)
        {
            // 调用ActionScheduler组件的StartAction方法，开始当前的攻击行为
            GetComponent<ActionScheduler>().StartAction(this);

            // 将传入的combatTarget的Transform组件赋值给target变量，设置当前攻击目标
            target = combatTarget.transform;
        }

        // 用于取消当前攻击目标的公共方法，实现IAction接口中的Cancel方法
        public void Cancel()
        {
            // 将target设置为null，表示没有攻击目标
            target = null;
        }

        //Animation Event
        void Hit ()
        {
            // 用于处理攻击动画事件，目前为空，可以在这里实现具体的攻击逻辑
        }
    }
}
