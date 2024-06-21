using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.InputManager;

public class AttackControl : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (VirtualInputManager.Instance.Slash)
        {
            animator.SetTrigger("Slash");
        }
    }
}
