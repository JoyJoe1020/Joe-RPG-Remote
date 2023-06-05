using System;
using RPG.Attributes;
using RPG.Combat;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 生成投射物效果
    [CreateAssetMenu(fileName = "Spawn Projectile Effect", menuName = "Abilities/Effects/Spawn Projectile", order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile projectileToSpawn; // 要生成的投射物
        [SerializeField] float damage; // 伤害值
        [SerializeField] bool isRightHand = true; // 是否是右手
        [SerializeField] bool useTargetPoint = true; // 是否使用目标点

        public override void StartEffect(AbilityData data, Action finished)
        {
            Fighter fighter = data.GetUser().GetComponent<Fighter>();
            Vector3 spawnPosition = fighter.GetHandTransform(isRightHand).position; // 获取生成位置
            if (useTargetPoint)
            {
                SpawnProjectileForTargetPoint(data, spawnPosition); // 生成针对目标点的投射物
            }
            else
            {
                SpawnProjectilesForTargets(data, spawnPosition); // 生成针对目标的投射物
            }
            finished();
        }

        private void SpawnProjectileForTargetPoint(AbilityData data, Vector3 spawnPosition)
        {
            Projectile projectile = Instantiate(projectileToSpawn);
            projectile.transform.position = spawnPosition;
            projectile.SetTarget(data.GetTargetedPoint(), data.GetUser(), damage); // 设置投射物目标点和伤害值
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
                    projectile.SetTarget(health, data.GetUser(), damage); // 设置投射物目标和伤害值
                }
            }
        }
    }
}
