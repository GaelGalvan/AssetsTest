using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMain : MonoBehaviour
{

    // Go back To Main
    public void retry()
    {
        SceneManager.LoadScene("Main");
    }

}
