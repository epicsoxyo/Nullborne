using System.Collections;

using UnityEngine;



namespace Nullborne.UI
{

    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class UIScreenElement : MonoBehaviour
    {

        [SerializeField] private UIScreen screen_;
        public UIScreen screen{get{return screen_;}}

        private CanvasGroup canvasGroup_;
        private float defaultAlpha_;

        private RectTransform rectTransform_;
        [SerializeField] private Vector2 offScreenAnchoredPosition_;
        private Vector2 onScreenAnchoredPosition_;

        [SerializeField] private float transitionTime_;

        private bool isOnScreen_;
        public bool isOnScreen
        {
            get {return isOnScreen_;}
            set
            {
                canvasGroup_.alpha = value ? defaultAlpha_ : 0f;
                rectTransform_.anchoredPosition = value ? onScreenAnchoredPosition_ : offScreenAnchoredPosition_;
                isOnScreen_ = value;
            }
        }



        private void Awake()
        {

            canvasGroup_ = GetComponent<CanvasGroup>();
            defaultAlpha_ = canvasGroup_.alpha;

            rectTransform_ = GetComponent<RectTransform>();
            onScreenAnchoredPosition_ = rectTransform_.anchoredPosition;

        }



        public void ExitTransition()
        {

            if(!isOnScreen_) return;

            isOnScreen_ = false;
            StopAllCoroutines();
            StartCoroutine(Transition(false));

        }



        public void EnterTransition()
        {

            if(isOnScreen_) return;

            isOnScreen_ = true;
            StopAllCoroutines();
            StartCoroutine(Transition(true));

        }



        private IEnumerator Transition(bool isEntering)
        {

            float timeElapsed = 0f;

            float startAlpha = canvasGroup_.alpha;
            float endAlpha = isEntering ? defaultAlpha_ : 0f;

            Vector2 startPos = rectTransform_.anchoredPosition;
            Vector2 endPos = isEntering ? onScreenAnchoredPosition_ : offScreenAnchoredPosition_;



            while(timeElapsed < transitionTime_)
            {

                canvasGroup_.alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / transitionTime_);
                rectTransform_.anchoredPosition = Vector2.Lerp(startPos, endPos, timeElapsed / transitionTime_);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            canvasGroup_.alpha = endAlpha;

        }

    }

}