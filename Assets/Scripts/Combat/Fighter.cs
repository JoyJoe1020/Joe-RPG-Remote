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
    // ս�����࣬���ڴ����ɫ��ս����Ϊ
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f; // �������ʱ��
        [SerializeField] Transform rightHandTransform = null; // ���ֹҵ�
        [SerializeField] Transform leftHandTransform = null; // ���ֹҵ�
        [SerializeField] WeaponConfig defaultWeapon = null; // Ĭ������
        [SerializeField] float autoAttackRange = 4f; // �Զ�������Χ

        Health target; // ����Ŀ��
        Equipment equipment; // װ��ϵͳ
        float timeSinceLastAttack = Mathf.Infinity; // �ϴι�����ʱ����
        WeaponConfig currentWeaponConfig; // ��ǰ����������
        LazyValue<Weapon> currentWeapon; // ��ǰ����

        private void Awake()
        {
            currentWeaponConfig = defaultWeapon; // ���õ�ǰ����ΪĬ������
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon); // ����һ���ӳټ��ص�Weapon����
            equipment = GetComponent<Equipment>(); // ��ȡװ��ϵͳ���
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon; // ע��װ�������¼�
            }
        }

        private Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon); // ����Ĭ������
        }

        private void Start()
        {
            currentWeapon.ForceInit(); // ǿ�Ƴ�ʼ����ǰ����
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime; // ���¹������ʱ��

            if (target == null || target.IsDead()) return; // �������Ŀ��Ϊ�ջ����������򷵻�

            if (!GetIsInRange(target.transform)) // ������ڹ�����Χ��
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f); // �ƶ�������Ŀ��
            }
            else
            {
                GetComponent<Mover>().Cancel(); // ȡ���ƶ�
                AttackBehaviour(); // ���й���
            }
        }

        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon; // ���õ�ǰ��������
            currentWeapon.value = AttachWeapon(weapon); // ��������
        }

        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig; // ��ȡװ�����е�����
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon); // ���û��������װ��Ĭ������
            }
            else
            {
                EquipWeapon(weapon); // װ��ָ������
            }
        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTransform, leftHandTransform, animator); // �����������󲢹��ص�����
        }

        public Health GetTarget()
        {
            return target; // ���ع���Ŀ��
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
            transform.LookAt(target.transform); // ���򹥻�Ŀ��
            if (timeSinceLastAttack > timeBetweenAttacks) // ��������������ʱ��
            {
                // ��������
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private Health FindNewTargetInRange()
        {
            Health best = null; // ��ѹ���Ŀ��
            float bestDistance = Mathf.Infinity; // ��ѹ���Ŀ�����
            foreach (var candidate in FindAllTargetsInRange()) // ������Χ�ڵ�����Ŀ��
            {
                float candidateDistance = Vector3.Distance(
                    transform.position, candidate.transform.position); // ����Ŀ��������ľ���
                if (candidateDistance < bestDistance) // ����������
                {
                    best = candidate; // ������ѹ���Ŀ��
                    bestDistance = candidateDistance; // ������ѹ���Ŀ�����
                }
            }
            return best; // ������ѹ���Ŀ��
        }

        private IEnumerable<Health> FindAllTargetsInRange()
        {
            RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position,
                                                autoAttackRange, Vector3.up); // �ڷ�Χ�ڽ����������߼��
            foreach (var hit in raycastHits)
            {
                Health health = hit.transform.GetComponent<Health>(); // ��ȡ����Ŀ�������ֵ���
                if (health == null) continue; // �������ֵ���Ϊ�գ�������
                if (health.IsDead()) continue; // ���Ŀ����������������
                if (health.gameObject == gameObject) continue; // ���Ŀ��������������
                yield return health; // ���ط�Χ�ڵ�Ŀ��
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack"); // ����ֹͣ�����Ĵ�����
            GetComponent<Animator>().SetTrigger("attack"); // ���ù����Ĵ�����
        }

        // �����¼�
        void Hit()
        {
            if (target == null) { return; }

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage); // ��ȡ�˺�ֵ
            BaseStats targetBaseStats = target.GetComponent<BaseStats>(); // ��ȡĿ��Ļ�������
            if (targetBaseStats != null)
            {
                float defence = targetBaseStats.GetStat(Stat.Defence); // ��ȡĿ��ķ���ֵ
                damage /= 1 + defence / damage; // ����ʵ���˺�ֵ
            }

            if (currentWeapon.value != null)
            {
                currentWeapon.value.OnHit(); // ���������������¼�
            }

            if (currentWeaponConfig.HasProjectile()) // �����ǰ������Ͷ����
            {
                currentWeaponConfig.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage); // ����Ͷ����
            }
            else
            {
                target.TakeDamage(gameObject, damage); // ��Ŀ������˺�
            }
        }

        void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.GetRange(); // �ж�Ŀ���Ƿ��ڹ�����Χ��
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
            return targetToTest != null && !targetToTest.IsDead(); // �ж��Ƿ���Թ���ָ��Ŀ��
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this); // ��ʼ������Ϊ
            target = combatTarget.GetComponent<Health>(); // ���ù���Ŀ��
        }

        public void Cancel()
        {
            StopAttack(); // ֹͣ����
            target = null; // ��չ���Ŀ��
            GetComponent<Mover>().Cancel(); // ȡ���ƶ�
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack"); // ���ù���������
            GetComponent<Animator>().SetTrigger("stopAttack"); // ����ֹͣ�����Ĵ�����
        }
    }
}
