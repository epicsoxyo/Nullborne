using UnityEngine;
using UnityEngine.EventSystems;



namespace Nullborne.UI
{

    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class DraggableElement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        private RectTransform rectTransform_;

        private CanvasGroup canvasGroup_;
        [SerializeField] private float dragAlpha_ = 0.6f;
        private float defaultAlpha_;

        private Vector3 mostRecentPosition_;

        [SerializeField] private float tileSwapDistThreshold_ = 10f;



        private void Awake()
        {

            rectTransform_ = GetComponent<RectTransform>();

            canvasGroup_ = GetComponent<CanvasGroup>();
            defaultAlpha_ = canvasGroup_.alpha;

        }



        public void UpdateMostRecentPosition()
        {
            mostRecentPosition_ = rectTransform_.position;
        }



        public void OnBeginDrag(PointerEventData eventData)
        {

            canvasGroup_.blocksRaycasts = false;
            canvasGroup_.alpha = dragAlpha_;

            UpdateMostRecentPosition();

        }



        public void OnDrag(PointerEventData eventData)
        {

            Transform currentUIPanel = transform.parent;

            rectTransform_.position = new Vector3
            (
                rectTransform_.position.x,
                eventData.position.y,
                rectTransform_.position.z
            );

            foreach(Transform otherTransform in currentUIPanel.transform)
            {
                if (otherTransform == transform) continue;

                float distanceToOther = Vector3.Distance
                (
                    rectTransform_.position,
                    otherTransform.position
                );

                if (distanceToOther <= tileSwapDistThreshold_)
                {
                    Vector3 temp = otherTransform.position;

                    otherTransform.position = new Vector3
                    (
                        otherTransform.position.x,
                        mostRecentPosition_.y,
                        otherTransform.position.z
                    );

                    rectTransform_.position = new Vector3
                    (
                        otherTransform.position.x,
                        temp.y,
                        otherTransform.position.z
                    );

                    rectTransform_.SetSiblingIndex(otherTransform.GetSiblingIndex());

                    UpdateMostRecentPosition();
                }
            }

        }



        public void OnEndDrag(PointerEventData eventData)
        {

            canvasGroup_.blocksRaycasts = true;
            canvasGroup_.alpha = defaultAlpha_;

            rectTransform_.position = mostRecentPosition_;

        }

    }

}