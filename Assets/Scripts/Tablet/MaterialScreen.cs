using UnityEngine.UI;

public class MaterialScreen : TabletScreen
{
    public Text NameText;
    public Text DescriptionText;
    public Text AdvantagesText;
    public Image MaterialImage;
    public MaterialInfo MaterialInfo { get; set; }

    public override void Show()
    {
        base.Show();

        if (MaterialInfo != null)
        {
            NameText.text = MaterialInfo.Name;
            DescriptionText.text = MaterialInfo.Description;
            AdvantagesText.text = MaterialInfo.Advantages;
            MaterialImage.material = MaterialInfo.Material;
        }
        else
        {
            NameText.text = "";
            DescriptionText.text = "";
            AdvantagesText.text = "";
            MaterialImage.material = null;
        }
    }
}
