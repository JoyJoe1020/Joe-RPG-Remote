using System; // ����System�����ռ�
using System.Collections;
using Cinemachine;
using RPG.Attributes; // ��������ϵͳ�����ռ�
using RPG.SceneManagement; // ���볡������ϵͳ�����ռ�
using UnityEngine;
using UnityEngine.AI; // ����Unity����ϵͳ�����ռ�

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] Transform respawnLocation; // ����λ��
        [SerializeField] float respawnDelay = 3; // �����ӳ�ʱ��
        [SerializeField] float fadeTime = 0.2f; // ���뵭��ʱ��
        [SerializeField] float healthRegenPercentage = 20; // ����ֵ�ظ��ٷֱ�
        [SerializeField] float enemyHealthRegenPercentage = 20; // ��������ֵ�ظ��ٷֱ�

        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn); // ע���ɫ�����¼��ļ�����
        }

        private void Start()
        {
            if (GetComponent<Health>().IsDead())
            {
                Respawn(); // �����ɫ������������Ϸ��ʼʱ���и���
            }
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine()); // ��������Э��
        }

        private IEnumerator RespawnRoutine()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            // savingWrapper.Save();
            yield return new WaitForSeconds(respawnDelay); // �ȴ������ӳ�ʱ��
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime); // ����Ч��
            RespawnPlayer(); // ��ɫ����
            ResetEnemies(); // ���õ���
            // savingWrapper.Save();
            yield return fader.FadeIn(fadeTime); // ����Ч��
        }

        private void ResetEnemies()
        {
            foreach (AIController enemyControllers in FindObjectsOfType<AIController>())
            {
                Health health = enemyControllers.GetComponent<Health>();
                if (health && !health.IsDead())
                {
                    enemyControllers.Reset(); // ���õ��˵�״̬
                    health.Heal(health.GetMaxHealthPoints() * enemyHealthRegenPercentage / 100); // �ظ����˵�����ֵ
                }
            }
        }

        private void RespawnPlayer()
        {
            Vector3 postionDelta = respawnLocation.position - transform.position;
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position); // ����ɫ���͵�����λ��
            Health health = GetComponent<Health>();
            health.Heal(health.GetMaxHealthPoints() * healthRegenPercentage / 100); // �ظ���ɫ������ֵ
            ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            if (activeVirtualCamera.Follow == transform)
            {
                activeVirtualCamera.OnTargetObjectWarped(transform, postionDelta); // ������������ɫ���͵����
            }
        }
    }
}
