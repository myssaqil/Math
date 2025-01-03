using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGameplay()
    {
        // Debug.Log("Hallo WOrld");
        SceneManager.LoadScene("GameplayScene");
    }
}
