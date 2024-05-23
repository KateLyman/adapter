using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogbookEntry : MonoBehaviour
{
    [SerializeField] CreatureSO entryCreature;
    [SerializeField] Image entryImage;
    [SerializeField] Text entryName;
    [SerializeField] Text entryClass;
    [SerializeField] Text entryLocation;
    [SerializeField] Text entryFact;

    public void SetCreature(CreatureSO creature)
    {
        entryCreature = creature;
    }

    public void SetCompleteVisual()
    {
        entryImage.sprite = entryCreature.GetLogbookSprite();
        entryImage.color = Color.white;
    }

    public void SetIncompleteVisual()
    {
        entryImage.sprite = entryCreature.GetLogbookSprite();
        entryImage.color = Color.black;
    }

    public void SetIncompleteText()
    {
        entryName.text = "???";
        entryClass.text = "class: ???";
        entryLocation.text = "location: ???";
        entryFact.text = "fun fact: ???";
    }

    public void SetCompleteText()
    {
        entryName.text = entryCreature.GetCreatureName();
        entryClass.text = "class: " + entryCreature.GetClassification();
        entryLocation.text = "location: " + entryCreature.GetLocation();
        entryFact.text = "fun fact: " + entryCreature.GetFunFact();
    }
}
