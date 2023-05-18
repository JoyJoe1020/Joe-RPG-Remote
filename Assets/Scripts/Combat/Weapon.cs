using UnityEngine;

namespace RPG.Combat
{
    // 使用CreateAssetMenu特性，可以在Unity的菜单中创建新的武器对象
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // 这些是武器的属性
        [SerializeField] AnimatorOverrideController animatorOverride = null;  // 武器的动画控制器，用于替换角色的动画
        [SerializeField] GameObject weaponPrefab = null;  // 武器的预制体

        // 这个方法用于生成武器，并替换角色的动画控制器
        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(weaponPrefab, handTransform);  // 在指定的位置生成武器
            animator.runtimeAnimatorController = animatorOverride;  // 替换角色的动画控制器
        }
    }
}
