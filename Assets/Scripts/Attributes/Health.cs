using System;
using GameDevTV.Utils;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    // 生命值类，实现了ISaveable接口以支持保存和加载
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70; // 回血百分比
        [SerializeField] TakeDamageEvent takeDamage; // 受伤事件
        public UnityEvent onDie; // 死亡事件

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyValue<float> healthPoints; // 延迟加载的生命值

        bool wasDeadLastFrame = false; // 上一帧是否死亡

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth); // 初始化延迟加载的生命值
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health); // 获取初始生命值
        }

        private void Start()
        {
            healthPoints.ForceInit(); // 强制初始化延迟加载的生命值
        }

        private void Update()
        {
            UpdateState(); // 更新状态
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth; // 注册升级事件的回调函数
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth; // 取消升级事件的回调函数
        }

        public bool IsDead()
        {
            return healthPoints.value <= 0; // 判断是否死亡
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0); // 扣除生命值，并确保不会小于0

            if (IsDead()) // 如果死亡
            {
                onDie.Invoke(); // 触发死亡事件
                AwardExperience(instigator); // 奖励经验值
            }
            else
            {
                takeDamage.Invoke(damage); // 触发受伤事件
            }
            UpdateState(); // 更新状态
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetMaxHealthPoints()); // 恢复生命值，并确保不会超过最大值
            UpdateState(); // 更新状态
        }

        public float GetHealthPoints()
        {
            return healthPoints.value; // 获取当前生命值
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health); // 获取最大生命值
        }

        public float GetPercentage()
        {
            return 100 * GetFraction(); // 获取生命值的百分比
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health); // 获取生命值的分数
        }

        private void UpdateState()
        {
            Animator animator = GetComponent<Animator>();
            if (!wasDeadLastFrame && IsDead()) // 如果之前没有死亡，但现在死亡
            {
                animator.SetTrigger("die"); // 播放死亡动画
                GetComponent<ActionScheduler>().CancelCurrentAction(); // 取消当前动作
            }

            if (wasDeadLastFrame && !IsDead()) // 如果之前死亡，但现在复活
            {
                animator.Rebind(); // 重新绑定动画状态机
            }

            wasDeadLastFrame = IsDead(); // 更新上一帧是否死亡的状态
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward)); // 奖励经验值
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100); // 计算回血的生命值
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints); // 回血并确保不会超过当前生命值
        }

        public object CaptureState()
        {
            return healthPoints.value; // 保存生命值的状态
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state; // 加载生命值的状态

            UpdateState(); // 更新状态
        }
    }
}