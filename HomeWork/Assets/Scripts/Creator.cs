
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Creator : MonoBehaviour
{
    [SerializeField] private List<Subject> subjects = new List<Subject>();

    private SubjectControls controls;

    private void Awake()
    {
        controls = new SubjectControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Subject.Keyboard.started += Spawn;
    }

    private void OnDisable()
    {
        controls.Subject.Keyboard.started -= Spawn;
        controls.Disable();
    }

    private void Start()
    {
        if(subjects.Count == 0)
        {
            throw new UnityException("List subject is empty");
        }
    }

    private void Spawn(InputAction.CallbackContext obj)
    {
        int index = Random.Range(0, subjects.Count);
        Instantiate(subjects[index]);
    }
}
