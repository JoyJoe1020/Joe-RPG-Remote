using System;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab; // 需要持久化的对象的预设

        static bool hasSpawned = false; // 是否已经生成过持久化对象的标志

        private void Awake()
        {
            if (hasSpawned) return; // 如果已经生成过持久化对象，就不再生成

            SpawnPersistentObjects(); // 生成持久化对象
            
            hasSpawned = true; // 设置标志为已生成
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab); // 实例化持久化对象的预设
            DontDestroyOnLoad(persistentObject); // 保持这个对象在加载新场景时不被销毁
        }
    }
}
