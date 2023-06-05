using UnityEngine;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Combat
{
    // 投射物类，用于处理投射物的行为
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1; // 投射物速度
        [SerializeField] bool isHoming = true; // 是否追踪目标
        [SerializeField] GameObject hitEffect = null; // 命中效果
        [SerializeField] float maxLifeTime = 10; // 最大存活时间
        [SerializeField] GameObject[] destroyOnHit = null; // 命中后销毁的对象
        [SerializeField] float lifeAfterImpact = 2; // 命中后的存活时间
        [SerializeField] UnityEvent onHit; // 命中事件

        Health target = null; // 攻击目标
        Vector3 targetPoint; // 目标点
        GameObject instigator = null; // 攻击者
        float damage = 0; // 伤害值

        private void Start()
        {
            transform.LookAt(GetAimLocation()); // 面向目标点
        }

        void Update()
        {
            if (target != null && isHoming && !target.IsDead()) // 如果有目标且追踪目标
            {
                transform.LookAt(GetAimLocation()); // 面向目标
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // 前进
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

        private Vector3 GetAimLocation()
        {
            if (target == null)
            {
                return targetPoint; // 返回目标点
            }
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position; // 返回目标位置
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2; // 返回目标高度
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponent<Health>(); // 获取生命值组件
            if (target != null && health != target) return; // 如果目标不为空且不是攻击目标，则返回
            if (health == null || health.IsDead()) return; // 如果没有生命值组件或目标已死亡，则返回
            if (other.gameObject == instigator) return; // 如果是攻击者本身，则返回
            health.TakeDamage(instigator, damage); // 对目标造成伤害

            speed = 0; // 停止移动

            onHit.Invoke(); // 触发命中事件

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation); // 生成命中效果
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy); // 销毁命中后的对象
            }

            Destroy(gameObject, lifeAfterImpact); // 销毁自身

        }

    }
}