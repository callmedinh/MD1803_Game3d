using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    public int experienceValue = 50;
    private AttributesManager atm;

    private void Start()
    {
        atm = GetComponent<AttributesManager>();
    }

    private void Update()
    {
        if (atm != null && atm.currentState == EntityState.Dead)
        {
            GiveExperience();
            // Destroy or disable the enemy
            Destroy(gameObject);
        }
    }

    private void GiveExperience()
    {
        ExperienceManager.Instance.AddExperience(experienceValue);
    }
}
