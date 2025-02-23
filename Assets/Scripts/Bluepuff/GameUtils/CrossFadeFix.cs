﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

namespace Bluepuff.Utils
{
    public static class CrossFadeFix
    {

        public static async UniTask CrossFadeAlphaAsync(this Image img, float alpha, float duration)
        {
            Color currentColor = img.color;

            Color visibleColor = img.color;
            visibleColor.a = alpha;


            float counter = 0;
            while (counter < duration)
            {
                counter += Time.deltaTime;
                img.color = Color.Lerp(currentColor, visibleColor, counter / duration);
                await UniTask.Yield();
            }

        }
        public static void CrossFadeAlphaWithCallBack(this Image img, float alpha, float duration, System.Action action)
        {
            MonoBehaviour mnbhvr = img.GetComponent<MonoBehaviour>();
            mnbhvr.StartCoroutine(CrossFadeAlphaCOR(img, alpha, duration, action));
        }

        public static IEnumerator CrossFadeAlphaCOR(Image img, float alpha, float duration, System.Action action)
        {
            Color currentColor = img.color;

            Color visibleColor = img.color;
            visibleColor.a = alpha;


            float counter = 0;

            while (counter < duration)
            {
                counter += Time.deltaTime;
                img.color = Color.Lerp(currentColor, visibleColor, counter / duration);
                yield return null;
            }

            //Done. Execute callback
            action.Invoke();
        }
    }
}
