using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Bắt đầu lại game (hoặc chạy scene chính)
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Thoát game (chạy ngoài Build sẽ hoạt động)
    public void ExitGame()
    {
        Debug.Log("Thoát game");
        Application.Quit();
    }
}
