using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; // 引入PlayableDirector类，管理时间线（timeline）的播放

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false; // 定义一个布尔值，标记电影剪辑是否已经触发过

        private void OnTriggerEnter(Collider other)
        {
            // 判断当前的电影剪辑是否已经触发过，以及触发者是否是玩家
            // 如果电影剪辑未触发过，并且触发者是玩家
            if (!alreadyTriggered && other.gameObject.tag == "Player")
            {
                alreadyTriggered = true; // 将触发标记设置为已触发
                GetComponent<PlayableDirector>().Play(); // 播放电影剪辑
            }
        }
    }
}
