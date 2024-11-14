using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HillGenerator : MonoBehaviour
{
    public GameObject[] Planes;
    private Queue<GameObject> PlanesQueue = new Queue<GameObject>();
    int HillCount = 0;
    float hillLength = 0f;
    float hillHeight = 0f;
    public GameObject player;
    int distance = 50;

    public void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            EnQueueHill(30);
        }
        //DeQueueHill();
    }
    public void EnQueueHill(int Angle)
    {
        GameObject hill = Planes[Random.Range(0,Planes.Length)];
        Debug.Log($"{hillLength}, {hillHeight}");
        GameObject newHill = Instantiate(hill, this.transform.TransformPoint(new Vector3(10 * Mathf.Cos(Angle * Mathf.Deg2Rad) * HillCount, (10 * Mathf.Sin(Angle * Mathf.Deg2Rad) * HillCount), 0)), Quaternion.Euler(0, 0, Angle), this.transform);
        PlanesQueue.Enqueue(newHill);
        HillCount++;
        
    }
    public void DeQueueHill()
    {
        GameObject hill = PlanesQueue.Dequeue();
        Destroy(hill);
        distance += 5;
    }
    public void GenerateHills()
    {
        float dt = -player.transform.position.y / Mathf.Sin(30);
        if (distance - dt <= 0)
        {
            EnQueueHill(30);
            DeQueueHill();
        }
    }
    public void FixedUpdate()
    {
        GenerateHills();
        //Debug.Log(-player.transform.position.y / Mathf.Sin(30));
    }
}
