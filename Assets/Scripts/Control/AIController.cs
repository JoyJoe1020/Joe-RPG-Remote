// 引入相关的库
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

// 在RPG.Control命名空间下定义一个AIController类，这个类继承自MonoBehaviour，用于处理AI角色的行为逻辑
namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        // 定义一系列的可序列化字段，用于在Unity编辑器中设置AI角色的行为参数
        [SerializeField] float chaseDistance = 5f;  // AI追击玩家的最大距离
        [SerializeField] float suspicionTime = 3f;  // AI在失去玩家视线后的怀疑时间，超过这个时间后将停止追击
        [SerializeField] PatrolPath patrolPath;  // AI的巡逻路线
        [SerializeField] float waypointTolerance = 1f;  // AI到达巡逻点的容忍距离，小于这个距离时认为已经到达巡逻点
        [SerializeField] float waypointDwellTime = 3f;  // AI在每个巡逻点停留的时间
        [Range(0,1)]
        // patrolSpeedFraction 是一个可序列化字段，表示AI角色在巡逻过程中的移动速度占其最大移动速度的比例。
        // 这个值必须在0到1之间，其中0表示AI不移动，1表示AI以最大速度移动。默认值设置为0.2，表示AI在巡逻时的移动速度为其最大速度的20%。
        [SerializeField] float patrolSpeedFraction = 0.2f;

        // 定义一些组件和游戏对象的引用
        Fighter fighter;  // Fighter组件的引用，用于处理战斗行为
        Health health;  // Health组件的引用，用于获取AI角色的生命状态
        Mover mover;  // Mover组件的引用，用于控制AI角色的移动
        GameObject player;  // 玩家游戏对象的引用

        // 定义一些用于控制AI行为的变量
        Vector3 guardPosition;  // AI的守卫位置，即AI在没有玩家进入追击范围时的待命位置
        float timeSinceLastSawPlayer = Mathf.Infinity;  // AI上次看到玩家后经过的时间
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;  // AI到达当前巡逻点后经过的时间
        int currentWaypointIndex = 0;  // AI当前的巡逻点索引

        // 在Start方法中初始化组件和游戏对象的引用，以及守卫位置
        private void Start()
        {
            fighter = GetComponent<Fighter>();  // 获取Fighter组件的引用
            health = GetComponent<Health>();  // 获取Health组件的引用
            mover = GetComponent<Mover>();  // 获取Mover组件的引用
            player = GameObject.FindWithTag("Player");  // 查找标签为"Player"的游戏对象

            guardPosition = transform.position;  // 获取AI角色当前的位置作为守卫位置
        }

        // 在Update方法中根据玩家的位置和AI角色的状态决定AI的行为
        private void Update()
        {
            // 如果AI角色已经死亡，则直接返回，不执行后续的行为逻辑
            if (health.IsDead()) { return; }

            // 判断玩家是否在AI的追击范围内，以及AI是否可以攻击玩家
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                // 如果满足条件，则执行攻击行为
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)  // 如果AI失去玩家视线的时间小于怀疑时间，则执行怀疑行为
            {
                SuspicionBehaviour();
            }
            else  // 如果都不满足，则执行巡逻行为
            {
                PatrolBehaviour();
            }

            // 更新AI的计时器
            UpdateTimers();
        }

        // 更新AI的计时器
        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;  // 更新AI上次看到玩家后经过的时间
            timeSinceArrivedAtWaypoint += Time.deltaTime;  // 更新AI到达当前巡逻点后经过的时间
        }

        // 控制AI的巡逻行为
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;  // 默认的下一个位置为守卫位置

            // 如果设置了巡逻路线
            if (patrolPath != null)
            {
                // 如果AI已经到达当前的巡逻点
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;  // 重置AI到达当前巡逻点后经过的时间
                    CycleWaypoint();  // 获取下一个巡逻点
                }
                nextPosition = GetCurrentWaypoint();  // 设置下一个位置为当前巡逻点
            }

            // 如果AI在当前巡逻点停留的时间超过了设定的值，则开始向下一个位置移动
            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                // 调用Mover组件的StartMoveAction方法，使AI角色开始向下一个巡逻点移动。
                // 第一个参数是目标位置，第二个参数是移动速度占最大速度的比例（即patrolSpeedFraction）。
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        // 判断AI是否已经到达当前的巡逻点
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());  // 计算AI到当前巡逻点的距离
            return distanceToWaypoint < waypointTolerance;  // 如果距离小于设定的容忍距离，则认为已经到达
        }

        // 获取下一个巡逻点
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);  // 更新当前巡逻点的索引
        }

        // 获取当前的巡逻点
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);  // 返回当前巡逻点的位置
        }

        // 控制AI的怀疑行为
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();  // 取消AI当前的行为
        }

        // 控制AI的攻击行为
        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;  // 重置AI上次看到玩家后经过的时间
            fighter.Attack(player);  // 让AI攻击玩家
        }

        // 判断玩家是否在AI的攻击范围内
        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);  // 计算玩家与AI之间的距离
            return distanceToPlayer < chaseDistance;  // 如果距离小于追击距离，则认为玩家在攻击范围内
        }

        // 当AI角色被选中时，在Unity编辑器中绘制表示攻击范围的球体
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;  // 设置球体颜色为蓝色
            Gizmos.DrawWireSphere(transform.position, chaseDistance);  // 绘制一个以角色位置为中心，追击距离为半径的线框球体
        }
    }
}
