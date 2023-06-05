using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

namespace RPG.Inventories
{
    // �����ű����̳���ScriptableObject
    [CreateAssetMenu(menuName = ("RPG/Inventory/Drop Library"))]
    public class DropLibrary : ScriptableObject
    {
        [SerializeField]
        DropConfig[] potentialDrops;  // Ǳ�ڵ�����Ʒ����
        [SerializeField] float[] dropChancePercentage;  // ������ʰٷֱ�����
        [SerializeField] int[] minDrops;  // ���ٵ�����������
        [SerializeField] int[] maxDrops;  // ��������������

        [System.Serializable]
        class DropConfig
        {
            public InventoryItem item;  // ������Ʒ
            public float[] relativeChance;  // ��Ը�������
            public int[] minNumber;  // ��С��������
            public int[] maxNumber;  // �����������
            public int GetRandomNumber(int level)
            {
                if (!item.IsStackable())  // �����Ʒ���ɶѵ�������1
                {
                    return 1;
                }
                int min = GetByLevel(minNumber, level);  // ��ȡ��Ӧ�ȼ�����С����
                int max = GetByLevel(maxNumber, level);  // ��ȡ��Ӧ�ȼ����������
                return UnityEngine.Random.Range(min, max + 1);  // ����С�������������֮�����ѡ��һ����������
            }
        }

        public struct Dropped
        {
            public InventoryItem item;  // ������Ʒ
            public int number;  // ����
        }

        public IEnumerable<Dropped> GetRandomDrops(int level)
        {
            if (!ShouldRandomDrop(level))  // ���ݵȼ��ж��Ƿ�Ӧ���������
            {
                yield break;
            }
            for (int i = 0; i < GetRandomNumberOfDrops(level); i++)
            {
                yield return GetRandomDrop(level);
            }
        }

        bool ShouldRandomDrop(int level)
        {
            return Random.Range(0, 100) < GetByLevel(dropChancePercentage, level);  // ���ݵȼ��͵�����ʰٷֱ��ж��Ƿ�Ӧ�ý����������
        }

        int GetRandomNumberOfDrops(int level)
        {
            int min = GetByLevel(minDrops, level);  // ��ȡ��Ӧ�ȼ�����С��������
            int max = GetByLevel(maxDrops, level);  // ��ȡ��Ӧ�ȼ�������������
            return Random.Range(min, max);  // ����С�������������֮�����ѡ��һ����������
        }

        Dropped GetRandomDrop(int level)
        {
            var drop = SelectRandomItem(level);  // ���ѡ��һ��������Ʒ
            var result = new Dropped();
            result.item = drop.item;  // ���õ�����Ʒ
            result.number = drop.GetRandomNumber(level);  // ���õ�������
            return result;
        }

        DropConfig SelectRandomItem(int level)
        {
            float totalChance = GetTotalChance(level);  // ��ȡ�ܸ���
            float randomRoll = Random.Range(0, totalChance);  // ��0���ܸ���֮�����ѡ��һ��ֵ
            float chanceTotal = 0;
            foreach (var drop in potentialDrops)
            {
                chanceTotal += GetByLevel(drop.relativeChance, level);  // ��ȡ��Ӧ�ȼ�����Ը���
                if (chanceTotal > randomRoll)  // ����ۼƸ��ʳ������ֵ��ѡ��õ�����Ʒ
                {
                    return drop;
                }
            }
            return null;
        }

        float GetTotalChance(int level)
        {
            float total = 0;
            foreach (var drop in potentialDrops)
            {
                total += GetByLevel(drop.relativeChance, level);  // ��ȡ��Ӧ�ȼ�����Ը��ʣ����ۼ�
            }
            return total;
        }

        static T GetByLevel<T>(T[] values, int level)
        {
            if (values.Length == 0)  // ������鳤��Ϊ0������Ĭ��ֵ
            {
                return default;
            }
            if (level > values.Length)  // ����ȼ��������鳤�ȣ������������һ��Ԫ��
            {
                return values[values.Length - 1];
            }
            if (level <= 0)  // ����ȼ�С�ڵ���0������Ĭ��ֵ
            {
                return default;
            }
            return values[level - 1];  // ���ض�Ӧ�ȼ���Ԫ��
        }
    }
}