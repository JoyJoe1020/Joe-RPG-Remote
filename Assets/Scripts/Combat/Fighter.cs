// ʹ��Unity����
using UnityEngine;
// ����RPG.Movement�����ռ�
using RPG.Movement;
using RPG.Core;

// ��RPG.combat�����ռ��¶���Fighter�࣬�̳���MonoBehaviour�����ڴ���ս���߼�
namespace RPG.combat
{
    public class Fighter : MonoBehaviour
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
                GetComponent<Mover>().Stop();
            }
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
            GetComponent<ActionScheduler>().StartAction(this);

            // �������combatTarget��Transform�����ֵ��target���������õ�ǰ����Ŀ��
            target = combatTarget.transform;
        }

        // ����ȡ����ǰ����Ŀ��Ĺ�������
        public void Cancel()
        {
            // ��target����Ϊnull����ʾû�й���Ŀ��
            target = null;
        }
    }
}