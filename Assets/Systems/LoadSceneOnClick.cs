using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        Debug.Log("Button clicked! Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
