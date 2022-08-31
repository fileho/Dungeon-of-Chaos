using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    private List<TabButton> tabButtons;
    [SerializeField] private List<GameObject> pages;
    [SerializeField] private Color tabIdle;
    [SerializeField] private Color tabHover;
    [SerializeField] private Color tabSelected;
    [SerializeField] private TabButton defaultTab;

    [HideInInspector]
    public TabButton selectedTab;

    private void Start()
    {
        if (defaultTab == null)
            return;
        OnTabSelected(defaultTab);
    }

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
            button.background.color = tabHover;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < pages.Count; i++)
        {
            if (index == i)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        if (tabButtons == null)
            return;
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab != null && selectedTab == button) { continue; }
            button.background.color = tabIdle;
        }
    }
}
