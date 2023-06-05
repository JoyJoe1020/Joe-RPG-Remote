using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Stats;
using UnityEngine;

namespace RPG.Inventories
{
    // ��װ����Ʒ���࣬ʵ����IModifierProvider�ӿ�
    [CreateAssetMenu(menuName = ("RPG/Inventory/Equipable Item"))]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField]
        Modifier[] additiveModifiers;  // �ӷ�����������
        [SerializeField]
        Modifier[] percentageModifiers;  // �ٷֱ�����������

        [System.Serializable]
        struct Modifier
        {
            public Stat stat;  // ����
            public float value;  // ����ֵ
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;  // ���ض�Ӧ���Եļӷ�����ֵ
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;  // ���ض�Ӧ���Եİٷֱ�����ֵ
                }
            }
        }
    }
}