using System;
using GameDevTV.Utils;
using RPG.Core;
using GameDevTV.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    // ����ֵ�࣬ʵ����ISaveable�ӿ���֧�ֱ���ͼ���
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70; // ��Ѫ�ٷֱ�
        [SerializeField] TakeDamageEvent takeDamage; // �����¼�
        public UnityEvent onDie; // �����¼�

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }

        LazyValue<float> healthPoints; // �ӳټ��ص�����ֵ

        bool wasDeadLastFrame = false; // ��һ֡�Ƿ�����

        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth); // ��ʼ���ӳټ��ص�����ֵ
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health); // ��ȡ��ʼ����ֵ
        }

        private void Start()
        {
            healthPoints.ForceInit(); // ǿ�Ƴ�ʼ���ӳټ��ص�����ֵ
        }

        private void Update()
        {
            UpdateState(); // ����״̬
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth; // ע�������¼��Ļص�����
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth; // ȡ�������¼��Ļص�����
        }

        public bool IsDead()
        {
            return healthPoints.value <= 0; // �ж��Ƿ�����
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0); // �۳�����ֵ����ȷ������С��0

            if (IsDead()) // �������
            {
                onDie.Invoke(); // ���������¼�
                AwardExperience(instigator); // ��������ֵ
            }
            else
            {
                takeDamage.Invoke(damage); // ���������¼�
            }
            UpdateState(); // ����״̬
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetMaxHealthPoints()); // �ָ�����ֵ����ȷ�����ᳬ�����ֵ
            UpdateState(); // ����״̬
        }

        public float GetHealthPoints()
        {
            return healthPoints.value; // ��ȡ��ǰ����ֵ
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health); // ��ȡ�������ֵ
        }

        public float GetPercentage()
        {
            return 100 * GetFraction(); // ��ȡ����ֵ�İٷֱ�
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health); // ��ȡ����ֵ�ķ���
        }

        private void UpdateState()
        {
            Animator animator = GetComponent<Animator>();
            if (!wasDeadLastFrame && IsDead()) // ���֮ǰû������������������
            {
                animator.SetTrigger("die"); // ������������
                GetComponent<ActionScheduler>().CancelCurrentAction(); // ȡ����ǰ����
            }

            if (wasDeadLastFrame && !IsDead()) // ���֮ǰ�����������ڸ���
            {
                animator.Rebind(); // ���°󶨶���״̬��
            }

            wasDeadLastFrame = IsDead(); // ������һ֡�Ƿ�������״̬
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward)); // ��������ֵ
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100); // �����Ѫ������ֵ
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints); // ��Ѫ��ȷ�����ᳬ����ǰ����ֵ
        }

        public object CaptureState()
        {
            return healthPoints.value; // ��������ֵ��״̬
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state; // ��������ֵ��״̬

            UpdateState(); // ����״̬
        }
    }
}