using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup; // CanvasGroup组件，用于控制Canvas的淡入淡出

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();  // 在Start方法中获取CanvasGroup组件
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
        
        // 淡出协程
        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time; // 逐渐增加CanvasGroup的透明度直到完全不透明
                yield return null;
            }
        }

        // 淡入协程
        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time; // 逐渐减少CanvasGroup的透明度直到完全透明
                yield return null;
            }
        }
    }
}
