using System; // 导入System命名空间
using System.Collections;
using Cinemachine;
using RPG.Attributes; // 导入属性系统命名空间
using RPG.SceneManagement; // 导入场景管理系统命名空间
using UnityEngine;
using UnityEngine.AI; // 导入Unity导航系统命名空间

namespace RPG.Control
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] Transform respawnLocation; // 复活位置
        [SerializeField] float respawnDelay = 3; // 复活延迟时间
        [SerializeField] float fadeTime = 0.2f; // 淡入淡出时间
        [SerializeField] float healthRegenPercentage = 20; // 生命值回复百分比
        [SerializeField] float enemyHealthRegenPercentage = 20; // 敌人生命值回复百分比

        private void Awake()
        {
            GetComponent<Health>().onDie.AddListener(Respawn); // 注册角色死亡事件的监听器
        }

        private void Start()
        {
            if (GetComponent<Health>().IsDead())
            {
                Respawn(); // 如果角色已死亡，在游戏开始时进行复活
            }
        }

        private void Respawn()
        {
            StartCoroutine(RespawnRoutine()); // 启动复活协程
        }

        private IEnumerator RespawnRoutine()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            // savingWrapper.Save();
            yield return new WaitForSeconds(respawnDelay); // 等待复活延迟时间
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeOut(fadeTime); // 淡出效果
            RespawnPlayer(); // 角色复活
            ResetEnemies(); // 重置敌人
            // savingWrapper.Save();
            yield return fader.FadeIn(fadeTime); // 淡入效果
        }

        private void ResetEnemies()
        {
            foreach (AIController enemyControllers in FindObjectsOfType<AIController>())
            {
                Health health = enemyControllers.GetComponent<Health>();
                if (health && !health.IsDead())
                {
                    enemyControllers.Reset(); // 重置敌人的状态
                    health.Heal(health.GetMaxHealthPoints() * enemyHealthRegenPercentage / 100); // 回复敌人的生命值
                }
            }
        }

        private void RespawnPlayer()
        {
            Vector3 postionDelta = respawnLocation.position - transform.position;
            GetComponent<NavMeshAgent>().Warp(respawnLocation.position); // 将角色传送到复活位置
            Health health = GetComponent<Health>();
            health.Heal(health.GetMaxHealthPoints() * healthRegenPercentage / 100); // 回复角色的生命值
            ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
            if (activeVirtualCamera.Follow == transform)
            {
                activeVirtualCamera.OnTargetObjectWarped(transform, postionDelta); // 处理相机跟随角色传送的情况
            }
        }
    }
}
