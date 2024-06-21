using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.InputManager
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                VirtualInputManager.Instance.Slash = true;
            } else
            {
                VirtualInputManager.Instance.Slash = false;
            }
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                VirtualInputManager.Instance.PickUp = true;
            } else
            {
                VirtualInputManager.Instance.PickUp = false;
            }
            if (Input.GetKey(KeyCode.F))
            {
                VirtualInputManager.Instance.Drop = true;
            } else
            {
                VirtualInputManager.Instance.Drop = false;
            }
            if (Input.GetKeyDown (KeyCode.Escape)) 
            { 
                VirtualInputManager.Instance.Escape = true;
            } else
            {
                VirtualInputManager.Instance.Escape = false;
            }
            if (Input.GetMouseButton(0))
            {
                VirtualInputManager.Instance.SwitchScene = true;
            } else
            {
                VirtualInputManager.Instance.SwitchScene= false;
            }
        }
    }
}
