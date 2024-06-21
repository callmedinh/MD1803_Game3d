using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.InputManager;

public class ExperienceManager : Singleton<ExperienceManager>
{
    private int experiencePoints;

    public delegate void ExperienceChanged(int newExperience);
    public event ExperienceChanged OnExperienceChanged;

    public void AddExperience(int amount)
    {
        experiencePoints += amount;
        OnExperienceChanged?.Invoke(experiencePoints);
    }

    public int GetExperience()
    {
        return experiencePoints;
    }
}
