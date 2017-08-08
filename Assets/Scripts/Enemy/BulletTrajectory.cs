using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrajectory : MonoBehaviour {

    public LineRenderer line;
    public Bullet bulletPrefab;
    public Vector3 trajectory;

    void Initialize(Vector3 start, Vector3 direction)
    {
        DrawTrajectory(start, direction);
        Instantiate(bulletPrefab, start, Quaternion.identity, transform);

    }

    void DrawTrajectory(Vector3 start, Vector3 direction)
    {
        RaycastHit hit;
        Vector3 endPoint = new Vector3();
        if (Physics.Raycast(start, direction, out hit))
        {
            endPoint = hit.point;
        }
        line = new LineRenderer();
        line.positionCount = 2;
        line.SetPositions(new Vector3[] { start, endPoint });
        line.startColor = Color.red;
        line.endColor = Color.red;
    }
}
