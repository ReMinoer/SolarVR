using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TabletView : MonoBehaviour
{
    public MapScreen MapScreen;
    public MaterialScreen MaterialScreen;
    private IList<ITabletScreen> _screens;

    void Start()
    {
        _screens = new List<ITabletScreen>
        {
            MapScreen,
            MaterialScreen
        };
    }

    public void ShowMaterialInfo(Material material, MaterialInfo info)
    {
        MaterialScreen.Material = material;
        MaterialScreen.MaterialInfo = info;

        ChangeScreen(MaterialScreen);
    }

    public void BackToDefault()
    {
        ChangeScreen(MapScreen);
    }

    private void ChangeScreen(ITabletScreen screen)
    {
        foreach (ITabletScreen tabletScreen in _screens)
            tabletScreen.Hide();

        screen.Show();
    }
}
