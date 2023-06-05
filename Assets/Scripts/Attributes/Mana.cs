using GameDevTV.Saving;
using GameDevTV.Utils;
using RPG.Stats;
using UnityEngine;

namespace RPG.Attributes
{
    // 法力值类，实现了ISaveable接口以支持保存和加载
    public class Mana : MonoBehaviour, ISaveable
    {
        LazyValue<float> mana; // 延迟加载的法力值

        private void Awake()
        {
            mana = new LazyValue<float>(GetMaxMana); // 初始化延迟加载的法力值
        }

        private void Update()
        {
            if (mana.value < GetMaxMana()) // 如果法力值小于最大值
            {
                mana.value += GetRegenRate() * Time.deltaTime; // 增加法力值，根据恢复速率和时间间隔计算
                if (mana.value > GetMaxMana()) // 如果法力值超过最大值
                {
                    mana.value = GetMaxMana(); // 将法力值设为最大值
                }
            }
        }

        public float GetMana()
        {
            return mana.value; // 获取当前法力值
        }

        public float GetMaxMana()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Mana); // 获取最大法力值
        }

        public float GetRegenRate()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ManaRegenRate); // 获取法力值恢复速率
        }

        public bool UseMana(float manaToUse)
        {
            if (manaToUse > mana.value) // 如果需要使用的法力值大于当前法力值
            {
                return false; // 返回false，表示法力值不足
            }
            mana.value -= manaToUse; // 扣除法力值
            return true; // 返回true，表示法力值使用成功
        }

        public object CaptureState()
        {
            return mana.value; // 保存法力值的状态
        }

        public void RestoreState(object state)
        {
            mana.value = (float)state; // 加载法力值的状态
        }
    }
}