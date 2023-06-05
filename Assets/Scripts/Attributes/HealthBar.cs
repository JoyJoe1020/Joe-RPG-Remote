using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    // 生命值条类，用于显示生命值条
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null; // 生命值组件
        [SerializeField] RectTransform foreground = null; // 前景矩形变换组件
        [SerializeField] Canvas rootCanvas = null; // 根画布

        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFraction(), 0)
            || Mathf.Approximately(healthComponent.GetFraction(), 1))
            {
                rootCanvas.enabled = false; // 如果生命值分数接近0或1，隐藏生命值条
                return;
            }

            rootCanvas.enabled = true; // 否则显示生命值条
            foreground.localScale = new Vector3(healthComponent.GetFraction(), 1, 1); // 根据生命值分数调整前景矩形的缩放
        }
    }
}