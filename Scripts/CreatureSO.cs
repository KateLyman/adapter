using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CreatureSO : ScriptableObject
{
    public enum CLSS {fish, cartilaginous_fishes, crustaceans, ray_finned_fishes, mammals, cephalopods, gastropods, true_jellies, reptiles, asteroidea, echinoidea, malacostracans, polychaeta, bivalvia};

    // General variables
    [SerializeField] string creatureName;
    [SerializeField] string classification;
    [SerializeField] string location;
    [SerializeField] string size;
    [SerializeField] string funFact;

    [SerializeField] bool isScannable = true;
    [SerializeField] bool isLogged = false;
    [SerializeField] List<CreatureBehaviors.BHV> availableBehaviors;
    [SerializeField] PlayerSO player;
    [SerializeField] PlayerSO.ADP adaptation;

    [SerializeField] float experience;

    // Logbook-related variables
    [SerializeField] LogbookSO logbook;
    [SerializeField] int logbookPosition;
    [SerializeField] Sprite logbookImage;

    // Varables for audio
    [SerializeField] float soundTimeMin;
    [SerializeField] float soundTimeMax;

    // Variables for behaviors
    [SerializeField] float speedNormal;
    [SerializeField] float speedFast;

    [SerializeField] float idleMinTime;
    [SerializeField] float idleMaxTime;

    [SerializeField] float pulseMinStrength;
    [SerializeField] float pulseMaxStrength;
    [SerializeField] float pulseMinTime;
    [SerializeField] float pulseMaxTime;

    [SerializeField] float fleeStartDistance;
    [SerializeField] float fleeEndDistance;

    [SerializeField] float hideStartDistance;
    [SerializeField] float hideEndDistance;

    public string GetCreatureName()
    {
        return creatureName;
    }

    public string GetClassification()
    {
        return classification;
    }

    public string GetLocation()
    {
        return location;
    }

    public string GetFunFact()
    {
        return funFact;
    }

    public PlayerSO.ADP GetAdaptation() {
        return adaptation;
    }

    public void Scan()
    {
        logbook.SetCurrentPage((logbookPosition + 1) / 2);

        if (isScannable)
        {
            isScannable = false;
            isLogged = true;
            logbook.LogEntry(this);
            
            if (!player.GetDiscoveredAdaptations().Contains(adaptation))
            {
                player.AddADP(adaptation);
            }
        }
    }

    public void UnScan()
    {
        isScannable = true;
        isLogged = false;
    }

    public bool IsScannable()
    {
        return isScannable;
    }

    public bool IsLogged()
    {
        return isLogged;
    }

    public float GetSoundTime()
    {
        return Random.Range(soundTimeMin, soundTimeMax);
    }
    public int GetLogbookPage()
    {
        return ((logbookPosition - 1) / 2) + 1 ;
    }

    public int GetLogbookPosition()
    {
        return logbookPosition;
    }

    public Sprite GetLogbookSprite()
    {
        return logbookImage;
    }

    public float GetExperience()
    {
        if (isScannable)
        {
            return experience;

        }
        else
        {
            return 5.0f;
        }
    }

    public float GetSpeedNormal()
    {
        return speedNormal;
    }

    public float GetSpeedFast()
    {
        return speedFast;
    }

    public float GetIdleTime()
    {
        return Random.Range(idleMinTime, idleMaxTime);
    }

    public float GetPulseStrength()
    {
        return Random.Range(pulseMinStrength, pulseMaxStrength);
    }

    public float GetPulseTime()
    {
        return Random.Range(pulseMinTime, pulseMaxTime);
    }

    public float GetFleeStartDistance()
    {
        return fleeStartDistance;
    }

    public float GetFleeEndDistance()
    {
        return fleeEndDistance;
    }

    public float GetHideStartDistance()
    {
        return hideStartDistance;
    }

    public float GetHideEndDistance()
    {
        return hideEndDistance;
    }
}