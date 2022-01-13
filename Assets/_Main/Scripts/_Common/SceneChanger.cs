using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float fadeDuration;

    // Start is called before the first frame update
    void Awake()
    {
        fade.gameObject.SetActive(true);
        fade.DOFade(0, fadeDuration);
    }

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(GoToScene(sceneIndex));
    }

    private IEnumerator GoToScene(int index)
    {
        fade.DOFade(1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        SceneManager.LoadSceneAsync(index);
    }
}
