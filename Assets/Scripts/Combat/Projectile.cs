using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f; // 投射物的移动速度
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        Health target = null; // 投射物的目标
        float damage = 0; // 投射物对目标的伤害值

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        // Update方法在每一帧中都会被调用
        void Update()
        {
            // 如果没有目标，则返回
            if (target == null) return;

            if (isHoming && !target.IsDead())
            {
                // 将投射物朝向目标位置
                transform.LookAt(GetAimLocation());
            }
            // 投射物向前移动
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // SetTarget方法用于设置投射物的目标和对目标的伤害值
        public void SetTarget(Health target, float damage)
        {
            // 设置投射物的目标
            this.target = target;
            // 设置投射物对目标的伤害值
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        // GetAimLocation方法用于获取投射物的目标位置
        private Vector3 GetAimLocation()
        {
            // 尝试获取目标上的CapsuleCollider组件
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            // 如果目标上没有CapsuleCollider组件
            if (targetCapsule == null)
            {
                // 则返回目标的位置
                return target.transform.position;
            }
            // 否则，返回目标位置上方的位置，这是为了考虑到CapsuleCollider的高度
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        // OnTriggerEnter方法在投射物进入其它Collider时被调用
        private void OnTriggerEnter(Collider other)
        {
            // 如果进入的Collider的Health组件不是投射物的目标，则返回
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            // 对目标造成伤害
            target.TakeDamage(damage);

            speed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }

}