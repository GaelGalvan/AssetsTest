using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    public PlayerMovement PMScript;
    public GameObject Panel;


    // Close Button
    public void ClosePanel()
    {
        Panel.SetActive(false);
        PMScript.isPanel = false;
    }

    // You lose
    public void lose()
    {
        SceneManager.LoadScene("You Lose");
    }

    // You win
    public void win()
    {
        SceneManager.LoadScene("You Win");
    }


}
