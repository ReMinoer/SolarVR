using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TabletView : MonoBehaviour
{
    public TabletScreen DefaultScreen;
    public MaterialScreen MaterialScreen;
    private IList<ITabletScreen> _screens;

    void Start()
    {
        _screens = new List<ITabletScreen>
        {
            DefaultScreen,
            MaterialScreen
        };
    }

    public void ShowMaterialInfo(MaterialInfo info)
    {
        MaterialScreen.MaterialInfo = info;
        ChangeScreen(MaterialScreen);
    }

    public void BackToDefault()
    {
        ChangeScreen(DefaultScreen);
    }

    private void ChangeScreen(ITabletScreen screen)
    {
        foreach (ITabletScreen tabletScreen in _screens)
            tabletScreen.Hide();

        screen.Show();
    }
}
