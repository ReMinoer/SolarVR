using UnityEngine;
using UnityEngine.UI;

public class MapScreen : TabletScreen
{
    public AutoNavigationManager AutoNavigationManager;
    public RectTransform MapPanel;
    public Text FloorText;
    public Text DestinationText;
    public Text AutoNavText;
    public Image CursorImage;
    public BoxCollider MapBounds;
    public string HeadNodeName = "HeadNode";
    public float[] FloorsHeight;
    public string[] FloorsLabels;
    private GameObject _head;

    void Start()
    {
        _head = GameObject.Find(HeadNodeName);
    }

    void Update()
    {
        Vector3 userPosition = MapBounds.ClosestPointOnBounds(_head.transform.position);
        Vector3 userForward = _head.transform.forward;

        var scale = new Vector2(1f / MapBounds.bounds.size.x, 1f / MapBounds.bounds.size.z);
        userPosition = userPosition - (MapBounds.bounds.center - MapBounds.bounds.size / 2);
        var cursorPosition = new Vector2(userPosition.x, userPosition.z);
        cursorPosition.Scale(scale);
        cursorPosition.Scale(MapPanel.rect.size);
        cursorPosition -= MapPanel.rect.size / 2;

        CursorImage.rectTransform.localPosition = cursorPosition;

        Quaternion quaternion = Quaternion.FromToRotation(userForward, MapBounds.transform.forward);
        CursorImage.rectTransform.localRotation = Quaternion.Euler(0, 0, quaternion.eulerAngles.y);

        if (MapBounds.bounds.Contains(_head.transform.position))
        {
            for (int i = 0; i < FloorsHeight.Length; i++)
                if (_head.transform.position.y - _head.transform.localPosition.y > FloorsHeight[i])
                {
                    FloorText.text = FloorsLabels[i];
                    break;
                }
        }
        else
            FloorText.text = "";

        if (AutoNavigationManager.Enabled && !AutoNavigationManager.IsMoving)
        {
            DestinationText.gameObject.SetActive(true);
            AutoNavText.gameObject.SetActive(true);
            DestinationText.text = string.Format("Cible : {0}", AutoNavigationManager.TargetedKeyPoint.pointName);
        }
        else
        {
            DestinationText.gameObject.SetActive(false);
            AutoNavText.gameObject.SetActive(false);
        }
    }
}
