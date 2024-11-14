using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;

public class BoulderBehavior : MonoBehaviour
{
    float boulderY;
    [DoNotSerialize]
    public int highScore = 0;
    [Header("Score Text")]
    public TextMeshProUGUI ScoreText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        boulderY = this.gameObject.transform.position.y;
        if (boulderY > highScore)
        {
            highScore = (int)boulderY;
            ScoreText.text = $"{highScore}m";
        }
    }
}
