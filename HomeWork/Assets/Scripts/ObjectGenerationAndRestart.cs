using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectGenerationAndRestart : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    public void ObjectGenerate()
    {
        foreach (GameObject obj in objects)
        {
            Instantiate(obj);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
