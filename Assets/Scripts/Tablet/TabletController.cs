using UnityEngine;

public class TabletController : MonoBehaviour
{
    public MaterialInfoDictionary Dictionary;
    public TabletView View;
    public string HeadNodeName = "HeadNode";

    private Material _currentMaterial;
    private GameObject _head;

    void Start()
    {
        _head = GameObject.Find(HeadNodeName);
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
                }
            }
        }
    }
}
