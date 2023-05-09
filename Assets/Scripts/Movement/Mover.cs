using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.combat;
using RPG.Core;

// 定义一个名为RPG.Movement的命名空间，以便组织相关的代码
namespace RPG.Movement
{
    // 定义一个名为Mover的类，继承自MonoBehaviour，用于处理游戏对象的移动
    public class Mover : MonoBehaviour
    {
        // 使用SerializeField属性，使得在Unity编辑器的Inspector面板中可见并可编辑
        // 定义一个Transform类型的变量target，用于存储目标位置（已不再使用，可移除）
        [SerializeField] Transform target;

        // 定义一个NavMeshAgent类型的变量navMeshAgent，用于操作NavMeshAgent组件
        NavMeshAgent navMeshAgent;

        // 在Start方法中获取NavMeshAgent组件并赋值给navMeshAgent变量
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // 在每帧更新时调用Update方法
        void Update()
        {
            // 调用UpdateAnimator方法，更新动画参数
            UpdateAnimator();
        }

        // 定义一个公共方法StartMoveAction，用于开始移动操作
        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // 调用Fighter组件的Cancel方法，取消攻击
            GetComponent<Fighter>().Cancel();
            // 调用MoveTo方法，设置目标位置并开始移动
            MoveTo(destination);
        }

        // 定义一个公共方法MoveTo，用于设置游戏对象的移动目标位置
        public void MoveTo(Vector3 destination)
        {
            // 设置NavMeshAgent组件的目标位置为传入的destination参数
            navMeshAgent.destination = destination;
            // 设置navMeshAgent为非停止状态，使角色开始移动
            navMeshAgent.isStopped = false;
        }

        // 定义一个公共方法Stop，用于停止角色移动
        public void Stop()
        {
            // 设置navMeshAgent为停止状态
            navMeshAgent.isStopped = true;
        }

        // 定义一个私有方法UpdateAnimator，用于更新动画控制器的参数
        private void UpdateAnimator()
        {
            // 获取NavMeshAgent组件的速度，并将其存储在velocity变量中
            Vector3 velocity = navMeshAgent.velocity;
            // 将速度从世界坐标系转换为本地坐标系，并将其存储在localVelocity变量中
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            // 获取localVelocity的z分量，并将其作为速度值
            float speed = localVelocity.z;
            // 获取Animator组件，并设置其"forwardSpeed"参数为计算得到的速度值
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}