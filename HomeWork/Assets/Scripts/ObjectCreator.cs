using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;

    private GameObject instance;
    private GameObject prefab;

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 400, 50), "Press SPACEBAR to continue");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (prefabs == null)
            {
                Debug.LogError("Prefab is NULL!!!");
                return;
            }

            prefab = prefabs[Random.Range(0, prefabs.Count)];

            if (instance != null)
            {
                Destroy(instance);
            }

            var rotation = Quaternion.identity;
            var position = new Vector3(Random.Range(-5.0f, 5.0f), 0.5f, Random.Range(-5.0f, 5.0f));
            instance = Instantiate(prefab, position, rotation);
        }
    }
}
