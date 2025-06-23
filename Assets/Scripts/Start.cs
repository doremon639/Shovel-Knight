using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
