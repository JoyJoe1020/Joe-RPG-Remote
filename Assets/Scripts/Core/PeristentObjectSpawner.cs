using System;
using UnityEngine;

namespace RPG.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab; // �־û������Ԥ����

        static bool hasSpawned = false; // �Ƿ������ɳ־û�����

        private void Awake()
        {
            if (hasSpawned) return; // ��������ɳ־û�����ֱ�ӷ���

            SpawnPersistentObjects(); // ���ɳ־û�����

            hasSpawned = true; // ���������ɳ־û�����ı�־
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab); // ʵ�����־û�����
            DontDestroyOnLoad(persistentObject); // �ڳ�������ʱ���ֳ־û����󲻱�����
        }
    }
}
