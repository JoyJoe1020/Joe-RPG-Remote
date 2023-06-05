using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    // 电影剪辑控制移除类
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player; // 玩家游戏对象

        private void Awake()
        {
            player = GameObject.FindWithTag("Player"); // 在标签为"Player"的游戏对象中查找玩家对象
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl; // 注册电影剪辑播放时的回调函数
            GetComponent<PlayableDirector>().stopped += EnableControl; // 注册电影剪辑停止时的回调函数
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl; // 取消注册电影剪辑播放时的回调函数
            GetComponent<PlayableDirector>().stopped -= EnableControl; // 取消注册电影剪辑停止时的回调函数
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction(); // 取消当前动作
            player.GetComponent<PlayerController>().enabled = false; // 禁用玩家控制
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true; // 启用玩家控制
        }
    }
}