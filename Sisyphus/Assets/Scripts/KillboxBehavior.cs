using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillboxBehavior : MonoBehaviour
{
    public GameObject Player;
    public BoulderBehavior Boulder;
    public GameObject TryAgainButton;
    float currentPlayerYPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.transform.position.x, this.transform.position.y, Player.transform.position.z);
        if (Player.GetComponent<Rigidbody>().velocity.y > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, Player.transform.position.y - 20, this.transform.position.z);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boulder" || other.tag == "Player")
        {
            Leaderboard.instance.SetLeaderboardEntry(Boulder.highScore);
            Leaderboard.instance.DisplayLeaderboard();
            Leaderboard.instance.leaderboardCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            TryAgainButton.SetActive(true);
        }
    }
}
