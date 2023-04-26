using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of the tab menu of the character sheet
/// </summary>
public class TabGroup : MonoBehaviour
{
    private List<TabBtn> tabButtons;
    [SerializeField] private List<GameObject> pages;
    [SerializeField] private Color tabIdle;
    [SerializeField] private Color tabHover;
    [SerializeField] private Color tabSelected;
    [SerializeField] private TabBtn defaultTab;

    [SerializeField] private SoundSettings buttonHover;
    [SerializeField] private SoundSettings buttonClick;

    [HideInInspector]
    public TabBtn selectedTab;

    private void Start()
    {
        if (defaultTab == null)
            return;
        OnTabSelected(defaultTab);
    }

    public void Subscribe(TabBtn button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabBtn>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabBtn button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
            SoundManager.instance.PlaySound(buttonHover);
        }
    }

    public void OnTabExit(TabBtn button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabBtn button)
    {
        selectedTab = button;
        SoundManager.instance.PlaySound(buttonClick);
        ResetTabs();
        button.background.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < pages.Count; i++)
        {
            if (index == i)
            {
                SetActive(pages[i], true);
            }
            else
            {
                SetActive(pages[i], false);
            }
        }
    }

    public void ResetTabs()
    {
        if (tabButtons == null)
            return;
        foreach (TabBtn button in tabButtons)
        {
            if (selectedTab != null && selectedTab == button) { continue; }
            button.background.color = tabIdle;
        }
    }

    private void SetActive(GameObject page, bool value)
    {
        CanvasGroup group = page.GetComponent<CanvasGroup>();
        group.blocksRaycasts = value;
        group.alpha = value ? 1 : 0;
        group.interactable = value;
    }
}
