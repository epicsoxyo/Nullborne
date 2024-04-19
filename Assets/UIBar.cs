using UnityEngine;
using UnityEngine.UI;



namespace Nullborne.UI
{

    public class UIBar : MonoBehaviour
    {

        private Image barImage;

        private float currentFill;
        private float newFill;

        [SerializeField] private float barLerpTime;
        private float timeElapsed = 0f;



        private void Start()
        {

            barImage = transform.GetChild(0).GetComponent<Image>();

            currentFill = barImage.fillAmount;
            newFill = barImage.fillAmount;

        }



        public void UpdateBarFill(float fillAmount)
        {

            currentFill = barImage.fillAmount;

            if(fillAmount < 0) newFill = 0;
            else if(fillAmount > 1f) newFill = 1f;
            else newFill = fillAmount;

            timeElapsed = 0f;

        }



        private void Update()
        {

            if(timeElapsed < barLerpTime)
            {
                barImage.fillAmount = Mathf.Lerp(currentFill, newFill, timeElapsed / barLerpTime);
                timeElapsed += Time.deltaTime;
            }

        }

    }

}