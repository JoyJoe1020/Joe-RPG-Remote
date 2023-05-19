using UnityEngine;

namespace RPG.Combat
{
    // 使用CreateAssetMenu特性，可以在Unity的菜单中创建新的武器对象
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // 这些是武器的属性
        [SerializeField] AnimatorOverrideController animatorOverride = null;  // 武器的动画控制器，用于替换角色的动画
        [SerializeField] GameObject equippedPrefab = null;  // 武器的预制体
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform;
                if (isRightHanded) handTransform = rightHand;
                else handTransform = leftHand;
                Instantiate(equippedPrefab, handTransform);
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;  // 替换角色的动画控制器
            }
        }
            

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}

