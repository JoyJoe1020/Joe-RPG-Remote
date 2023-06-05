using System.Collections.Generic;
using GameDevTV.Inventories;
using RPG.Attributes;
using RPG.Core;
using UnityEngine;

namespace RPG.Abilities
{
    // �����࣬�̳���ActionItem����Ϸ�е��ж���Ʒ��
    [CreateAssetMenu(fileName = "My Ability", menuName = "Abilities/Ability", order = 0)]
    public class Ability : ActionItem
    {
        [SerializeField] TargetingStrategy targetingStrategy; // Ŀ��ѡ�����
        [SerializeField] FilterStrategy[] filterStrategies; // ���˲�������
        [SerializeField] EffectStrategy[] effectStrategies; // Ч����������
        [SerializeField] float cooldownTime = 0; // ��ȴʱ��
        [SerializeField] float manaCost = 0; // ���ĵķ���ֵ

        // ʹ�ü���
        public override bool Use(GameObject user)
        {
            Mana mana = user.GetComponent<Mana>(); // ��ȡʹ�ü��ܵ���Ϸ�����Mana���
            if (mana.GetMana() < manaCost) // �������ֵ������ʹ�ü���
            {
                return false; // ����false����ʾʹ��ʧ��
            }

            CooldownStore cooldownStore = user.GetComponent<CooldownStore>(); // ��ȡʹ�ü��ܵ���Ϸ�����CooldownStore���
            if (cooldownStore.GetTimeRemaining(this) > 0) // ������ܻ�����ȴ��
            {
                return false; // ����false����ʾʹ��ʧ��
            }

            AbilityData data = new AbilityData(user); // ����һ���µ�AbilityData���󣬴���ʹ�ü��ܵ���Ϸ����

            ActionScheduler actionScheduler = user.GetComponent<ActionScheduler>(); // ��ȡʹ�ü��ܵ���Ϸ�����ActionScheduler���
            actionScheduler.StartAction(data); // ����ActionScheduler�Ķ���������AbilityData����

            // ��ʼĿ��ѡ�񣬴���AbilityData�����Ŀ��ѡ����ɺ�Ļص�����
            targetingStrategy.StartTargeting(data,
                () =>
                {
                    TargetAquired(data);
                });

            return true; // ����true����ʾʹ�óɹ�
        }

        // Ŀ��ѡ����ɺ�Ļص�����
        private void TargetAquired(AbilityData data)
        {
            if (data.IsCancelled()) return; // ���Ŀ��ѡ��ȡ������ֱ�ӷ���

            Mana mana = data.GetUser().GetComponent<Mana>(); // ��ȡʹ�ü��ܵ���Ϸ�����Mana���
            if (!mana.UseMana(manaCost)) return; // �������ֵ������ʹ�ü��ܣ�ֱ�ӷ���

            CooldownStore cooldownStore = data.GetUser().GetComponent<CooldownStore>(); // ��ȡʹ�ü��ܵ���Ϸ�����CooldownStore���
            cooldownStore.StartCooldown(this, cooldownTime); // ��ʼ������ȴ

            foreach (var filterStrategy in filterStrategies)
            {
                // ��Ŀ����й��ˣ�������AbilityData�е�Ŀ���б�
                data.SetTargets(filterStrategy.Filter(data.GetTargets()));
            }

            foreach (var effect in effectStrategies)
            {
                effect.StartEffect(data, EffectFinished); // ��ʼЧ����������AbilityData�����Ч����ɺ�Ļص�����
            }
        }

        private void EffectFinished()
        {

        }
    }
}