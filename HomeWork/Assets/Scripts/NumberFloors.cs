using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberFloors : MonoBehaviour
{
    [SerializeField] private TextMeshPro floorUP;
    [SerializeField] private TextMeshPro floorDOWN;

    private int floor1 = 1;
    private int floor2 = 2;

    public int Floor { get => floor1; private set => floor1 = value; } 

    public void Add()
    {
        if(Floor > 1)
        {
            --floor1;
            --floor2;
            ShowNumberFloor();
        }
    }

    public void Subtract() 
    {
        ++floor1;
        ++floor2;
        ShowNumberFloor();
    }

    private void ShowNumberFloor()
    {
        floorUP.text = $"{floor1}";
        floorDOWN.text = $"{floor2}";
    }
}
