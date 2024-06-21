using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPos;
    private Vector3 temPos;
    // Start is called before the first frame update
    void Start()
    {
        temPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        temPos = new(playerPos.position.x, transform.position.y, playerPos.position.z);
        transform.position = temPos;
    }
}
