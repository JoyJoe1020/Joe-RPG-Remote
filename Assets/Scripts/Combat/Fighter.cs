// 引入Unity引擎命名空间
using UnityEngine;
// 引入RPG.Movement命名空间，用于访问角色移动相关的功能
using RPG.Movement;
// 引入RPG.Core命名空间，用于访问核心游戏功能
using RPG.Core;

// 在RPG.Combat命名空间下定义Fighter类，继承自MonoBehaviour，并实现IAction接口，用于处理战斗逻辑
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // 使用[SerializeField]属性，将武器范围暴露到Unity编辑器中，便于调整
        [SerializeField] float weaponRange = 2f;

        // 攻击间隔时间
        [SerializeField] float timeBetweenAttacks = 1f;

        // 新增字段表示武器的伤害
        [SerializeField] float weaponDamage = 5f;
 
        Health target;

        // 记录上次攻击的时间
        float timeSinceLastAttack = 0;

        // 每帧更新时调用此方法
        private void Update()
        {
            // 计算时间增量并累加到上次攻击时间
            timeSinceLastAttack += Time.deltaTime;

            // 如果没有攻击目标（target为null），则跳过后续逻辑
            if (target == null) return;
            // 如果目标已死亡，跳过后续逻辑
            if(target.IsDead()) return;

            // 如果目标不在攻击范围内
            if (!GetIsInRange())
            {
                // 调用Mover组件的MoveTo方法，使角色移动到目标位置
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                // 如果目标在攻击范围内，停止移动
                GetComponent<Mover>().Cancel();
                // 进行攻击
                AttackBehaviour();
            }
        }

        // 进行攻击的方法
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            // 如果到达攻击间隔时间
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                //这将会触发Hit()方法

                // 设置Animator组件的"attack"触发器，使角色播放攻击动画
                GetComponent<Animator>().SetTrigger("attack");
                // 重置上次攻击时间
                timeSinceLastAttack = 0;

            }
        }

        //Animation Event
        void Hit()
        {
            // 调用目标的TakeDamage方法，对目标造成伤害
            target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        // 判断目标是否在武器范围内的方法
        private bool GetIsInRange()
        {
            // 计算当前对象和目标对象之间的距离，如果距离小于武器范围，则返回true，表示目标在武器范围内
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        // 用于对战斗目标发起攻击的公共方法
        public void Attack(CombatTarget combatTarget)
        {
            // 调用ActionScheduler组件的StartAction方法，开始当前的攻击行为
            GetComponent<ActionScheduler>().StartAction(this);

            // 从传入的CombatTarget对象中获取Health组件，并将其赋值给target变量
            target = combatTarget.GetComponent<Health>();
        }

        // 用于取消当前攻击目标的公共方法，实现IAction接口中的Cancel方法
        public void Cancel()
        {
            // 设置Animator组件的"stopAttack"触发器，使角色停止攻击动画
            GetComponent<Animator>().SetTrigger("stopAttack");
            // 将target设置为null，表示没有攻击目标
            target = null;
        }
    }
}
