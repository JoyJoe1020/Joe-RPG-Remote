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
        [SerializeField] Transform handTransform = null;  // ��ɫ�ֲ���Transform�����������������
        [SerializeField] Weapon defaultWeapon = null;  // Weapon���͵����ã�����������������

        // ��Щ�Ǻ�ս��״̬��صı���
        Health target;  // ����Health���͵ı��������ڴ洢����Ŀ�������ֵ���
        float timeSinceLastAttack = Mathf.Infinity;  // �ϴι�����ʱ��
        Weapon currentWeapon = null;

        private void Start()
        {
            EquipWeapon(defaultWeapon);    // ��Ϸ��ʼʱ��������
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

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();  // ��ȡ��ɫ��Animator���
            weapon.Spawn(handTransform, animator);  // ����������Spawn�����������������滻��ɫ�Ķ���������
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

        //Animation Event
        void Hit()
        {
            // ���Ŀ�겻���ڣ�ֱ�ӷ���
            if (target == null) { return; }
            target.TakeDamage(currentWeapon.GetDamage());
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
