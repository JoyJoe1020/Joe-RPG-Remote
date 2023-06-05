using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        //���廭����
        CanvasGroup canvasGroup;
        //���嵱ǰ����ĵ���Э��
        Coroutine currentActiveFade = null;

        //��Awake�׶λ�ȡ���������
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        //������ɵ���
        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }

        //��ָ����ʱ����ɵ���
        public Coroutine FadeOut(float time)
        {
            return Fade(1, time);
        }

        //��ָ����ʱ����ɵ���
        public Coroutine FadeIn(float time)
        {
            return Fade(0, time);
        }

        //��ָ����ʱ����ɵ���򵭳���Ŀ��͸����
        public Coroutine Fade(float target, float time)
        {
            //�����Э������ִ�У���ֹͣ��ǰЭ��
            if (currentActiveFade != null)
            {
                StopCoroutine(currentActiveFade);
            }
            //�������뵭��Э��
            currentActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentActiveFade;
        }

        //���뵭��Э��
        private IEnumerator FadeRoutine(float target, float time)
        {
            //�����ı仭�����͸����ֱ���ﵽĿ��͸����
            while (!Mathf.Approximately(canvasGroup.alpha, target))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, Time.unscaledDeltaTime / time);
                yield return null;
            }
        }
    }
}
