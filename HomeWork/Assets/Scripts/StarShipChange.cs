using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarShipChange : MonoBehaviour
{
    [SerializeField] private List<GameObject> listStarShips;
    [SerializeField] private Button next;
    [SerializeField] private Button previous;

    private int index = 0;
    public GameObject ActiveStarShip
    {
        get
        {
            if (listStarShips != null)
            {
                return listStarShips[index];
            }
            else
            {
                return null;
            }
        }
    }
    void Start()
    {
        if (listStarShips.Count == 0)
        {
            return;
        }

        listStarShips[0].SetActive(true);

        next.onClick.AddListener(() => Next(true));
        previous.onClick.AddListener(() => Next(false));

    }

    private void Next(bool next)
    {
        listStarShips[index].SetActive(false);

        if (next)
        {
            index++;
            if (index > listStarShips.Count - 1)
            {
                index = 0;
            }
        }
        else
        {
            index--;
            if (index < 0)
            {
                index = listStarShips.Count - 1;
            }
        }

        listStarShips[index].SetActive(true);
    }

}
