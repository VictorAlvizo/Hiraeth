using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public static LoadScene instance = null;

    public GameObject loadPanel;

    public Slider loadBar;
    public Text progressText;

    #region Singleton

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion singleton

    public void StartLoad(int sceneIndex)
    {
        StartCoroutine(LoadProgress(sceneIndex));
    }

    IEnumerator LoadProgress(int sceneIndex)
    {
        loadPanel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            float roundValue = Mathf.CeilToInt(progress * 100);

            loadBar.value = progress;
            progressText.text = string.Format("{0}%", roundValue);

            if(progress == 1)
            {
                loadPanel.SetActive(false);
            }

            yield return null;
        }
    }
}
