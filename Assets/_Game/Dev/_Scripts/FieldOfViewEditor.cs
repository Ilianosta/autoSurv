using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {

        FieldOfView fow = (FieldOfView)target;
        foreach (fovParams fov in fow.fovs)
        {
            Handles.color = Color.white;
            Handles.DrawWireArc(fow.transform.position, Vector3.back, Vector3.right, 360, fov.viewRadius);
            Vector3 viewAngleA = fow.DirFromAngle(-fov.viewAngle / 2, false);
            Vector3 viewAngleB = fow.DirFromAngle(fov.viewAngle / 2, false);

            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fov.viewRadius);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fov.viewRadius);

            Handles.color = Color.red;
            foreach (Transform visibleTarget in fow.visibleTargets)
            {
                Handles.DrawLine(fow.transform.position, visibleTarget.position);
            }
        }
    }
}
#endif

