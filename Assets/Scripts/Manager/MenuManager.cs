using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void OnStartButton()
    {
        // Example: start level 1
        if (GameManager.Instance != null)
            GameManager.Instance.StartLevel(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
