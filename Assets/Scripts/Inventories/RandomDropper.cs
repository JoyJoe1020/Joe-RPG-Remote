using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    // ���������Ʒ���࣬�̳���ItemDropper
    public class RandomDropper : ItemDropper
    {
        // ��������
        [Tooltip("How far can the pickups be scattered from the dropper.")]
        [SerializeField] float scatterDistance = 1;  // ������Ʒ������ߵ�ɢ������
        [SerializeField] DropLibrary dropLibrary;  // �����

        // ����
        const int ATTEMPTS = 30;  // ���Ի�ȡNavMesh�Ĵ���

        // ���������Ʒ�ķ���
        public void RandomDrop()
        {
            var baseStats = GetComponent<BaseStats>();  // ��ȡBaseStats���

            var drops = dropLibrary.GetRandomDrops(baseStats.GetLevel());  // ���ݽ�ɫ�ȼ��ӵ�����ȡ���������Ʒ
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);  // ������Ʒ
            }
        }

        protected override Vector3 GetDropLocation()
        {
            // ���ǿ�����Ҫ��γ��Բ�����NavMesh�ϻ�ȡλ��
            for (int i = 0; i < ATTEMPTS; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;  // ���������һ�����������ѡ��һ��λ��
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 0.1f, NavMesh.AllAreas))  // ���������һ�������ڵ�λ���ϻ�ȡ��NavMesh����ĵ�
                {
                    return hit.position;  // ����λ������
                }
            }
            return transform.position;  // ���û���ҵ����ʵ�λ�ã��򷵻ص����ߵ�λ��
        }
    }
}