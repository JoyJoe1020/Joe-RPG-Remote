// 引入所需的命名空间
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

// 在RPG.SceneManagement命名空间下定义一个名为Portal的类，继承自MonoBehaviour
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        // 新增一个枚举类型用于标识传送点的目标
        enum DestinationIdentifier
        {
            A, B, C, D
        }

        [SerializeField] int sceneToLoad = -1; // 用于在Inspector中设置要加载的场景的索引
        [SerializeField] Transform spawnPoint; // 玩家出生点的Transform组件，用于设置玩家在新场景中的位置和旋转
        [SerializeField] DestinationIdentifier destination; // 目标标识符，用于识别不同的传送点
        [SerializeField] float fadeOutTime = 1f; // 进行场景淡出的时间
        [SerializeField] float fadeInTime = 2f; // 进行场景淡入的时间
        [SerializeField] float fadeWaitTime = 0.5f; // 场景淡入淡出之间的等待时间

        // 当有其他物体进入该游戏对象的触发器时调用
        private void OnTriggerEnter(Collider other)
        {
            // 如果进入触发器的物体标签为"Player"，则启动场景切换的协程
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        // 场景切换的协程
        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("没有设置要加载的场景");
                yield break;
            }

            DontDestroyOnLoad(gameObject); // 在切换场景时保持此游戏对象不被销毁

            Fader fader = FindObjectOfType<Fader>(); // 找到场景中的Fader对象

            yield return fader.FadeOut(fadeOutTime); // 调用FadeOut方法进行场景淡出

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad); // 异步加载指定的场景，并等待加载完成

            wrapper.Load();

            Portal otherPortal = GetOtherPortal(); // 获取目标场景的Portal对象
            UpdatePlayer(otherPortal); // 更新玩家的位置和旋转

            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime); // 等待一段时间
            yield return fader.FadeIn(fadeInTime); // 调用FadeIn方法进行场景淡入

            Destroy(gameObject); // 销毁此游戏对象
        }

        // 更新玩家的位置和旋转
        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player"); // 查找标签为"Player"的游戏对象
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position; // 设置玩家的位置为目标出生点的位置
            player.transform.rotation = otherPortal.spawnPoint.rotation; // 设置玩家的旋转为目标出生点的旋转
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        // 获取目标场景的Portal对象
        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>()) // 遍历当前场景中所有的Portal对象
            {
                if (portal == this) continue; // 如果是当前的Portal对象，跳过当前迭代

                if (portal.destination != destination) continue; // 如果目标Portal的destination和当前的不同，跳过当前迭代

                return portal; // 返回找到的其他Portal对象
            }

            return null; // 如果没有找到其他的Portal对象，返回null
        }
    }
}
