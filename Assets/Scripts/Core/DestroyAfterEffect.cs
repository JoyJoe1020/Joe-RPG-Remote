using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null; // 要销毁的目标对象

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetToDestroy != null)
                {
                    Destroy(targetToDestroy); // 如果目标对象不为空，销毁目标对象
                }
                else
                {
                    Destroy(gameObject); // 否则销毁自身
                }
            }
        }
    }
}
