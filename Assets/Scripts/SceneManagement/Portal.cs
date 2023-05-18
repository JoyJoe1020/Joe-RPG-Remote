// 引入所需的命名空间
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
            // 在切换场景时保持此游戏对象不被销毁
            DontDestroyOnLoad(gameObject);
            // 异步加载指定的场景，并等待加载完成
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            // 当场景加载完成后，打印一条消息
            print("Scene Loaded");
            // 销毁此游戏对象
            Destroy(gameObject);
        }
    }
}
