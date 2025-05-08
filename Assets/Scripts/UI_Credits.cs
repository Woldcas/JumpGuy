using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Credits : MonoBehaviour
{
    [SerializeField] private RectTransform creditsPanel;
    [SerializeField] private float scrollSpeed = 150;
    [SerializeField] private float screenPosition = 1500;

    [SerializeField] private string mainMenuSceneName = "MainMenu";
    private bool creditsSkipped;

    private void Update()
    {
        creditsPanel.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if(creditsPanel.anchoredPosition.y >= screenPosition)
            GoToMainMenu();
    }

    public void SkipCredits()
    {
        if (creditsSkipped == false)
        {
            scrollSpeed *= 10;
            creditsSkipped = true;
        }
        else
        {
            GoToMainMenu();
        }
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
