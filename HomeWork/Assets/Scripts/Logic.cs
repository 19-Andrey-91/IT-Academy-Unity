using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Logic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] cells = new TextMeshProUGUI[16];
    
    
    int row = 4;
    int column = 4;

    int[][] array;

    Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Keyboard.Up.started += UpCell;
        controls.Keyboard.Down.started += DownCell;
        controls.Keyboard.Left.started += LeftCell;
        controls.Keyboard.Right.started += RightCell;
    }


    private void OnDisable()
    {
        controls.Keyboard.Up.started -= UpCell;
        controls.Keyboard.Down.started -= DownCell;
        controls.Keyboard.Left.started -= LeftCell;
        controls.Keyboard.Right.started -= RightCell;
        controls.Disable();
    }

    private void Start()
    {
        array = new int[row][];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new int[column];
        }
        Generate(2);
    }

    private void Generate(int _count)
    {
        int count = _count;

        while (count > 0 && CheckEmptySlots())
        {
            int rowPosition = UnityEngine.Random.Range(0, row );
            int columnPosition = UnityEngine.Random.Range(0, column);
            if (array[rowPosition][columnPosition] == 0)
            {
                array[rowPosition][columnPosition] = 2;
                count--;
            }
        }
        UpdateCells();
    }

    private void multiplyLeftRight()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column-1; j++)
            {
                if (array[i][j] == array[i][j + 1])
                {
                    array[i][j] *= 2;
                    if (array[i][j] == 2048)
                    {
                        Win();
                    }
                    array[i][j + 1] = 0;
                }
            }
        }
        UpdateCells();
    }

    private void multiplyUpDown()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column - 1; j++)
            {
                if (array[j][i] == array[j+1][i])
                {
                    array[j][i] *= 2;
                    if (array[j][i] == 2048)
                    {
                        Win();
                    }
                    array[j+1][i] = 0;
                }
            }
        }
        UpdateCells();
    }

    private void Left()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column - 1; j++)
            {
                if (array[i][j] == 0)
                {
                    array[i][j] = array[i][j + 1];
                    array[i][j + 1] = 0;
                }
            }
        }
        UpdateCells();
    }

    private void Right()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = column - 1; j > 0; j--)
            {
                if (array[i][j] == 0)
                {
                    array[i][j] = array[i][j - 1];
                    array[i][j - 1] = 0;
                }
            }
        }
        UpdateCells();
    }

    private void Up()
    {
        for (int i = 0; i < row - 1; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (array[i][j] == 0)
                {
                    array[i][j] = array[i + 1][j];
                    array[i + 1][j] = 0;
                }
            }
        }
        UpdateCells();
    }

    private void Down()
    {
        for (int i = row -1; i > 0; i--)
        {
            for (int j = 0; j < column; j++)
            {
                if (array[i][j] == 0)
                {
                    array[i][j] = array[i - 1][j];
                    array[i - 1][j] = 0;
                }
            }
        }
        UpdateCells();
    }

    private void UpdateCells()
    {
        int celCount = 0;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                cells[celCount].text =  array[i][j].ToString();
                celCount++;
            }
        }
    }

    private void UpCell(InputAction.CallbackContext obj)
    {
        RunMove(4, Up);
        multiplyUpDown();
        RunMove(2, Up);
        Generate(1);
    }

    private void DownCell(InputAction.CallbackContext obj)
    {
        RunMove(4, Down);
        multiplyUpDown();
        RunMove(2, Down);
        Generate(1);
    }

    private void LeftCell(InputAction.CallbackContext obj)
    {
        RunMove(4, Left);
        multiplyLeftRight();
        RunMove(2, Left);
        Generate(1);
    }

    private void RightCell(InputAction.CallbackContext obj)
    {
        RunMove(4, Right);
        multiplyLeftRight();
        RunMove(2, Right);
        Generate(1);
    }

    void Win()
    {
        Debug.Log("Win");
    }

    private void RunMove(int num, Action action)
    {
        for(int i = 0;i <= num; i++)
        {
            action();
        }
    }

    private bool CheckEmptySlots()
    {
        bool empty = false;
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (array[i][j] == 0)
                {
                    empty = true;
                }
            }
        }
        return empty;
    }
}
