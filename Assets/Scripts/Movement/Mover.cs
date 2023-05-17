// 引入Unity引擎命名空间
using UnityEngine;
// 引入UnityEngine.AI命名空间，用于处理导航网格相关功能
using UnityEngine.AI;
// 引入RPG.Core命名空间，用于处理核心逻辑
using RPG.Core;

// 定义一个名为RPG.Movement的命名空间，以便组织相关的代码
namespace RPG.Movement
{
    // 定义一个名为Mover的类，继承自MonoBehaviour，并实现IAction接口，用于处理游戏对象的移动
    public class Mover : MonoBehaviour, IAction
    {
        // 使用SerializeField属性，使得在Unity编辑器的Inspector面板中可见并可编辑
        // 定义一个Transform类型的变量target，用于存储目标位置（已不再使用，可移除）
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        // 定义一个NavMeshAgent类型的变量navMeshAgent，用于操作NavMeshAgent组件
        NavMeshAgent navMeshAgent;

        // 定义一个Health类型的变量，用于存储游戏对象的生命值信息
        Health health;

        // 在Start方法中获取NavMeshAgent组件并赋值给navMeshAgent变量
        private void Start()
        {
            // 获取游戏对象上的NavMeshAgent组件，并将其赋值给navMeshAgent变量
            navMeshAgent = GetComponent<NavMeshAgent>();
            // 获取游戏对象上的Health组件，并将其赋值给health变量
            health = GetComponent<Health>();
        }

        // 在每帧更新时调用Update方法
        void Update()
        {
            // 在Update方法中，通过判断角色是否死亡来决定是否启用navMeshAgent组件
            navMeshAgent.enabled = !health.IsDead();

            // 调用UpdateAnimator方法，更新动画参数
            UpdateAnimator();
        }

        // 定义一个公共方法StartMoveAction，用于开始移动操作
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            // 调用ActionScheduler组件的StartAction方法，开始当前的移动行为
            GetComponent<ActionScheduler>().StartAction(this);

            MoveTo(destination, speedFraction);
        }

        // 定义一个公共方法MoveTo，用于设置游戏对象的移动目标位置
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            // 设置NavMeshAgent组件的目标位置为传入的destination参数
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            // 设置navMeshAgent为非停止状态，使角色开始移动
            navMeshAgent.isStopped = false;
        }

        // 定义一个公共方法Cancel，实现IAction接口中的Cancel方法，用于停止角色移动
        public void Cancel()
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
