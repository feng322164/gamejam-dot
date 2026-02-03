using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentLevel = 0;

    public enum State { MainMenu, Playing, Paused }
    public State state = State.MainMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel(int index)
    {
        currentLevel = index;
        state = State.Playing;
        // Scene loading left to user; hook SceneManager.LoadScene(index) here if desired.
    }

    public void ReturnToMenu()
    {
        state = State.MainMenu;
        // SceneManager.LoadScene("MainMenu");
    }
}
