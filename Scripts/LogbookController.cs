using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogbookController : MonoBehaviour
{
    public LogbookSO logbook;
    [SerializeField] CreatureSO[] allCreatures;
    [SerializeField] LogbookEntry[] entryBoxes;

    // Visual components of logbook
    [SerializeField] Text loggedCreaturesNumber;
    [SerializeField] Text pageNumber;

    // Start is called before the first frame update
    void Start()
    {
        allCreatures = Resources.LoadAll<CreatureSO>("Creatures");
        loggedCreaturesNumber.text = logbook.GetCreatureEntries().Count.ToString() + "/" + allCreatures.Length.ToString();
        GeneratePage();
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pageNumber.text = logbook.GetCurrentPage().ToString() + "/" + logbook.GetMaxPage();
        loggedCreaturesNumber.text = logbook.GetCreatureEntries().Count.ToString() + "/" + allCreatures.Length.ToString();

    }

    public void GeneratePage()
    {
        foreach (CreatureSO creature in allCreatures)
        {
            if (creature.GetLogbookPage() == logbook.GetCurrentPage()) 
            {
                if (creature.IsLogged())
                {
                    EntryComplete(creature.GetLogbookPosition(), creature);
                }
                else 
                {
                    EntryIncomplete(creature.GetLogbookPosition(), creature);
                }
            }
        }
    }

    public void EntryComplete(int entry, CreatureSO creature)
    {
        if (entry % 2 != 0)
        {
            entryBoxes[0].SetCreature(creature);
            entryBoxes[0].SetCompleteVisual();
            entryBoxes[0].SetCompleteText();
        }
        else
        {
            entryBoxes[1].SetCreature(creature);
            entryBoxes[1].SetCompleteVisual();
            entryBoxes[1].SetCompleteText();
        }

    }

    public void EntryIncomplete(int entry, CreatureSO creature)
    {
        if (entry % 2 != 0)
        {
            entryBoxes[0].SetCreature(creature);
            entryBoxes[0].SetIncompleteVisual();
            entryBoxes[0].SetIncompleteText();
        }
        else
        {
            entryBoxes[1].SetCreature(creature);
            entryBoxes[1].SetIncompleteVisual();
            entryBoxes[1].SetIncompleteText();
        }
    }
}
