using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    // 电影剪辑触发器类
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false; // 是否已经触发过

        private void OnTriggerEnter(Collider other)
        {
            if (!alreadyTriggered && other.gameObject.tag == "Player") // 如果还未触发且碰撞对象的标签是"Player"
            {
                alreadyTriggered = true; // 设置已触发标志为true
                GetComponent<PlayableDirector>().Play(); // 播放电影剪辑
            }
        }
    }
}