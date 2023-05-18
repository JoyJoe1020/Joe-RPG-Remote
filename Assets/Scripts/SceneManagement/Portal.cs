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
        // 用于在Inspector中设置要加载的场景的索引
        [SerializeField] int sceneToLoad = -1;
        // 出生点的Transform组件，用于设置玩家在新场景中的位置和旋转
        [SerializeField] Transform spawnPoint;

        // 当有其他物体进入该游戏对象的触发器时调用
        private void OnTriggerEnter(Collider other)
        {
            // 如果进入触发器的物体标签为"Player"，则启动场景切换的协程
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        // 定义名为Transition的协程，用于处理场景的切换
        private IEnumerator Transition()
        {
            // 调用DontDestroyOnLoad方法，该方法的功能是让指定的游戏对象在加载新场景时不被自动销毁
            // 这里传入的是gameObject，表示当前的游戏对象，即包含Portal组件的游戏对象
            DontDestroyOnLoad(gameObject);

            // 调用SceneManager.LoadSceneAsync方法，该方法的功能是异步加载指定的场景
            // 这里传入的是sceneToLoad，表示要加载的场景的索引
            // LoadSceneAsync方法会立即返回一个AsyncOperation对象，但是新场景的加载会在后台进行
            // 新场景加载完成后，AsyncOperation的isDone属性会变为true
            // 这里使用yield return将该AsyncOperation对象返回，这样可以在新场景加载完成前挂起协程的执行
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // 调用GetOtherPortal方法，获取目标场景的Portal对象
            // GetOtherPortal方法的功能是在当前场景中查找所有的Portal对象，并返回第一个不是当前对象的Portal对象
            Portal otherPortal = GetOtherPortal();

            // 调用UpdatePlayer方法，更新玩家的位置和旋转
            // UpdatePlayer方法的功能是查找标签为"Player"的游戏对象，然后将其位置和旋转设置为目标出生点的位置和旋转
            UpdatePlayer(otherPortal);

            // 调用Destroy方法，销毁当前的游戏对象
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

                // 返回找到的其他Portal对象
                return portal;
            }

            // 如果没有找到其他的Portal对象，返回null
            return null;
        }
    }
}
