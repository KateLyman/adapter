using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LogbookSO : ScriptableObject
{
    [SerializeField] List<CreatureSO> creatureEntries;
    [SerializeField] int currentPage;
    [SerializeField] int maxPage;
    [SerializeField] float flipDirection;

    public List<CreatureSO> GetCreatureEntries()
    {
        return creatureEntries;
    }

    public void LogEntry(CreatureSO creature)
    {
        creatureEntries.Add(creature);
    }

    public void UnlogEntry(CreatureSO creature)
    {
        creatureEntries.Remove(creature);
    }

    public void SetCurrentPage(int pageNum)
    {
        currentPage = pageNum;
    }

    public int GetCurrentPage()
    {
        return currentPage;
    }

    public int GetMaxPage()
    {
        return maxPage;
    }

    public void ClearEntries()
    {
        foreach (CreatureSO creature in creatureEntries)
        {
            creature.UnScan();
        }

        creatureEntries.Clear();
        currentPage = 1;
    }

    public void SetFlipDirection(float i)
    {
        flipDirection = i;
    }

    public void FlipPage()
    {
        Debug.Log("FLIPPING PAGE:");
        Debug.Log(currentPage);

        if (flipDirection > 0.0f)
        {
            NextPage();
        }
        else if (flipDirection < 0.0f)
        {
            PrevPage();
        }
        Debug.Log(currentPage);
    }

    private void NextPage()
    {
        if (currentPage < maxPage)
        {
            currentPage += 1;
        }
    }

    private void PrevPage()
    {
        if (currentPage > 1)
        {
            currentPage -= 1;
        }
    }
}
