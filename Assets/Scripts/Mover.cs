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

    void Update()
    {
        // 当鼠标左键被按下时
        if(Input.GetMouseButtonDown(0))
        {
            // 调用MoveToCursor方法，使游戏对象移动到鼠标点击的位置
            MoveToCursor();
        }

        // 调用UpdateAnimator方法，更新动画参数
        UpdateAnimator();
    }

    // 定义一个私有方法MoveToCursor，用于计算鼠标点击的位置并移动游戏对象
    private void MoveToCursor()
    {
        // 根据鼠标在屏幕上的位置计算一条射线，并将其存储在ray变量中
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 定义一个RaycastHit类型的变量hit，用于存储射线与物体的交点信息
        RaycastHit hit;
        // 使用Physics.Raycast方法检测射线是否与场景中的物体发生碰撞，并将结果存储在hasHit变量中
        bool hasHit = Physics.Raycast(ray, out hit);
        // 如果射线与物体发生碰撞（hasHit为true）
        if (hasHit)
        {
            // 获取当前游戏对象的NavMeshAgent组件
            // 设置NavMeshAgent组件的目标位置为射线与物体的交点（hit.point）
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }

    // 定义一个私有方法UpdateAnimator，用于更新动画控制器的参数
    private void UpdateAnimator()
    {
        // 获取NavMeshAgent组件的速度，并将其存储在velocity变量中
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        // 将速度从世界坐标系转换为本地坐标系，并将其存储在localVelocity变量中
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        // 获取localVelocity的z分量，并将其作为速度值
        float speed = localVelocity.z;
        // 获取Animator组件，并设置其"forwardSpeed"参数为计算得到的速度值
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
