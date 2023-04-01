using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMenuScript : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void LearnXORByRetropropagation()
    {
        SceneManager.LoadScene("LearnXORByRetropropagationScene");
    }
}
