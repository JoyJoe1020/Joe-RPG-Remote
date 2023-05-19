// 引入Unity引擎命名空间
using UnityEngine;
// 引入RPG.Movement命名空间，用于访问角色移动相关的功能
using RPG.Movement;
// 引入RPG.Core命名空间，用于访问核心游戏功能
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // 这些是和武器相关的属性
        [SerializeField] float timeBetweenAttacks = 1f;  // 两次攻击之间的间隔时间
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;  // Weapon类型的引用，用于引用武器对象

        // 这些是和战斗状态相关的变量
        Health target;  // 声明Health类型的变量，用于存储攻击目标的生命值组件
        float timeSinceLastAttack = Mathf.Infinity;  // 上次攻击的时间
        Weapon currentWeapon = null;

        private void Start()
        {
            EquipWeapon(defaultWeapon);    // 游戏开始时生成武器
        }

        // 每帧更新时调用此方法
        private void Update()
        {
            // 计算时间增量并累加到上次攻击时间
            timeSinceLastAttack += Time.deltaTime;

            // 如果没有攻击目标（target为null），则跳过后续逻辑
            if (target == null) return;
            // 如果目标已死亡，跳过后续逻辑
            if (target.IsDead()) return;

            // 如果目标不在攻击范围内
            if (!GetIsInRange())
            {
                // 调用当前GameObject的Mover组件，使该GameObject以1f的速度向目标位置移动
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                // 如果目标在攻击范围内，停止移动
                GetComponent<Mover>().Cancel();
                // 进行攻击
                AttackBehaviour();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();  // 获取角色的Animator组件
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);  // 调用武器的Spawn方法生成武器，并替换角色的动画控制器
        }

        // 进行攻击的方法
        private void AttackBehaviour()
        {
            // 让角色始终面向目标
            transform.LookAt(target.transform);

            // 如果到达攻击间隔时间
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //这将会触发Hit()方法

                // 触发攻击动画
                TriggerAttack();
                // 重置上次攻击时间
                timeSinceLastAttack = 0;

            }
        }

        // 触发攻击动画的方法
        private void TriggerAttack()
        {
            // 重置Animator组件的"stopAttack"触发器，确保动画状态正确
            GetComponent<Animator>().ResetTrigger("stopAttack");

            // 设置Animator组件的"attack"触发器，使角色播放攻击动画
            GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            // 如果目标不存在，直接返回
            if (target == null) { return; }
            target.TakeDamage(currentWeapon.GetDamage());
        }


        // 判断目标是否在武器范围内的方法
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        // 判断是否可以攻击的方法
        public bool CanAttack(GameObject combatTarget)
        {
            // 如果combatTarget为null，则返回false，表示无法攻击
            if (combatTarget == null) { return false; }
            // 从combatTarget中获取Health组件
            Health targetToTest = combatTarget.GetComponent<Health>();
            // 判断目标是否存在且未死亡，如果满足条件则返回true，表示可以攻击
            return targetToTest != null && !targetToTest.IsDead();
        }


        // 用于对战斗目标发起攻击的公共方法
        public void Attack(GameObject combatTarget)
        {
            // 调用ActionScheduler组件的StartAction方法，开始当前的攻击行为
            GetComponent<ActionScheduler>().StartAction(this);

            // 从传入的CombatTarget对象中获取Health组件，并将其赋值给target变量
            target = combatTarget.GetComponent<Health>();
        }

        // 用于取消当前攻击目标的公共方法，实现IAction接口中的Cancel方法
        public void Cancel()
        {
            // 停止攻击动画
            StopAttack();
            // 将target设置为null，表示没有攻击目标
            target = null;
            GetComponent<Mover>().Cancel();
        }

        // 停止攻击动画的方法
        private void StopAttack()
        {
            // 重置Animator组件的"attack"触发器，确保动画状态正确
            GetComponent<Animator>().ResetTrigger("attack");
            // 设置Animator组件的"stopAttack"触发器，使角色停止攻击动画
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
