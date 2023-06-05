using System;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab; // 持久化对象的预制体

        static bool hasSpawned = false; // 是否已生成持久化对象

        private void Awake()
        {
            if (hasSpawned) return; // 如果已生成持久化对象，直接返回

            SpawnPersistentObjects(); // 生成持久化对象

            hasSpawned = true; // 设置已生成持久化对象的标志
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab); // 实例化持久化对象
            DontDestroyOnLoad(persistentObject); // 在场景加载时保持持久化对象不被销毁
        }
    }
}
