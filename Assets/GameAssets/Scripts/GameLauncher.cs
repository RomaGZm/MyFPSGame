using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class GameLauncher : MonoBehaviour
{
    [SerializeField]
    private Button btnSettings;
    [SerializeField]
    private Button btnPlay;
    [SerializeField]
    private Button btnExit;
    [SerializeField]
    private List<EasyTween> hideComponents;

    public void OnBtnPlayClick()
    {
        StartCoroutine(LoadScene(2));
    }
    public void OnBtnSettingsClick()
    {
        btnSettings.interactable = false;
    }
    public void OnBtnSettingsCloseClick()
    {
        btnSettings.interactable = true;
    }
    public void OnBtnExitClick()
    {
        Application.Quit();

    }
    private void HideComponents()
    {
        foreach (EasyTween et in hideComponents)
            et.OpenCloseObjectAnimation();
    }

    IEnumerator LoadScene(float delay)
    {
        btnPlay.interactable = false;
        btnExit.interactable = false;
        btnSettings.interactable = false;
        HideComponents();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level1");
    }
}
