using System.Collections;

using UnityEngine;



namespace Nullborne.UI
{

    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class MenuTransition : MonoBehaviour
    {

        private CanvasGroup canvasGroup_;
        private float defaultAlpha;

        private RectTransform rectTransform_;
        [SerializeField] private Vector2 offScreenAnchoredPosition_;
        private Vector2 onScreenAnchoredPosition_;

        [SerializeField] private float transitionTime_;

        [SerializeField] private bool startsOnScreen_;
        private bool isOnScreen;



        private void Awake()
        {

            canvasGroup_ = GetComponent<CanvasGroup>();
            defaultAlpha = canvasGroup_.alpha;

            rectTransform_ = GetComponent<RectTransform>();
            onScreenAnchoredPosition_ = rectTransform_.anchoredPosition;

            canvasGroup_.alpha = startsOnScreen_ ? defaultAlpha : 0f;
            rectTransform_.anchoredPosition = startsOnScreen_ ? onScreenAnchoredPosition_ : offScreenAnchoredPosition_;
            isOnScreen = startsOnScreen_;

        }



        private void Update()
        {
            // test
            if(!Input.GetKeyDown(KeyCode.E)) return;
            
            if(isOnScreen) ExitTransition();
            else EnterTransition();

            isOnScreen = !isOnScreen;

        }



        public void ExitTransition()
        {
            StopAllCoroutines();
            StartCoroutine(Transition(false));
        }



        public void EnterTransition()
        {
            StopAllCoroutines();
            StartCoroutine(Transition(true));
        }



        private IEnumerator Transition(bool isEntering)
        {

            float timeElapsed = 0f;

            float startAlpha = canvasGroup_.alpha;
            float endAlpha = isEntering ? defaultAlpha : 0f;

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