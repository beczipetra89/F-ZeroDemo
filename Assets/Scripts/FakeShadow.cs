using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeShadow : MonoBehaviour
{
    public Transform _parent;
    public Vector3 _parentOffest = new Vector3(0f, 0.01f, 0f);
    public LayerMask _layerMask;

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hitInfo;
       // _renderer.enabled = true;


        if (Physics.Raycast(ray, out hitInfo, 100f, _layerMask))
        {
            // Position
            _parent.position = hitInfo.point + _parentOffest;

            // Rotate to the ground´s angle
            _parent.up = hitInfo.normal;
            _renderer.enabled = true;

        }
         else
         {
            // If raycast not hitting air (air below feet) then position it far away
            //_parent.position = new Vector3(0f, 1000f, 0f);
            _renderer.enabled = false;

        } 

    }
}
