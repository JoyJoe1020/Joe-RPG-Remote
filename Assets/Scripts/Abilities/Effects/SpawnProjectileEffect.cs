using System;
using RPG.Attributes;
using RPG.Combat;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 生成投射物效果类
    [CreateAssetMenu(fileName = "Spawn Projectile Effect", menuName = "Abilities/Effects/Spawn Projectile", order = 0)]
    public class SpawnProjectileEffect : EffectStrategy
    {
        [SerializeField] Projectile projectileToSpawn; // 要生成的投射物类型
        [SerializeField] float damage; // 生成的投射物所能造成的伤害值
        [SerializeField] bool isRightHand = true; // 是否从右手生成投射物
        [SerializeField] bool useTargetPoint = true; // 是否生成目标点的投射物

        // 开始执行效果
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

        // 生成针对目标点的投射物
        private void SpawnProjectileForTargetPoint(AbilityData data, Vector3 spawnPosition)
        {
            Projectile projectile = Instantiate(projectileToSpawn); // 实例化投射物
            projectile.transform.position = spawnPosition; // 设置投射物的位置
            projectile.SetTarget(data.GetTargetedPoint(), data.GetUser(), damage); // 设置投射物的目标和伤害值
        }

        // 生成针对目标的投射物
        private void SpawnProjectilesForTargets(AbilityData data, Vector3 spawnPosition)
        {
            foreach (var target in data.GetTargets())
            {
                Health health = target.GetComponent<Health>(); // 获取目标的生命值组件
                if (health) // 如果存在生命值组件
                {
                    Projectile projectile = Instantiate(projectileToSpawn); // 实例化投射物
                    projectile.transform.position = spawnPosition; // 设置投射物的位置
                    projectile.SetTarget(health, data.GetUser(), damage); // 设置投射物的目标和伤害值
                }
            }
        }
    }
}
