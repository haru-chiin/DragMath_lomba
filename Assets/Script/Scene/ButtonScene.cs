using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScene : MonoBehaviour
{
    public string gameplaySceneName = "GameplayScene";
    public void OnClickContinue()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }
}
