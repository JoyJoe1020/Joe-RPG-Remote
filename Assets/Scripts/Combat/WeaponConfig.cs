using UnityEngine;
using RPG.Attributes;
using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat
{
    // 武器配置类，用于配置武器属性
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : EquipableItem, IModifierProvider
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null; // 动画控制器覆盖
        [SerializeField] Weapon equippedPrefab = null; // 装备预制体
        [SerializeField] float weaponDamage = 5f; // 武器伤害值
        [SerializeField] float percentageBonus = 0; // 百分比加成
        [SerializeField] float weaponRange = 2f; // 武器攻击范围
        [SerializeField] bool isRightHanded = true; // 是否右手持武器
        [SerializeField] Projectile projectile = null; // 投射物

        const string weaponName = "Weapon";

        // 生成武器对象
        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Weapon weapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return weapon;
        }

        // 销毁旧的武器
        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        // 获取武器的位置
        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        // 判断是否有投射物
        public bool HasProjectile()
        {
            return projectile != null;
        }

        // 发射投射物
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        // 获取武器伤害值
        public float GetDamage()
        {
            return weaponDamage;
        }

        // 获取百分比加成
        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        // 获取攻击范围
        public float GetRange()
        {
            return weaponRange;
        }

        // 获取增加的修正器
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return weaponDamage;
            }
        }

        // 获取百分比修正器
        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return percentageBonus;
            }
        }
    }
}
