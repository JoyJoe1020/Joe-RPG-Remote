// ����Unity���������ռ�
using UnityEngine;
// ����RPG.Movement�����ռ�
using RPG.Movement;
// ����RPG.Core�����ռ�
using RPG.Core;

// ��RPG.combat�����ռ��¶���Fighter�࣬�̳���MonoBehaviour����ʵ��IAction�ӿڣ����ڴ���ս���߼�
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        // ʹ��[SerializeField]���ԣ���������Χ��¶��Unity�༭���У����ڵ���
        [SerializeField] float weaponRange = 2f;

        // ����һ��Transform���͵ı��������ڴ洢����Ŀ��
        Transform target;

        // ÿ֡����ʱ���ô˷���
        private void Update()
        {
            // ���û�й���Ŀ�꣨targetΪnull���������������߼�
            if (target == null) return;

            // ���Ŀ�겻�ڹ�����Χ��
            if (!GetIsInRange())
            {
                // ��ȡMover���������MoveTo������ʹ��ɫ�ƶ���Ŀ��λ��
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                // ���Ŀ���ڹ�����Χ�ڣ�ֹͣ�ƶ�
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            // ����Animator�����"attack"��������ʹ��ɫ���Ź�������
            GetComponent<Animator>().SetTrigger("attack");
        }

        // �ж�Ŀ���Ƿ���������Χ�ڵķ���
        private bool GetIsInRange()
        {
            // ���㵱ǰ������Ŀ�����֮��ľ��룬���С��������Χ�򷵻�true�����򷵻�false
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        // ���ڶ�ս��Ŀ�귢�𹥻��Ĺ�������
        public void Attack(CombatTarget combatTarget)
        {
            // ����ActionScheduler�����StartAction��������ʼ��ǰ�Ĺ�����Ϊ
            GetComponent<ActionScheduler>().StartAction(this);

            // �������combatTarget��Transform�����ֵ��target���������õ�ǰ����Ŀ��
            target = combatTarget.transform;
        }

        // ����ȡ����ǰ����Ŀ��Ĺ���������ʵ��IAction�ӿ��е�Cancel����
        public void Cancel()
        {
            // ��target����Ϊnull����ʾû�й���Ŀ��
            target = null;
        }

        //Animation Event
        void Hit ()
        {
            // ���ڴ����������¼���ĿǰΪ�գ�����������ʵ�־���Ĺ����߼�
        }
    }
}
