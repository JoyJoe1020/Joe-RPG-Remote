// ����Unity���������ռ�
using UnityEngine;
// ����RPG.Movement�����ռ䣬���ڷ��ʽ�ɫ�ƶ���صĹ���
using RPG.Movement;
// ����RPG.Core�����ռ䣬���ڷ��ʺ�����Ϸ����
using RPG.Core;

// ��RPG.Combat�����ռ��¶���Fighter�࣬�̳���MonoBehaviour����ʵ��IAction�ӿڣ����ڴ���ս���߼�
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // ʹ��[SerializeField]���ԣ���������Χ��¶��Unity�༭���У����ڵ���
        [SerializeField] float weaponRange = 2f;

        // �������ʱ��
        [SerializeField] float timeBetweenAttacks = 1f;

        // �����ֶα�ʾ�������˺�
        [SerializeField] float weaponDamage = 5f;
 
        Health target;

        // ��¼�ϴι�����ʱ��
        float timeSinceLastAttack = 0;

        // ÿ֡����ʱ���ô˷���
        private void Update()
        {
            // ����ʱ���������ۼӵ��ϴι���ʱ��
            timeSinceLastAttack += Time.deltaTime;

            // ���û�й���Ŀ�꣨targetΪnull���������������߼�
            if (target == null) return;
            // ���Ŀ�������������������߼�
            if(target.IsDead()) return;

            // ���Ŀ�겻�ڹ�����Χ��
            if (!GetIsInRange())
            {
                // ����Mover�����MoveTo������ʹ��ɫ�ƶ���Ŀ��λ��
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                // ���Ŀ���ڹ�����Χ�ڣ�ֹͣ�ƶ�
                GetComponent<Mover>().Cancel();
                // ���й���
                AttackBehaviour();
            }
        }

        // ���й����ķ���
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            // ������﹥�����ʱ��
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                //�⽫�ᴥ��Hit()����

                // ����Animator�����"attack"��������ʹ��ɫ���Ź�������
                GetComponent<Animator>().SetTrigger("attack");
                // �����ϴι���ʱ��
                timeSinceLastAttack = 0;

            }
        }

        //Animation Event
        void Hit()
        {
            // ����Ŀ���TakeDamage��������Ŀ������˺�
            target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        // �ж�Ŀ���Ƿ���������Χ�ڵķ���
        private bool GetIsInRange()
        {
            // ���㵱ǰ�����Ŀ�����֮��ľ��룬�������С��������Χ���򷵻�true����ʾĿ����������Χ��
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        // ���ڶ�ս��Ŀ�귢�𹥻��Ĺ�������
        public void Attack(CombatTarget combatTarget)
        {
            // ����ActionScheduler�����StartAction��������ʼ��ǰ�Ĺ�����Ϊ
            GetComponent<ActionScheduler>().StartAction(this);

            // �Ӵ����CombatTarget�����л�ȡHealth����������丳ֵ��target����
            target = combatTarget.GetComponent<Health>();
        }

        // ����ȡ����ǰ����Ŀ��Ĺ���������ʵ��IAction�ӿ��е�Cancel����
        public void Cancel()
        {
            // ����Animator�����"stopAttack"��������ʹ��ɫֹͣ��������
            GetComponent<Animator>().SetTrigger("stopAttack");
            // ��target����Ϊnull����ʾû�й���Ŀ��
            target = null;
        }
    }
}
