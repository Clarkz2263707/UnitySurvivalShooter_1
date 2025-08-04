using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Tooltip("Name of the scene to load when the button is clicked.")]
    public string LoadScene; 
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadNextInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
