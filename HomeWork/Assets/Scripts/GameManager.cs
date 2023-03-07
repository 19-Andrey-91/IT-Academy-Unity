using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    protected static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameManager>();

            if (instance != null)
                return instance;

            Create();

            return instance;
        }
    }

    [SerializeField] Character character2d;
    [SerializeField] IsometricCharacter isometricCharacter;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas playerInterface;

    Button[] buttonsMainCanvas;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Canvas instantiateMainCanvas = Instantiate(mainCanvas);
        buttonsMainCanvas = instantiateMainCanvas.GetComponentsInChildren<Button>();
        buttonsMainCanvas[0].onClick.AddListener(StartScene1);
        buttonsMainCanvas[1].onClick.AddListener(StartScene2);
    }

    private void StartScene1()
    {
        StartCoroutine(LoadingScene(1));
    }

    private void StartScene2()
    {
        StartCoroutine(LoadingScene(2));
    }

    IEnumerator LoadingScene(int numberScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(numberScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        InstantiateLogic(numberScene);
    }

    public static GameManager Create()
    {
        GameObject gameManagerGameObject = new GameObject("GameManager");
        instance = gameManagerGameObject.AddComponent<GameManager>();

        return instance;
    }

    private void InstantiatePlayerInterface(UnityAction startScene)
    {
        Canvas instantiatePlayerInterface = Instantiate(playerInterface);
        Button changeLevel = instantiatePlayerInterface.GetComponentInChildren<Button>();
        changeLevel.GetComponentInChildren<TextMeshProUGUI>().text = startScene.Method.Name;
        changeLevel.onClick.AddListener(startScene);
    }

    private void InstantiateLogic(int numberScene)
    {
        switch (numberScene)
        {
            case 1:
                Instantiate(character2d);
                InstantiatePlayerInterface(StartScene2);
                break;
            case 2:
                Instantiate(isometricCharacter);
                InstantiatePlayerInterface(StartScene1);
                break;
            default:
                break;
        }
    }
}
