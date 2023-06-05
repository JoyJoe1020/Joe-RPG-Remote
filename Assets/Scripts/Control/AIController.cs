using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using RPG.Attributes;
using GameDevTV.Utils;
using UnityEngine.AI;

namespace RPG.Control
{
    // AI控制器，用于控制敌人的行为
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; // 追击距离
        [SerializeField] float suspicionTime = 3f; // 怀疑时间
        [SerializeField] float agroCooldownTime = 5f; // 攻击冷却时间
        [SerializeField] PatrolPath patrolPath; // 巡逻路径
        [SerializeField] float waypointTolerance = 1f; // 路径点容差
        [SerializeField] float waypointDwellTime = 3f; // 停留时间
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f; // 巡逻速度系数
        [SerializeField] float shoutDistance = 5f; // 喊叫距离

        Fighter fighter;
        Health health;
        Mover mover;
        GameObject player;

        LazyValue<Vector3> guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        float timeSinceAggrevated = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = new LazyValue<Vector3>(GetGuardPosition);
            guardPosition.ForceInit();
        }

        // 重置AI状态
        public void Reset()
        {
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.Warp(guardPosition.value);
            timeSinceLastSawPlayer = Mathf.Infinity;
            timeSinceArrivedAtWaypoint = Mathf.Infinity;
            timeSinceAggrevated = Mathf.Infinity;
            currentWaypointIndex = 0;
        }

        // 获取守卫位置
        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (health.IsDead()) return;

            if (IsAggrevated() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        // 激怒AI
        public void Aggrevate()
        {
            timeSinceAggrevated = 0;
        }

        // 更新计时器
        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
            timeSinceAggrevated += Time.deltaTime;
        }

        // 巡逻行为
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        // 是否到达路径点
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        // 切换路径点
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        // 获取当前路径点
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        // 怀疑行为
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        // 攻击行为
        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);

            AggrevateNearbyEnemies();
        }

        // 激怒附近的敌人
        private void AggrevateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            foreach (RaycastHit hit in hits)
            {
                AIController ai = hit.collider.GetComponent<AIController>();
                if (ai == null) continue;

                ai.Aggrevate();
            }
        }

        // 是否激怒
        private bool IsAggrevated()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance || timeSinceAggrevated < agroCooldownTime;
        }

        // Unity调用的绘制Gizmos函数
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
