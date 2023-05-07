using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 定义一个名为Mover的类，继承自MonoBehaviour，用于处理游戏对象的移动
public class Mover : MonoBehaviour
{
    // 使用SerializeField属性，使得在Unity编辑器的Inspector面板中可见并可编辑
    // 定义一个Transform类型的变量target，用于存储目标位置
    [SerializeField] Transform target;

    // Update is called once per frame
    // Update方法在每一帧中被调用
    void Update()
    {
        // 获取当前游戏对象的NavMeshAgent组件
        // 设置NavMeshAgent组件的目标位置为target变量所指向的Transform的位置
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
