using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void LoadSceneAdditive(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }
}