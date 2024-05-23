using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    private Slider slider;
    private Animator animator;

    [SerializeField] PlayerSO player;
    [SerializeField] Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        animator = GetComponent<Animator>();

        slider.value = player.GetCurrentExperience() / player.GetCurrentExperienceGoal();
        if (slider.value > 0)
        {
            animator.Play("Idle");
        }
        levelText.text = "level: " + player.GetLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateExperience(float experience)
    {
        float initialExperience = player.GetCurrentExperience();
        player.AddExperience(experience);

        if (slider.value == 0)
        {
            animator.SetFloat("value", player.GetCurrentExperience() / player.GetCurrentExperienceGoal());
            animator.Play("Start");
        }

        if (player.GetCurrentExperience() >= player.GetCurrentExperienceGoal())
        {
            IncrementSlider((initialExperience / player.GetCurrentExperienceGoal()), 1.0f);
        }
        else
        {
            IncrementSlider(initialExperience / player.GetCurrentExperienceGoal(), player.GetCurrentExperience() / player.GetCurrentExperienceGoal());
        }
    }

    public void IncrementSlider(float initialPercent, float percentComplete)
    {
        StartCoroutine(Increment(initialPercent, percentComplete));
    }

    IEnumerator Increment(float initialPercent, float percentComplete)
    {

        while (slider.value < percentComplete)
        {
            slider.value += (1.0f / 200.0f);
            yield return new WaitForSeconds(0.004f / (percentComplete - initialPercent));
        }
        
        if (percentComplete == 1.0f)
        {
            animator.SetBool("complete", true);
            UpdateLevel();
        }
        yield break;
    }

    IEnumerator ResetExperienceBar()
    {
        yield return new WaitForSeconds(1.0f);
        slider.value = 0.0f;
        animator.SetFloat("value", slider.value);
        animator.SetBool("complete", false);
        levelText.text = "level: " + player.GetLevel();
        yield break;
    }

    public void UpdateLevel()
    {
        player.AddLevel();
        player.UpdateLevelEffects();
        StartCoroutine(ResetExperienceBar());
    }

}
