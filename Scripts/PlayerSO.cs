using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]
public class PlayerSO : ScriptableObject
{
    public enum ADP {none, gills, anemone, fin, biolum, pressure, camo, flex, sonar, lure, chemo};

    [SerializeField] int currentSkin = 0;
    [SerializeField] int totalSkinCount = 3;

    [SerializeField] List<ADP> discoveredADP;
    [SerializeField] List<ADP> activeADP;
    [SerializeField] ADP currentADP;

    [SerializeField] float currentExperience;
    [SerializeField] float currentExperienceGoal = 50.0f;
    [SerializeField] List<float> experiencesGoals = new List<float>() { 50.0f, 100.0f, 300.0f, 500.0f, 1000.0f };
    [SerializeField] int level = 1;

    [SerializeField] float currentPlayerSpeed = 30;
    [SerializeField] float normalPlayerSpeed = 30;
    [SerializeField] float fastPlayerSpeed = 50;

    [SerializeField] Vector2 startPos;

    [SerializeField] string previousScene;

    // Adaptation variables

    private bool isHidden = false;
    private bool hasBones = true;
    private bool lureActive = false;
    private bool sonarActive = false;

    public void SetPreviousScene(string sceneName)
    {
        previousScene = sceneName;
    }

    public string GetPreviousScene() 
    { 
        return previousScene; 
    }

    public void ResetPlayer()
    {
        ResetLevel();
        UpdateLevelEffects();
        ResetSpeed();

        discoveredADP.Clear();
        activeADP.Clear();
        discoveredADP.Add(ADP.none);
        SetActiveADP(ADP.none);

        SetStartPos(new Vector2(0.0f, 7.0f));

        SetSkin(0);
    }

    public void SetSkin(int skin)
    {
        currentSkin = skin;
    }

    public int GetSkin()
    {
        return currentSkin;
    }

    public int GetSkinCount()
    {
        return totalSkinCount;
    }

    public List<ADP> GetDiscoveredAdaptations()
    {
        return discoveredADP;
    }

    public List<ADP> GetActiveAdaptations()
    {
        return activeADP;
    }

    public ADP GetCurrentAdaptation()
    {
        return currentADP;
    }

    public void UpgradeSpeed()
    {
        currentPlayerSpeed = fastPlayerSpeed;
    }

    public void ResetSpeed()
    {
        currentPlayerSpeed = normalPlayerSpeed;
    }

    public void SetActiveADP(ADP adaptation)
    {
        currentADP = adaptation;
    }

    public void AddADP(ADP adaptation)
    {
        Debug.Log("adaptation added!");
        discoveredADP.Add(adaptation);

        if (adaptation == ADP.flex || adaptation == ADP.lure || adaptation == ADP.sonar || adaptation == ADP.chemo || adaptation == ADP.camo)
        {
            activeADP.Add(adaptation);
        }
    }

    public bool GetIsHidden()
    {
        return isHidden;
    }

    public void SetIsHidden(bool var)
    {
        isHidden = var;
    }

    public bool GetHasBones()
    {
        return hasBones;
    }

    public void SetHasBones(bool var)
    {
        hasBones = var;
    }

    public bool GetLureActive()
    {
        return lureActive;
    }

    public void SetLureActive(bool var)
    {
        lureActive = var;
    }

    public bool GetSonarActive()
    {
        return sonarActive;
    }

    public void SetSonarActive(bool var)
    {
        sonarActive = var;
    }

    public float GetCurrentPlayerSpeed()
    {
        return currentPlayerSpeed;
    }

    public float GetCurrentExperience()
    {
        return currentExperience;
    }

    public float GetCurrentExperienceGoal()
    {
        return currentExperienceGoal;
    }

    public int GetLevel()
    {
        return level;
    }

    public void ResetExperience()
    {
        currentExperience = 0.0f;
    }

    public void AddExperience(float experience)
    {
        currentExperience += experience;
    }

    public void ResetLevel()
    {
        level = 1;
    }

    public void AddLevel()
    {
        level += 1;
    }

    public void AddLevels(int num)
    {
        level += num;
    }

    public void UpdateLevelEffects()
    {
        ResetExperience();

        switch (level)
        {
            case 1:
                currentExperienceGoal = experiencesGoals[0];
                break;
            case 2:
                currentExperienceGoal = experiencesGoals[1];
                // scansize
                break;
            case 3:
                currentExperienceGoal = experiencesGoals[2];
                // scanspeed
                break;
            case 4:
                currentExperienceGoal = experiencesGoals[3];
                // adaptation upgrades
                break;
            case 5:
                currentExperienceGoal = experiencesGoals[4];
                // scan all creatures
                break;
            default:
                break;
        }
    }

    public Vector2 GetStartPos()
    {
        return startPos;
    }

    public void SetStartPos(Vector2 pos)
    {
        startPos = pos;
    }

    public void SetStartPosX(float x)
    {
        startPos.x = x;
    }

    public void SetStartPosY(float y)
    {
        startPos.y = y;
    }
}