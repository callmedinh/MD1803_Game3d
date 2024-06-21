using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.InputManager;

namespace Game.Weapon
{
    public class Sword : MonoBehaviour
    {
        public AttributesManager atm;
        private void OnTriggerEnter(Collider other)
        {
            if (VirtualInputManager.Instance.Slash && other.CompareTag("Enemy"))
            {
                Debug.Log("Va cham");
                atm.DealDamage(other.gameObject);

            }
        }
    }
}
