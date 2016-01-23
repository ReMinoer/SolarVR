using UnityEngine;

public class TabletController : MonoBehaviour
{
    private enum Mode
    {
        Wand,
        Fixed
    }

    public MaterialInfoDictionary Dictionary;
    public GameObject Tablet;
    public TabletView View;
    public GameObject MaterialCursor;
    public string HeadNodeName = "HeadNode";

    private Mode _mode = Mode.Wand;
    private Material _currentMaterial;
    private GameObject _head;

    void Start()
    {
        MaterialCursor.SetActive(false);

        _head = GameObject.Find(HeadNodeName);

        if (!(GetConfigFilename() == "Assets/default.vrx"
            || GetConfigFilename() == "Assets/gamepad.vrx"))
        {
            Tablet.transform.localPosition = 0.25f * Vector3.forward;
            Tablet.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
        }
        else
        {
            _mode = Mode.Fixed;
            Tablet.SetActive(false);
        }
    }

    void Update()
    {
        var ray = new Ray(_head.transform.position, _head.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var materialRenderer = hit.collider.gameObject.GetComponent<Renderer>();
            if (materialRenderer != null)
            {
                if (MiddleVR.VRDeviceMgr.IsWandButtonToggled(1, true))
                {
                    MaterialInfo materialInfo = Dictionary[materialRenderer.material];
                    if (materialInfo != null)
                    {
                        View.ShowMaterialInfo(materialRenderer.material, materialInfo);
                        _currentMaterial = materialRenderer.material;

                        MaterialCursor.SetActive(true);
                        MaterialCursor.transform.position = hit.point + hit.normal * 0.01f;
                        MaterialCursor.transform.rotation = Quaternion.LookRotation(hit.normal);

                        return;
                    }
                }

                if (_currentMaterial != null && _currentMaterial.mainTexture != materialRenderer.material.mainTexture)
                {
                    View.BackToDefault();
                    _currentMaterial = null;

                    MaterialCursor.gameObject.SetActive(false);
                }
            }
        }

        if (_mode == Mode.Fixed)
            Tablet.SetActive(_currentMaterial != null || MiddleVR.VRDeviceMgr.IsWandButtonPressed(2));
    }

    private string GetConfigFilename()
    {
        string configFileName = GameObject.Find("VRManager").GetComponent<VRManagerScript>().ConfigFile;
        bool nextArgIsConfigFile = false;
        for (uint i = 0, iEnd = MiddleVR.VRKernel.GetCommandLineArgumentsNb(); i < iEnd; ++i)
        {
            string cmdLineArg = MiddleVR.VRKernel.GetCommandLineArgument(i);

            if (cmdLineArg == "--config")
            {
                nextArgIsConfigFile = true;
            }
            else if (nextArgIsConfigFile)
            {
                configFileName = cmdLineArg;
                nextArgIsConfigFile = false;
            }
        }

        return configFileName;
    }
}
