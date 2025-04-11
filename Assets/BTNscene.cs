using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTNscene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
