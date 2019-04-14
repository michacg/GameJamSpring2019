using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PlayerController player1_controller;
    public PlayerController player2_controller;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.R))
        {
            if (!paused)
            {
                Time.timeScale = 0.0f;
                paused = true;

                /*
                player1_controller.Disable_Inputs();
                player2_controller.Disable_Inputs();
                */
            }
            else
            {
                Time.timeScale = 1.0f;
                paused = false;

                /*
                player1_controller.Enable_Inputs();
                player2_controller.Enable_Inputs();
                */
            }
        }

        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.O)) && paused)
        {
            SceneManager.LoadScene("StartMenuScene", LoadSceneMode.Single);
        }
    }
}
