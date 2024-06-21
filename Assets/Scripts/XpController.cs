using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XpController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelText;
    //[SerializeField] private TextMeshProUGUI ExperienceText;
    //[SerializeField] private Image XpProgressBar;
    [SerializeField] private int level = 1;
    private float currentXp;
    [SerializeField] private float targetXp;

    private void Start()
    {
        ExperienceManager.Instance.OnExperienceChanged += UpdateExperienceUI;
        UpdateExperienceUI(ExperienceManager.Instance.GetExperience());
    }

    private void UpdateExperienceUI(int newExperience)
    {
        currentXp = newExperience;
        //XpProgressBar.fillAmount = currentXp / targetXp;
        //ExperienceText.text = $"XP: {currentXp}/{targetXp}";

        if (currentXp >= targetXp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        LevelText.text = level.ToString();
        currentXp -= targetXp;
        targetXp *= 1.5f; // Increase the required XP for next level
        UpdateExperienceUI((int)currentXp);
    }
}
