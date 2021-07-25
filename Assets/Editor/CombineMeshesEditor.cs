using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;



//[ExecuteInEditMode]
[CustomEditor(typeof(CombineMeshes))]
public class CombineMeshesEditor : Editor
{
    void OnSceneGUI()
       {
           CombineMeshes cm = target as CombineMeshes;
           if (Handles.Button(cm.transform.position + Vector3.up * 5, Quaternion.LookRotation(Vector3.up), 10, 10, Handles.CylinderHandleCap))
           {
               cm.CombineMeshesMethod();
           }
       }

}




