using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneSwitch : MonoBehaviour
{
    public bool LevelStart = false;

    //This checks if the button was pushed to start the level
    public void SwitchScenestoNewLevel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            LevelStart = true;
        }
    }

}
