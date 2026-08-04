using UnityEngine;

public class Laser : MonoBehaviour
{
    
    public float distanceRay = 100f;
    public GameObject laserFirePoint;
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        Vector2 start = laserFirePoint.transform.position;
        Vector2 end = start + (Vector2)laserFirePoint.transform.right * distanceRay;

        Draw2DRay(start, end);
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}