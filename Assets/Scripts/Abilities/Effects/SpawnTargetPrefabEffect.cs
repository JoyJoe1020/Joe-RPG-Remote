using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // 生成目标预制体效果
    [CreateAssetMenu(fileName = "Spawn Target Prefab Effect", menuName = "Abilities/Effects/Spawn Target Prefab", order = 0)]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] Transform prefabToSpawn; // 要生成的预制体
        [SerializeField] float destroyDelay = -1; // 销毁延迟时间

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(Effect(data, finished));
        }

        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instance = Instantiate(prefabToSpawn); // 生成预制体实例
            instance.position = data.GetTargetedPoint(); // 设置位置为目标点
            if (destroyDelay > 0)
            {
                yield return new WaitForSeconds(destroyDelay); // 等待一段时间
                Destroy(instance.gameObject); // 销毁预制体实例
            }
            finished();
        }
    }
}
