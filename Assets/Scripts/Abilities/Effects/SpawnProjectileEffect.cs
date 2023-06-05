using System;
using RPG.Attributes;
using RPG.Combat;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // ����Ͷ����Ч��
    [CreateAssetMenu(fileName = "Spawn Projectile Effect", menuName = "Abilities/Effects/Spawn Projectile", order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile projectileToSpawn; // Ҫ���ɵ�Ͷ����
        [SerializeField] float damage; // �˺�ֵ
        [SerializeField] bool isRightHand = true; // �Ƿ�������
        [SerializeField] bool useTargetPoint = true; // �Ƿ�ʹ��Ŀ���

        public override void StartEffect(AbilityData data, Action finished)
        {
            Fighter fighter = data.GetUser().GetComponent<Fighter>();
            Vector3 spawnPosition = fighter.GetHandTransform(isRightHand).position; // ��ȡ����λ��
            if (useTargetPoint)
            {
                SpawnProjectileForTargetPoint(data, spawnPosition); // �������Ŀ����Ͷ����
            }
            else
            {
                SpawnProjectilesForTargets(data, spawnPosition); // �������Ŀ���Ͷ����
            }
            finished();
        }

        private void SpawnProjectileForTargetPoint(AbilityData data, Vector3 spawnPosition)
        {
            Projectile projectile = Instantiate(projectileToSpawn);
            projectile.transform.position = spawnPosition;
            projectile.SetTarget(data.GetTargetedPoint(), data.GetUser(), damage); // ����Ͷ����Ŀ�����˺�ֵ
        }

        private void SpawnProjectilesForTargets(AbilityData data, Vector3 spawnPosition)
        {
            foreach (var target in data.GetTargets())
            {
                Health health = target.GetComponent<Health>();
                if (health)
                {
                    Projectile projectile = Instantiate(projectileToSpawn);
                    projectile.transform.position = spawnPosition;
                    projectile.SetTarget(health, data.GetUser(), damage); // ����Ͷ����Ŀ����˺�ֵ
                }
            }
        }
    }
}
