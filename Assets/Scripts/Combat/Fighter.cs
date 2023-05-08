using UnityEngine;
using RPG.Movement;

// ��RPG.combat�����ռ��¶���һ����ΪFighter���࣬�̳���MonoBehaviour�����ڴ���ս���߼�
namespace RPG.combat
{
    public class Fighter : MonoBehaviour
    {
        // ʹ��[SerializeField]���ԣ���������Χ��¶��Unity�༭����
        [SerializeField] float weaponRange = 2f;

        // ����һ��Transform���͵ı��������ڴ洢����Ŀ��
        Transform target;

        // ��ÿһ֡����ʱ����
        private void Update()
        {
            // �ж�Ŀ���Ƿ���������Χ��
            bool isInRange = Vector3.Distance(transform.position, target.position) < weaponRange;

            // ���Ŀ������Ҳ���������Χ��
            if (target != null && !isInRange)
            {
                // ��ȡMover���������MoveTo������ʹ��ɫ�ƶ���Ŀ��λ��
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                // �����������Χ�ڣ�ֹͣ�ƶ�
                GetComponent<Mover>().Stop();
            }
        }

        // ����һ����������Attack�����ڶ�ս��Ŀ����й���
        public void Attack(CombatTarget combatTarget)
        {
            // �������combatTarget��Transform�����ֵ��target����
            target = combatTarget.transform;
        }

        public void ClearTarget()
        {
            target = null;
        }
    }
}
