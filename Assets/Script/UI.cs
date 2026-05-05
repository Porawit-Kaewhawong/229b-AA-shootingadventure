using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void RestartLevel()
    {
        string currentName = SceneManager.GetActiveScene().name;

        PlayerController player = FindAnyObjectByType<PlayerController>();
        Destroy(player.gameObject);

        Time.timeScale = 1f;

        SceneManager.LoadScene(currentName);

        Debug.Log("Loadscene" + currentName);
    }
}
