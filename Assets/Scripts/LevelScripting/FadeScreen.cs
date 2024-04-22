using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{

    public static FadeScreen instance;
    private CanvasGroup canvasGroup_;
    [SerializeField] private float transitionTime_;



    private void Awake()
    {

        if(instance != null)
        {
            Debug.LogWarning("Multiple FadeScreen instances detected!");
            return;
        }
        instance = this;

        canvasGroup_ = GetComponent<CanvasGroup>();
        canvasGroup_.alpha = 1f;

    }



    public void FadeIn()
    {
        StartCoroutine(Fade(true));
    }



    public void FadeOut()
    {
        StartCoroutine(Fade(false));
    }



    private IEnumerator Fade(bool fadeIn)
    {


        float timeElapsed = 0f;

        float startAlpha = canvasGroup_.alpha;
        float endAlpha = fadeIn ? 0f : 1f;



        while(timeElapsed < transitionTime_)
        {
            canvasGroup_.alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / transitionTime_);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        canvasGroup_.alpha = endAlpha;

    }


}