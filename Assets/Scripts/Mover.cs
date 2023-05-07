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

    // 定义一个Ray类型的变量lastRay，用于存储上一次鼠标点击产生的射线
    Ray lastRay;

    // Update is called once per frame
    // Update方法在每一帧中被调用
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // 根据鼠标在屏幕上的位置计算一条射线，并将其存储在lastRay变量中
            //根据鼠标在屏幕上的位置计算一条从摄像机发出的射线
            lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // 在场景中绘制lastRay变量表示的射线，方向乘以100以增加射线长度
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);

        // 获取当前游戏对象的NavMeshAgent组件
        // 设置NavMeshAgent组件的目标位置为target变量所指向的Transform的位置
        GetComponent<NavMeshAgent>().destination = target.position;
    }
}
