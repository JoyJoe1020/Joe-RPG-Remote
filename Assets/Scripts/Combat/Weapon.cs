using UnityEngine;

namespace RPG.Combat
{
    // ʹ��CreateAssetMenu���ԣ�������Unity�Ĳ˵��д����µ���������
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // ��Щ������������
        [SerializeField] AnimatorOverrideController animatorOverride = null;  // �����Ķ����������������滻��ɫ�Ķ���
        [SerializeField] GameObject equippedPrefab = null;  // ������Ԥ����
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;

        // ������������������������滻��ɫ�Ķ���������
        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Instantiate(equippedPrefab, handTransform);  // ��ָ����λ����������
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;  // �滻��ɫ�Ķ���������
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

