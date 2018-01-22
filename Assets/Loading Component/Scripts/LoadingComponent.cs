using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class LoadingComponent : MonoBehaviour
{
    private AsyncOperation asy;
    public bool normalized = false;

    public Image LoadBar;

    private void Start()
    {
        LoadNextLevel();
    }

    private void Update()
    {
        if (normalized)
        {
            //loadingBarPosition.x = Mathf.Clamp(loadingBarPosition.x, 0f, 1f);
            //loadingBarPosition.y = Mathf.Clamp(loadingBarPosition.y, 0f, 1f);
        }
    }

    private void LoadNextLevel()
    {
        if (PlayerPrefs.GetInt("tutor") == 0)
        {
            switch (PlayerPrefs.GetInt("selmap"))
            {
                case 0:
                    asy = SceneManager.LoadSceneAsync(3);
                    break;
                case 1:
                    asy = SceneManager.LoadSceneAsync(4);
                    break;
                case 2:
                    asy = SceneManager.LoadSceneAsync(5);
                    break;
                case 3:
                    asy = SceneManager.LoadSceneAsync(6);
                    break;
            }
        }
        else
        {
            asy = SceneManager.LoadSceneAsync(2);
        }
    }

    private void FixedUpdate()
    {
        LoadBar.fillAmount = asy.progress;
    }
}