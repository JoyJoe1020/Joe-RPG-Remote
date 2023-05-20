using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player; // 定义一个GameObject类型的变量用于存储玩家对象

        private void Start()
        {
            // 当PlayableDirector开始播放时，触发DisableControl方法
            GetComponent<PlayableDirector>().played += DisableControl;
            // 当PlayableDirector停止播放时，触发EnableControl方法
            GetComponent<PlayableDirector>().stopped += EnableControl;
            // 找到带有"Player"标签的对象并赋值给player变量
            player = GameObject.FindWithTag("Player");
        }

        // 禁用玩家控制的方法
        void DisableControl(PlayableDirector pd)
        {
            // 取消玩家当前的动作
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            // 禁用玩家控制器
            player.GetComponent<PlayerController>().enabled = false;
        }

        // 启用玩家控制的方法
        void EnableControl(PlayableDirector pd)
        {
            // 启用玩家控制器
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
