using UnityEngine;
using UnityEngine.UI;

public class NavigationAutoDisplay : MonoBehaviour
{
    public AutoNavigationManager AutoNavigationManager;
    public Text Text;

	void Update ()
    {
	    if (AutoNavigationManager.Enabled && !AutoNavigationManager.IsMoving)
	    {
	        Text.gameObject.SetActive(true);
	        Text.text = AutoNavigationManager.TargetedKeyPoint.pointName;
	    }
	    else
        {
            Text.gameObject.SetActive(false);
	    }
	}
}
