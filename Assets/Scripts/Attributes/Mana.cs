using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    // ����ֵ�࣬ʵ����ISaveable�ӿ���֧�ֱ���ͼ���
    public class Mana : MonoBehaviour, ISaveable
    {
        LazyValue<float> mana; // �ӳټ��صķ���ֵ

        private void Awake()
        {
            mana = new LazyValue<float>(GetMaxMana); // ��ʼ���ӳټ��صķ���ֵ
        }

        private void Update()
        {
            if (mana.value < GetMaxMana()) // �������ֵС�����ֵ
            {
                mana.value += GetRegenRate() * Time.deltaTime; // ���ӷ���ֵ�����ݻָ����ʺ�ʱ��������
                if (mana.value > GetMaxMana()) // �������ֵ�������ֵ
                {
                    mana.value = GetMaxMana(); // ������ֵ��Ϊ���ֵ
                }
            }
        }

        public float GetMana()
        {
            return mana.value; // ��ȡ��ǰ����ֵ
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana); // ��ȡ�����ֵ
        }

        public float GetRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate); // ��ȡ����ֵ�ָ�����
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana.value) // �����Ҫʹ�õķ���ֵ���ڵ�ǰ����ֵ
            {
                return false; // ����false����ʾ����ֵ����
            }
            mana.value -= manaToUse; // �۳�����ֵ
            return true; // ����true����ʾ����ֵʹ�óɹ�
        }

        public object CaptureState()
        {
            return mana.value; // ���淨��ֵ��״̬
        }

        public void RestoreState(object state)
        {
            mana.value = (float)state; // ���ط���ֵ��״̬
        }
    }
}