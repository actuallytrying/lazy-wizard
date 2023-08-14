using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum CollectibleType
{
    Monster,
    Item
}

public class BookManager : MonoBehaviour, IScreen
{
    [Header("Data")]
    [SerializeField] private MonsterBook book;

    [Header("Prefabs")]
    [SerializeField] private GameObject monsterSlotPrefab;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject dummySlotPrefab;

    [Header("UI")]
    [SerializeField] private Transform firstPageParent;
    [SerializeField] private Transform secondPageParent;
    [SerializeField] private TextMeshProUGUI pageTitle;
    [SerializeField] private int itemsPerPage;

    private CollectibleType currentType = CollectibleType.Monster; // Default
    private List<ICollectible> allCollectibles;
    private int currentPageIndex = 0;

    public void OnActivate()
    {
        UpdateUI();
    }

    public void ChangeCollectibleType(CollectibleType type)
    {
        currentType = type;
        currentPageIndex = 0; // reset page index when changing type
        UpdateUI();
    }

    public void GoToNextPage()
    {
        int totalNumPages = Mathf.CeilToInt(allCollectibles.Count / 32f); // Assuming 32 per two pages
        if (currentPageIndex < totalNumPages - 1)
        {
            currentPageIndex++;
            UpdateUI();
        }
    }

    public void GoToPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        ClearPages();
        pageTitle.text = "Pages: " + (currentPageIndex + 1) + " - " + (currentPageIndex + 2);
        allCollectibles = FetchCollectibles(currentType);

        int startIndex = currentPageIndex * itemsPerPage * 2;  // Assuming 2 pages per view.
        int endIndex = Mathf.Min(startIndex + itemsPerPage * 2, allCollectibles.Count);

        for (int i = startIndex; i < endIndex; i++)
        {
            Transform parent = i < startIndex + itemsPerPage ? firstPageParent : secondPageParent;
            GameObject slotPrefab;

            if (currentType == CollectibleType.Monster)
            {
                slotPrefab = monsterSlotPrefab;
            }
            else
            {
                slotPrefab = itemSlotPrefab;
            }

            GameObject slot = Instantiate(slotPrefab, parent);
            if (i < allCollectibles.Count)  // Ensure the index isn't out of range.
            {
                if (currentType == CollectibleType.Monster)
                {
                    slot.GetComponent<MonsterSlot>().SetMonster(allCollectibles[i] as Monster);
                }
                else
                {
                    slot.GetComponent<ItemBookSlot>().SetItem(allCollectibles[i] as Item);
                }
            }
        }

        // Fill the remaining vacant slots with dummy slots.
        int itemsInBothPages = itemsPerPage * 2;
        int currentItemsInBothPages = endIndex - startIndex;
        int dummySlotsToAdd = itemsInBothPages - currentItemsInBothPages;
        for (int i = 0; i < dummySlotsToAdd; i++)
        {
            Transform parent = (endIndex + i) < startIndex + itemsPerPage ? firstPageParent : secondPageParent;
            Instantiate(dummySlotPrefab, parent);
        }
    }


    private List<ICollectible> FetchCollectibles(CollectibleType type)
    {
        List<ICollectible> collectibles = new();

        if (type == CollectibleType.Monster)
        {
            List<Page> allPages = book.GetPages();
            foreach (Page page in allPages)
            {
                collectibles.AddRange(page.monsters);
            }
        }
        else // For items
        {
            collectibles.AddRange(ItemManager.Instance.Items);
        }

        return collectibles;
    }


    private void ClearPages()
    {
        foreach (Transform child in firstPageParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in secondPageParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetToMonsterType() 
    {
        ChangeCollectibleType(CollectibleType.Monster);
    }

    public void SetToItemType() 
    {
        ChangeCollectibleType(CollectibleType.Item);
    }
}
