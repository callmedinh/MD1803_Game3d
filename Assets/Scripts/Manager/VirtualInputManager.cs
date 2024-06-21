using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.InputManager
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool Slash;
        public bool GrabItem;
        public bool PickUp;
        public bool Drop;
        public bool Escape;
        public bool SwitchScene;
    }
}
