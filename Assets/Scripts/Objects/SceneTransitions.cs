using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{

    [SerializeField] private string sceneToLoad;
    [SerializeField] private Vector2 playerPosition;
    public VectorValue playerStorage;
    [SerializeField] private GameObject fadeInPanel;
    [SerializeField] private GameObject fadeOutPanel;
    [SerializeField] private float fadeWait;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initalValue = playerPosition;
            StartCoroutine(FadeCo());
        }
    }
    private IEnumerator FadeCo()
    {
        if (fadeOutPanel = null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
