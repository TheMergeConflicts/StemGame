using UnityEngine;
using System.Collections;

public class PauseMenuLogic : MonoBehaviour {
    public GameObject pauseObject;
    public string mainMenu = "MainMenu";

    PlayerController pController;

    void Start()
    {
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            setPauseActive();
        }
    }

    public void setPauseActive()
    {
        pauseObject.SetActive(!pauseObject.activeSelf);
        pController.enabled = !pauseObject.activeSelf;
    }

    public void quitToMainMenu()
    {
        Application.LoadLevel(mainMenu);
    }

    public void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
