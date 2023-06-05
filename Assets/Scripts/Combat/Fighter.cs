using UnityEngine;
using RPG.Movement;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Attributes;
using RPG.Stats;
using System.Collections.Generic;
using GameDevTV.Utils;
using System;
using GameDevTV.Inventories;

namespace RPG.Combat
{
    // 战斗者类，用于处理角色的战斗行为
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f; // 攻击间隔时间
        [SerializeField] Transform rightHandTransform = null; // 右手挂点
        [SerializeField] Transform leftHandTransform = null; // 左手挂点
        [SerializeField] WeaponConfig defaultWeapon = null; // 默认武器
        [SerializeField] float autoAttackRange = 4f; // 自动攻击范围

        Health target; // 攻击目标
        Equipment equipment; // 装备系统
        float timeSinceLastAttack = Mathf.Infinity; // 上次攻击的时间间隔
        WeaponConfig currentWeaponConfig; // 当前武器的配置
        LazyValue<Weapon> currentWeapon; // 当前武器

        private void Awake()
        {
            currentWeaponConfig = defaultWeapon; // 设置当前武器为默认武器
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon); // 创建一个延迟加载的Weapon对象
            equipment = GetComponent<Equipment>(); // 获取装备系统组件
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon; // 注册装备更新事件
            }
        }

        private Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon); // 设置默认武器
        }

        private void Start()
        {
            currentWeapon.ForceInit(); // 强制初始化当前武器
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime; // 更新攻击间隔时间

            if (target == null || target.IsDead()) return; // 如果攻击目标为空或已死亡，则返回

            if (!GetIsInRange(target.transform)) // 如果不在攻击范围内
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f); // 移动到攻击目标
            }
            else
            {
                GetComponent<Mover>().Cancel(); // 取消移动
                AttackBehaviour(); // 进行攻击
            }
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon; // 设置当前武器配置
            currentWeapon.value = AttachWeapon(weapon); // 附着武器
        }

        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig; // 获取装备槽中的武器
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon); // 如果没有武器则装备默认武器
            }
            else
            {
                EquipWeapon(weapon); // 装备指定武器
            }
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator); // 生成武器对象并挂载到手上
        }

        public Health GetTarget()
        {
            return target; // 返回攻击目标
        }

        public Transform GetHandTransform(bool isRightHand)
        {
            if (isRightHand)
            {
                return rightHandTransform;
            }
            else
            {
                return leftHandTransform;
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform); // 面向攻击目标
            if (timeSinceLastAttack > timeBetweenAttacks) // 如果超过攻击间隔时间
            {
                // 触发攻击
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private Health FindNewTargetInRange()
        {
            Health best = null; // 最佳攻击目标
            float bestDistance = Mathf.Infinity; // 最佳攻击目标距离
            foreach (var candidate in FindAllTargetsInRange()) // 遍历范围内的所有目标
            {
                float candidateDistance = Vector3.Distance(
                    transform.position, candidate.transform.position); // 计算目标与自身的距离
                if (candidateDistance < bestDistance) // 如果距离更近
                {
                    best = candidate; // 更新最佳攻击目标
                    bestDistance = candidateDistance; // 更新最佳攻击目标距离
                }
            }
            return best; // 返回最佳攻击目标
        }

        private IEnumerable<Health> FindAllTargetsInRange()
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position,
                                                autoAttackRange, Vector3.up); // 在范围内进行球形射线检测
            foreach (var hit in raycastHits)
            {
                Health health = hit.transform.GetComponent<Health>(); // 获取击中目标的生命值组件
                if (health == null) continue; // 如果生命值组件为空，则跳过
                if (health.IsDead()) continue; // 如果目标已死亡，则跳过
                if (health.gameObject == gameObject) continue; // 如果目标是自身，则跳过
                yield return health; // 返回范围内的目标
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack"); // 重置停止攻击的触发器
            GetComponent<Animator>().SetTrigger("attack"); // 设置攻击的触发器
        }

        // 动画事件
        void Hit()
        {
            if (target == null) { return; }

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage); // 获取伤害值
            BaseStats targetBaseStats = target.GetComponent<BaseStats>(); // 获取目标的基础属性
            if (targetBaseStats != null)
            {
                float defence = targetBaseStats.GetStat(Stat.Defence); // 获取目标的防御值
                damage /= 1 + defence / damage; // 计算实际伤害值
            }

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit(); // 触发武器的命中事件
            }

            if (currentWeaponConfig.HasProjectile()) // 如果当前武器是投射物
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage); // 发射投射物
            }
            else
            {
                target.TakeDamage(gameObject, damage); // 对目标造成伤害
            }
        }

        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.GetRange(); // 判断目标是否在攻击范围内
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            if (!GetComponent<Mover>().CanMoveTo(combatTarget.transform.position) &&
                !GetIsInRange(combatTarget.transform))
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead(); // 判断是否可以攻击指定目标
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this); // 开始攻击行为
            target = combatTarget.GetComponent<Health>(); // 设置攻击目标
        }

        public void Cancel()
        {
            StopAttack(); // 停止攻击
            target = null; // 清空攻击目标
            GetComponent<Mover>().Cancel(); // 取消移动
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack"); // 重置攻击触发器
            GetComponent<Animator>().SetTrigger("stopAttack"); // 设置停止攻击的触发器
        }
    }
}
