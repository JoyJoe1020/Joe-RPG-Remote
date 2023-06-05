using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        //定义画布组
        CanvasGroup canvasGroup;
        //定义当前激活的淡出协程
        Coroutine currentActiveFade = null;

        //在Awake阶段获取画布组组件
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        //立即完成淡出
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        //按指定的时间完成淡出
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        //按指定的时间完成淡入
        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        //按指定的时间完成淡入或淡出到目标透明度
        public Coroutine Fade(float target, float time)
        {
            //如果有协程正在执行，则停止当前协程
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            //启动淡入淡出协程
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        //淡入淡出协程
        private IEnumerator FadeRoutine(float target, float time)
        {
            //渐渐改变画布组的透明度直到达到目标透明度
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }
    }
}
