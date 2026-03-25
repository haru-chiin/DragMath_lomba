using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pengaturan UI")]
    public Button continueButton;
    [Header("Pengaturan Scene")]
    public string gameplaySceneName = "GameplayScene";
    public string StoryScene = "Prologue";
    public GameObject collectionPanel;

    private void Start()
    {
        if (PlayerPrefs.GetInt("HasStartedGame", 0) == 1)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }

        if (collectionPanel != null)
        {
            collectionPanel.SetActive(false);
        }
    }

    public void OnClickStart()
    {
        PlayerPrefs.SetInt("HasStartedGame", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(StoryScene);
    }

    public void OnClickContinue()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void OnClickCollectionBook()
    {
        if (collectionPanel != null)
        {
            collectionPanel.SetActive(true);
        }
    }

    public void CloseCollectionBook()
    {
        if (collectionPanel != null)
        {
            collectionPanel.SetActive(false);
        }
    }

    public void OnClickExit()
    {
        Debug.Log("Keluar dari Game...");

        Application.Quit();
    }

    public void DebugResetSaveData()
    {
        PlayerPrefs.DeleteKey("HasStartedGame");
        continueButton.interactable = false;
        Debug.Log("Save data di-reset!");
    }
}