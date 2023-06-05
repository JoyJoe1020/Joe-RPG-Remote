using System;
using System.Collections;
using UnityEngine;

namespace RPG.Abilities.Effects
{
    // ����Ŀ��Ԥ����Ч��
    [CreateAssetMenu(fileName = "Spawn Target Prefab Effect", menuName = "Abilities/Effects/Spawn Target Prefab", order = 0)]
    public class SpawnTargetPrefabEffect : EffectStrategy
    {
        [SerializeField] Transform prefabToSpawn; // Ҫ���ɵ�Ԥ����
        [SerializeField] float destroyDelay = -1; // �����ӳ�ʱ��

        public override void StartEffect(AbilityData data, Action finished)
        {
            data.StartCoroutine(Effect(data, finished));
        }

        private IEnumerator Effect(AbilityData data, Action finished)
        {
            Transform instance = Instantiate(prefabToSpawn); // ����Ԥ����ʵ��
            instance.position = data.GetTargetedPoint(); // ����λ��ΪĿ���
            if (destroyDelay > 0)
            {
                yield return new WaitForSeconds(destroyDelay); // �ȴ�һ��ʱ��
                Destroy(instance.gameObject); // ����Ԥ����ʵ��
            }
            finished();
        }
    }
}
