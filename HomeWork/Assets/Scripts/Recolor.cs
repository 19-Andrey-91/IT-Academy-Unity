using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recolor : MonoBehaviour
{
    [Space]
    [SerializeField] private Button button1;
    [SerializeField] private Texture2D texture1;

    [Space]
    [SerializeField] private Button button2;
    [SerializeField] private Texture2D texture2;

    [Space]
    [SerializeField] private Button button3;
    [SerializeField] private Texture2D texture3;

    [Space]
    [SerializeField] private Button button4;
    [SerializeField] private Texture2D texture4;

    StarShipChange starShipChange;
    GameObject activeStarShip;



    void Start()
    {
        starShipChange = GetComponent<StarShipChange>();

        button1.onClick.AddListener(()=> ChangeTexture(texture1));
        button2.onClick.AddListener(() => ChangeTexture(texture2));
        button3.onClick.AddListener(() => ChangeTexture(texture3));
        button4.onClick.AddListener(() => ChangeTexture(texture4));
    }

    private void ChangeTexture(Texture2D texture)
    {
        activeStarShip = starShipChange.ActiveStarShip;
        activeStarShip.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
