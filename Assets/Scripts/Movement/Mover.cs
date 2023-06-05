using RPG.Core; // 导入RPG核心包
using GameDevTV.Saving; // 导入GameDevTV的游戏保存包
using UnityEngine; // 导入Unity引擎的基础包
using UnityEngine.AI; // 导入Unity引擎的AI导航包
using RPG.Attributes; // 导入RPG属性包

// 定义RPG移动命名空间
namespace RPG.Movement
{
    // 定义Mover类，这是一个继承了MonoBehaviour的组件类，并实现了IAction和ISaveable接口
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        // 定义序列化字段，这些字段在Unity编辑器中可见
        [SerializeField] Transform target; // 目标Transform
        [SerializeField] float maxSpeed = 6f; // 最大移动速度
        [SerializeField] float maxNavPathLength = 40f; // 导航路径的最大长度

        // 定义私有成员变量
        NavMeshAgent navMeshAgent; // 导航代理
        Health health; // 健康状态

        // Awake方法在实例化脚本后被调用
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>(); // 获取NavMeshAgent组件
            health = GetComponent<Health>(); // 获取Health组件
        }

        // Update方法在每一帧调用
        void Update()
        {
            // 如果角色已死亡，禁用navMeshAgent
            navMeshAgent.enabled = !health.IsDead();

            // 更新Animator
            UpdateAnimator();
        }

        // 开始移动行动
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this); // 开始执行行动
            MoveTo(destination, speedFraction); // 移动到目标位置
        }

        // 判断是否能移动到目标位置
        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath(); // 创建一个新的NavMeshPath
            // 计算从当前位置到目标位置的路径
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) return false; // 如果没有路径，返回false
            if (path.status != NavMeshPathStatus.PathComplete) return false; // 如果路径不完整，返回false
            if (GetPathLength(path) > maxNavPathLength) return false; // 如果路径长度超过最大长度，返回false

            return true; // 否则，返回true
        }

        // 移动到目标位置
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            if (navMeshAgent.enabled) // 如果navMeshAgent启用
            {
                navMeshAgent.destination = destination; // 设置目标位置
                navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction); // 设置移动速度
                navMeshAgent.isStopped = false; // 恢复导航
            }
        }

        // 取消当前行动
        public void Cancel()
        {
            if (navMeshAgent.enabled) // 如果navMeshAgent启用
            {
                navMeshAgent.isStopped = true; // 停止导航
            }
        }

        // 更新Animator的参数
        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity; // 获取当前速度
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); // 将速度转换为本地坐标系
            float speed = localVelocity.z; // 获取z轴上的速度，即前进速度
            GetComponent<Animator>().SetFloat("forwardSpeed", speed); // 设置Animator的前进速度参数
        }

        // 获取路径长度
        private float GetPathLength(NavMeshPath path)
        {
            float total = 0; // 初始化总长度为0
            if (path.corners.Length < 2) return total; // 如果路径点少于2个，返回0
            for (int i = 0; i < path.corners.Length - 1; i++) // 遍历路径点
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]); // 累加相邻路径点间的距离
            }

            return total; // 返回总长度
        }

        // 捕获当前状态，用于保存游戏
        public object CaptureState()
        {
            return new SerializableVector3(transform.position); // 返回当前位置的序列化对象
        }

        // 恢复状态，用于加载游戏
        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state; // 将状态转换为SerializableVector3对象
            navMeshAgent.enabled = false; // 禁用navMeshAgent
            transform.position = position.ToVector(); // 设置当前位置
            navMeshAgent.enabled = true; // 启用navMeshAgent
            GetComponent<ActionScheduler>().CancelCurrentAction(); // 取消当前行动
        }
    }
}
