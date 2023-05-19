using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    // 使用CreateAssetMenu特性，可以在Unity的菜单中创建新的武器对象
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // 武器的各种属性
        [SerializeField] AnimatorOverrideController animatorOverride = null;  // 武器的动画控制器，用于替换角色的动画
        [SerializeField] GameObject equippedPrefab = null;  // 装备时的武器预制体
        [SerializeField] float weaponDamage = 5f;  // 武器的伤害值
        [SerializeField] float weaponRange = 2f;  // 武器的攻击范围
        [SerializeField] bool isRightHanded = true;  // 是否为右手武器
        [SerializeField] Projectile projectile = null;  // 武器的投射物

        const string weaponName = "Weapon";

        // 创建武器实例并装备
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;  // 使用该武器的动画控制器替换角色的动画控制器
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;
            
            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
        }

        // 判断应该在哪只手上装备武器
        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;  // 如果是右手武器，则选择右手
            else handTransform = leftHand;  // 否则选择左手
            return handTransform;
        }

        // 判断是否有投射物
        public bool HasProjectile()
        {
            return projectile != null;  // 如果投射物不为空，则返回true
        }

        // 发射投射物
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);  // 实例化投射物
            projectileInstance.SetTarget(target, weaponDamage);  // 设置投射物的目标和伤害值
        }

        // 获取武器的伤害值
        public float GetDamage()
        {
            return weaponDamage;
        }

        // 获取武器的攻击范围
        public float GetRange()
        {
            return weaponRange;
        }
    }
}
