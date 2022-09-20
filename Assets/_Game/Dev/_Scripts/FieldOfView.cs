using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldOfView : MonoBehaviour
{
    public fovParams[] fovs;
    public List<Transform> visibleTargets = new List<Transform>();
    public Transform GetRandomTarget
    {
        get
        {
            if (visibleTargets.Count == 0) return null;
            return visibleTargets[Random.Range(0, visibleTargets.Count)];
        }
    }
    public bool targetLocated => visibleTargets.Count != 0 ? true : false;
    //public Transform playerLocated => visibleTargets[0];  // **Only get the target[0]** If u want to get the position of the target uncomment this lane
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.1f));
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            visibleTargets.Clear();
            foreach (fovParams fov in fovs)
            {
                FindVisibleTargets(fov.viewRadius, fov.viewAngle, fov.targetMask, fov.obstacleMask);
            }
        }
    }
    void FindVisibleTargets(float viewRadius, float viewAngle, LayerMask targetMask, LayerMask obstacleMask)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);
        foreach (Collider2D coll in colls)
        {
            Transform target = coll.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
}
[System.Serializable]
public struct fovParams
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
}
