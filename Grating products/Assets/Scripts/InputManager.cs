using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool _userInput;
    void Update()
    {
        _userInput = Input.GetKey(KeyCode.Mouse0);
    }

    public bool GetUserInput()
    {
        return _userInput;
    }
}
