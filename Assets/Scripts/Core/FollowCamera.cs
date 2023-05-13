using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定义一个名为RPG.Core的命名空间，以便组织相关的代码
namespace RPG.Core
{
    // 定义一个名为FollowCamera的类，继承自MonoBehaviour，用于处理摄像机跟随目标的功能
    public class FollowCamera : MonoBehaviour
    {
        // 定义一个Transform类型的变量target，用于存储跟随的目标对象
        [SerializeField] Transform target;

        // 添加一个Vector3类型的变量offset，用于存储摄像机相对于目标的位置偏移
        [SerializeField] Vector3 offset = new Vector3(0, 10, -10); 

        void Update()
        {
            // 将摄像机的位置设置为目标对象的位置，实现摄像机跟随目标的功能
            transform.position = target.position;

            // 调整摄像机的角度，使其俯视目标
            transform.LookAt(target);
        }
    }
}
