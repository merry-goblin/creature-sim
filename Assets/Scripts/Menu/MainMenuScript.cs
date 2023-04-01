using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene1");
    }
    public void Debug()
    {
        SceneManager.LoadScene("DebugMenuScene");
    }
}
