using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个名为FollowCamera的类，继承自MonoBehaviour，用于处理摄像机跟随目标的功能
public class FollowCamera : MonoBehaviour
{
    // 定义一个Transform类型的变量target，用于存储跟随的目标对象
    [SerializeField] Transform target;

    void Update()
    {
        // 将摄像机的位置设置为目标对象的位置，实现摄像机跟随目标的功能
        transform.position = target.position;
    }
}
