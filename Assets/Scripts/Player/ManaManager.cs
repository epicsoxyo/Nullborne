using UnityEngine;
using Nullborne.UI;


namespace Nullborne.Player
{

    public class ManaManager : MonoBehaviour
    {

        [SerializeField] private float maxMana_;
        public float MaxMana {get{return MaxMana;}}

        private float currentMana_;
        public float currentMana{get{return currentMana_;}}

        // [SerializeField] private float manaGainPerSecond_;

        [SerializeField] private UIBar manaBar_;



        private void Awake()
        {
            currentMana_ = maxMana_;
        }



        // private void Update()
        // {
        //     if(currentMana > maxMana_) return;
        //     currentMana_ += manaGainPerSecond_ * Time.deltaTime;
        //     UpdateManaBar();
        // }



        public void UseMana(float mana)
        {
            currentMana_ -= mana;
            if(currentMana_ < 0f) currentMana_ = 0f;
            UpdateManaBar();
        }



        public void GainMana(float mana)
        {
            currentMana_ += mana;
            if(currentMana_ > maxMana_) currentMana_ = maxMana_;
            UpdateManaBar();
        }



        private void UpdateManaBar()
        {
            manaBar_.UpdateBarFill(currentMana_/maxMana_);
        }

    }

}