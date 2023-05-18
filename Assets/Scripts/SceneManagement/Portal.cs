// 引入所需的命名空间
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 在RPG.SceneManagement命名空间下定义一个名为Portal的类，继承自MonoBehaviour
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D
        }

        // 用于在Inspector中设置要加载的场景的索引
        [SerializeField] int sceneToLoad = -1;
        // 出生点的Transform组件，用于设置玩家在新场景中的位置和旋转
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

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
                Debug.LogError(("没有设置要加载的场景"));
                yield break;
            }

            // 在切换场景时保持此游戏对象不被销毁
            DontDestroyOnLoad(gameObject);
            // 异步加载指定的场景，并等待加载完成
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // 获取目标场景的Portal对象
            Portal otherPortal = GetOtherPortal();
            // 更新玩家的位置和旋转
            UpdatePlayer(otherPortal);

            // 销毁此游戏对象
            // 因为已经完成了场景切换和玩家的位置更新，所以这个Portal对象的任务已经完成，可以被销毁
            Destroy(gameObject);
        }

        // 更新玩家的位置和旋转
        private void UpdatePlayer(Portal otherPortal)
        {
            // 查找标签为"Player"的游戏对象
            GameObject player = GameObject.FindWithTag("Player");
            // 设置玩家的位置为目标出生点的位置
            player.transform.position = otherPortal.spawnPoint.position;
            // 设置玩家的旋转为目标出生点的旋转
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        // 获取目标场景的Portal对象
        private Portal GetOtherPortal()
        {
            // 遍历当前场景中所有的Portal对象
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                // 如果是当前的Portal对象，跳过当前迭代
                if (portal == this) continue;

                if (portal.destination != destination) continue;

                // 返回找到的其他Portal对象
                return portal;
            }

            // 如果没有找到其他的Portal对象，返回null
            return null;
        }
    }
}
