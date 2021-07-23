using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(CombineMeshes))]
public class CombineMeshesEditor : Editor
{
    void OnSceneGUI()
    {
        /*  CombineMeshes cm = target as CombineMeshes;
          if (Handles.Button(cm.transform.position + Vector3.up * 5, Quaternion.LookRotation(Vector3.up), 1, 1, Handles.CylinderHandleCap ))
          {
              cm.CombineMeshesMethod();
          }
          */

        CombineMeshes cm = (CombineMeshes)target;

        Vector3 position = cm.transform.position + Vector3.up * 2f;
        float size = 2f;
        float pickSize = size * 2f;

        if (Handles.Button(position, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
            Debug.Log("The button was pressed!");
    }
}




