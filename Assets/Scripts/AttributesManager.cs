using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEngine.Rendering.DebugUI;

public class AttributesManager : MonoBehaviour
{
    //Manages health, damage, and states of entities (e.g., enemies, players)
    public GameObject FloatingTextPrefab;
    public int health;
    public int attack;
    public Vector3 offset = new(2, 3, 2);

    public float critDamage = 1.5f;
    public float critChance = 0.5f;

    public Slider slider;
    public EntityState currentState;

    public int experienceValue = 50;
    private void Start()
    {
        currentState = EntityState.Alive;
        slider = gameObject.GetComponentInChildren<Slider>();
        if (slider != null )
        {
            slider.maxValue = health;
            slider.value = health;
        }
    }

    public void TakeDamage(int amount)
    {
        if (currentState == EntityState.Dead) return;
     
        health -= amount;
        health = Mathf.Max(health, 0); //ensure health doesn't go below 0

        Animator animator = GetComponent<Animator>();
        if (animator != null )
        {
            animator.SetTrigger("Take_Damage_1");
        }

        if (slider != null )
        { 
            slider.value = health;
        }
       
        if (FloatingTextPrefab && health > 0)
        {
            ShowFloatingText();
        }

        if (health <= 0)
        {
            ChangeState(EntityState.Dead);
        }
    }

    public void DealDamage(GameObject target)
    {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            float totalDamage = attack;
            if (UnityEngine.Random.Range(0f, 1f) < critChance)
            {
                totalDamage *= critDamage;
            }
            atm.TakeDamage((int)totalDamage);
        }
    }
    void ShowFloatingText()
    {
        if (FloatingTextPrefab != null)
        {
            var textObject = Instantiate(FloatingTextPrefab, transform.position + offset, Quaternion.identity, transform);
            var textMesh = textObject.GetComponent<TextMesh>();
            if (textMesh != null)
            {
                textMesh.text = health.ToString();
            }
        }
    }

    private void ChangeState(EntityState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        switch (currentState)
        {
            case EntityState.Alive:
                break;
            case EntityState.Dead:
                HandleDeath();
                break;
        }
    }

    private void HandleDeath()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        ExperienceManager.Instance.AddExperience(experienceValue);
        //Notify listener that this entity has died;
        Destroy(gameObject, 3f);
    }
}
