using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 生成目标预制体效果类
    [CreateAssetMenu(fileName = "Spawn Target Prefab Effect", menuName = "Abilities/Effects/Spawn Target Prefab", order = 0)]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] Transform prefabToSpawn; // 要生成的预制体
        [SerializeField] float destroyDelay = -1; // 预制体的销毁延迟时间

        // 开始执行效果
        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(Effect(data, finished)); // 开始执行协程
        }

        // 协程方法
        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instance = Instantiate(prefabToSpawn); // 实例化预制体
            instance.position = data.GetTargetedPoint(); // 设置预制体的位置为目标点
            if (destroyDelay > 0) // 如果存在销毁延迟时间
            {
                yield return new WaitForSeconds(destroyDelay); // 等待一段时间
                Destroy(instance.gameObject); // 销毁预制体实例
            }
            finished();
        }
    }
}
