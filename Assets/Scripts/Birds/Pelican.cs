using UnityEngine;

namespace Birds
{
    public class Pelican : Bird
    {
        public int healAmount;
        private Health _carHealth;

        public override void GetHit()
        {
            _carHealth = GameObject.FindGameObjectWithTag("Car").GetComponent<Health>();
            _carHealth.Heal(healAmount);
            base.GetHit();
        }
    }
}