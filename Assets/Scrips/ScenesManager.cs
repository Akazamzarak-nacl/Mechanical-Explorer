using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void Play()
    {
        //切换到SampleScene场景
        SceneManager.LoadScene("SampleScene");
    }
    public void Help()
    {
        SceneManager.LoadScene("Help");
    }
    public void Exit()
    {
        SceneManager.LoadScene("Scene1");
    }
}
