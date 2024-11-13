using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillGenerator : MonoBehaviour
{
    public GameObject[] Planes;
    private Queue<GameObject> PlanesQueue = new Queue<GameObject>();
    int PlaneCount = 0;

    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            EnQueuePlane();
        }
    }
    public void EnQueuePlane()
    {
        GameObject hill = Planes[0];
        PlanesQueue.Enqueue(hill);
        GameObject newHill = Instantiate(hill,this.transform.TransformPoint(new Vector3(0, 10 * PlaneCount, 0)),Quaternion.Euler(0,0,-45), this.transform);
        PlaneCount++;
        
    }
    public void DeQueuePlane()
    {

    }
}
