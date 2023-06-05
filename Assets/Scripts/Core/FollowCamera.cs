using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target; // 跟随的目标物体的Transform组件

        void LateUpdate()
        {
            transform.position = target.position; // 将物体的位置设置为目标物体的位置
        }
    }
}
