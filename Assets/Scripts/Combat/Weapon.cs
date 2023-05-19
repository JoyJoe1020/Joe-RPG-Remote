using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    // ʹ��CreateAssetMenu���ԣ�������Unity�Ĳ˵��д����µ���������
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        // �����ĸ�������
        [SerializeField] AnimatorOverrideController animatorOverride = null;  // �����Ķ����������������滻��ɫ�Ķ���
        [SerializeField] GameObject equippedPrefab = null;  // װ��ʱ������Ԥ����
        [SerializeField] float weaponDamage = 5f;  // �������˺�ֵ
        [SerializeField] float weaponRange = 2f;  // �����Ĺ�����Χ
        [SerializeField] bool isRightHanded = true;  // �Ƿ�Ϊ��������
        [SerializeField] Projectile projectile = null;  // ������Ͷ����

        // ��������ʵ����װ��
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                Instantiate(equippedPrefab, handTransform);  // ��ָ���ֲ�λ��ʵ����Ԥ����
            }

            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;  // ʹ�ø������Ķ����������滻��ɫ�Ķ���������
            }
        }

        // �ж�Ӧ������ֻ����װ������
        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;  // �����������������ѡ������
            else handTransform = leftHand;  // ����ѡ������
            return handTransform;
        }

        // �ж��Ƿ���Ͷ����
        public bool HasProjectile()
        {
            return projectile != null;  // ���Ͷ���ﲻΪ�գ��򷵻�true
        }

        // ����Ͷ����
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);  // ʵ����Ͷ����
            projectileInstance.SetTarget(target, weaponDamage);  // ����Ͷ�����Ŀ����˺�ֵ
        }

        // ��ȡ�������˺�ֵ
        public float GetDamage()
        {
            return weaponDamage;
        }

        // ��ȡ�����Ĺ�����Χ
        public float GetRange()
        {
            return weaponRange;
        }
    }
}
