// ����Unity���������ռ�
using UnityEngine;
// ����RPG.Movement�����ռ䣬���ڷ��ʽ�ɫ�ƶ���صĹ���
using RPG.Movement;
// ����RPG.Core�����ռ䣬���ڷ��ʺ�����Ϸ����
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // ��Щ�Ǻ�������ص�����
        [SerializeField] float timeBetweenAttacks = 1f;  // ���ι���֮��ļ��ʱ��

        // ���������ֶηֱ��ʾ��ɫ�����ֺ����ֵ�λ�ã�������λ��ͨ���ǽ�ɫ��������λ��
        [SerializeField] Transform rightHandTransform = null;  
        [SerializeField] Transform leftHandTransform = null;

        [SerializeField] Weapon defaultWeapon = null;  // Weapon���͵����ã�����������������
        [SerializeField] string defaultWeaponName = "UnArmed";

        // ��Щ�Ǻ�ս��״̬��صı���
        Health target;  // ����Health���͵ı��������ڴ洢����Ŀ�������ֵ���
        float timeSinceLastAttack = Mathf.Infinity;  // �ϴι�����ʱ��
        Weapon currentWeapon = null;  // currentWeapon��ʾ��ɫ��ǰװ��������

        private void Start()
        {
            Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
            EquipWeapon(weapon);    // ��Ϸ��ʼʱ��������
        }

        // ÿ֡����ʱ���ô˷���
        private void Update()
        {
            // ����ʱ���������ۼӵ��ϴι���ʱ��
            timeSinceLastAttack += Time.deltaTime;

            // ���û�й���Ŀ�꣨targetΪnull���������������߼�
            if (target == null) return;
            // ���Ŀ�������������������߼�
            if (target.IsDead()) return;

            // ���Ŀ�겻�ڹ�����Χ��
            if (!GetIsInRange())
            {
                // ���õ�ǰGameObject��Mover�����ʹ��GameObject��1f���ٶ���Ŀ��λ���ƶ�
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                // ���Ŀ���ڹ�����Χ�ڣ�ֹͣ�ƶ�
                GetComponent<Mover>().Cancel();
                // ���й���
                AttackBehaviour();
            }
        }

        // EquipWeapon�������ڸ���ɫװ��һ���µ�����
        public void EquipWeapon(Weapon weapon)
        {
            // ����������ֵ��currentWeapon����ʾ��ɫ�Ѿ�װ�����������
            currentWeapon = weapon;

            // ��ȡ��ɫ��Animator��������������ڿ��ƽ�ɫ�Ķ���
            Animator animator = GetComponent<Animator>();

            // ����������Spawn�������ڽ�ɫ���������������������������صĶ���������
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        // ���й����ķ���
        private void AttackBehaviour()
        {
            // �ý�ɫʼ������Ŀ��
            transform.LookAt(target.transform);

            // ������﹥�����ʱ��
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                //�⽫�ᴥ��Hit()����

                // ������������
                TriggerAttack();
                // �����ϴι���ʱ��
                timeSinceLastAttack = 0;

            }
        }

        // �������������ķ���
        private void TriggerAttack()
        {
            // ����Animator�����"stopAttack"��������ȷ������״̬��ȷ
            GetComponent<Animator>().ResetTrigger("stopAttack");

            // ����Animator�����"attack"��������ʹ��ɫ���Ź�������
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Hit������ʾ��ɫ��������Ŀ��
        void Hit()
        {
            // ���Ŀ��Ϊ�գ���ֱ�ӷ���
            if (target == null) { return; }

            // �����ǰװ����������Ͷ������������ҩ�ȣ�
            if (currentWeapon.HasProjectile())
            {
                // ��ӽ�ɫ�����з���Ͷ�������Ŀ��
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                // ����ֱ�Ӷ�Ŀ������˺�
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        // Shoot����Ҳ�Ǳ�ʾ��ɫ��������Ŀ�꣬��ʵ��ֻ�Ǽ򵥵ص�����Hit����
        void Shoot()
        {
            Hit();
        }

        // �ж�Ŀ���Ƿ���������Χ�ڵķ���
        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        // �ж��Ƿ���Թ����ķ���
        public bool CanAttack(GameObject combatTarget)
        {
            // ���combatTargetΪnull���򷵻�false����ʾ�޷�����
            if (combatTarget == null) { return false; }
            // ��combatTarget�л�ȡHealth���
            Health targetToTest = combatTarget.GetComponent<Health>();
            // �ж�Ŀ���Ƿ������δ������������������򷵻�true����ʾ���Թ���
            return targetToTest != null && !targetToTest.IsDead();
        }


        // ���ڶ�ս��Ŀ�귢�𹥻��Ĺ�������
        public void Attack(GameObject combatTarget)
        {
            // ����ActionScheduler�����StartAction��������ʼ��ǰ�Ĺ�����Ϊ
            GetComponent<ActionScheduler>().StartAction(this);

            // �Ӵ����CombatTarget�����л�ȡHealth����������丳ֵ��target����
            target = combatTarget.GetComponent<Health>();
        }

        // ����ȡ����ǰ����Ŀ��Ĺ���������ʵ��IAction�ӿ��е�Cancel����
        public void Cancel()
        {
            // ֹͣ��������
            StopAttack();
            // ��target����Ϊnull����ʾû�й���Ŀ��
            target = null;
            // ֹͣ��ɫ���ƶ�
            GetComponent<Mover>().Cancel();
        }

        // ֹͣ���������ķ���
        private void StopAttack()
        {
            // ����Animator�����"attack"��������ȷ������״̬��ȷ
            GetComponent<Animator>().ResetTrigger("attack");
            // ����Animator�����"stopAttack"��������ʹ��ɫֹͣ��������
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
