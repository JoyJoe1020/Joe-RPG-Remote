using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    // 投射物类，用于处理投射物的行为
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1; // 投射物的速度
        [SerializeField] bool isHoming = true; // 投射物是否具有追踪目标的功能
        [SerializeField] GameObject hitEffect = null; // 命中目标时的特效
        [SerializeField] float maxLifeTime = 10; // 投射物的最大生存时间
        [SerializeField] GameObject[] destroyOnHit = null; // 命中目标后需要销毁的游戏对象数组
        [SerializeField] float lifeAfterImpact = 2; // 投射物命中目标后的生存时间
        [SerializeField] UnityEvent onHit; // 当投射物命中目标时的事件

        Health target = null; // 攻击的目标
        Vector3 targetPoint; // 攻击的目标点
        GameObject instigator = null; // 攻击的发起者
        float damage = 0; // 攻击造成的伤害

        // 在投射物实例化后首次更新前调用
        private void Start()
        {
            transform.LookAt(GetAimLocation()); // 让投射物面向攻击的目标点
        }

        // 每帧调用一次
        void Update()
        {
            // 如果设置了攻击目标，且投射物具有追踪功能，并且目标还未死亡
            if (target != null && isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation()); // 让投射物面向攻击的目标
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // 让投射物向前移动
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, target);
        }

        public void SetTarget(Vector3 targetPoint, GameObject instigator, float damage)
        {
            SetTarget(instigator, damage, null, targetPoint);
        }

        public void SetTarget(GameObject instigator, float damage, Health target = null, Vector3 targetPoint = default)
        {
            this.target = target; // 设置攻击目标
            this.targetPoint = targetPoint; // 设置目标点
            this.damage = damage; // 设置伤害值
            this.instigator = instigator; // 设置攻击者

            Destroy(gameObject, maxLifeTime); // 设置最大存活时间
        }

        // 获取攻击的目标点
        private Vector3 GetAimLocation()
        {
            if (target == null) // 如果没有设置攻击目标
            {
                return targetPoint; // 直接返回攻击的目标点
            }
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null) // 如果目标没有胶囊碰撞体组件
            {
                return target.transform.position; // 返回目标的位置作为攻击目标点
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2; // 否则返回目标胶囊碰撞体的中心点作为攻击目标点
        }

        // 当投射物的触发器碰到其他游戏对象的碰撞器时调用
        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>(); // 获取被碰撞游戏对象的生命值组件
            if (target != null && health != target) return; // 如果被碰撞游戏对象不是攻击的目标，则直接返回
            if (health == null || health.IsDead()) return; // 如果被碰撞游戏对象没有生命值组件，或者已经死亡，则直接返回
            if (other.gameObject == instigator) return; // 如果被碰撞游戏对象是攻击的发起者，则直接返回

            health.TakeDamage(instigator, damage); // 让被碰撞游戏对象受到攻击的伤害

            speed = 0; // 停止投射物的移动

            onHit.Invoke(); // 触发投射物命中目标的事件

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation); // 在攻击目标点生成命中特效
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy); // 销毁命中目标后需要销毁的游戏对象
            }

            Destroy(gameObject, lifeAfterImpact); // 在命中目标后一段时间后销毁投射物
        }

    }
}