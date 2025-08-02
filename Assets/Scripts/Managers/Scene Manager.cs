using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Scene Level01; 
    public void LoadSceneByName()
    {
        SceneManager.LoadScene("Level01");
    }

    public void LoadNextInBuild()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
