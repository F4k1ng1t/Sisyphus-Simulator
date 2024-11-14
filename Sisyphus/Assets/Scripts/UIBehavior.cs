using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIBehavior : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
