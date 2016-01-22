using UnityEngine;

public class TabletController : MonoBehaviour
{
    public MaterialInfoDictionary Dictionary;
    public GameObject Tablet;
    public TabletView View;
    public string HeadNodeName = "HeadNode";

    private Material _currentMaterial;
    private GameObject _head;

    void Start()
    {
        _head = GameObject.Find(HeadNodeName);

        if (!(GetConfigFilename() == "Assets/default.vrx"
            || GetConfigFilename() == "Assets/gamepad.vrx"))
        {
            Tablet.transform.localPosition = 0.25f * Vector3.forward;
            Tablet.transform.localRotation = Quaternion.AngleAxis(90, Vector3.right);
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
                if (MiddleVR.VRDeviceMgr.IsWandButtonPressed(1))
                {
                    MaterialInfo materialInfo = Dictionary[materialRenderer.material];
                    if (materialInfo != null)
                    {
                        View.ShowMaterialInfo(materialRenderer.material, materialInfo);
                        _currentMaterial = materialRenderer.material;
                        return;
                    }
                }

                if (_currentMaterial != null && _currentMaterial.mainTexture != materialRenderer.material.mainTexture)
                {
                    View.BackToDefault();
                    _currentMaterial = null;
                }
            }
        }
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
