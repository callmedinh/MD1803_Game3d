using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.InputManager;

public class LoadGameManager : MonoBehaviour
{
    [Header("Spawner Setting")]
    [SerializeField] Transform spawner;
    [SerializeField] Vector3 spawnerOffset;
    [SerializeField] float spawnTime;

    [Header("Reference to Prefabs")]
    [SerializeField] GameObject bridge01Prefab;

    [Header("Bridge List & Layers")]
    [SerializeField] List<GameObject> bridges = new List<GameObject>();

    [Header("Main Camera Reference")]
    [SerializeField] Transform mainCamera;

    int gamePhase = 1;
    bool canSpawnNextBridge;

    float cameraSpeed;
    Vector3 cameraPos;

    Ray cameraRay;
    RaycastHit cameraRayInfo;
    float rayDistance = 5f;
    [SerializeField] LayerMask bridgeLayer;

    private void Start()
    {
        canSpawnNextBridge = true;

        cameraPos = Vector3.zero;
        cameraSpeed = spawnerOffset.z / spawnTime;
    }
    private void Update()
    {
        SpawnBridge();
        MoveCamera();
        DeleteBridge();
        LoadScence();
    }
    void SpawnBridge()
    {
        if (gamePhase == 1)
        {
            if (canSpawnNextBridge)
            {
                canSpawnNextBridge = false;

                bridges.Add(Instantiate(bridge01Prefab, spawner));
                foreach (GameObject bridge in bridges)
                {
                    bridge.transform.parent = null;
                }

                spawner.position = spawner.position + spawnerOffset;
                StartCoroutine(WaitForTime());
            }
        }
    }
    void MoveCamera()
    {
        if (gamePhase == 1)
        {
            cameraPos.z = cameraSpeed * Time.deltaTime;
            mainCamera.transform.position = mainCamera.transform.position + cameraPos;
        }
    }
    void DeleteBridge()
    {
        cameraRay.origin = mainCamera.transform.position;
        cameraRay.direction = -mainCamera.transform.up;

        Debug.DrawLine(cameraRay.origin,cameraRay.origin + (cameraRay.direction * rayDistance), Color.red);
        if (Physics.Raycast(cameraRay.origin, cameraRay.direction, out cameraRayInfo, rayDistance, bridgeLayer))
        {
            if (bridges.Contains(cameraRayInfo.collider.gameObject))
            {
                if (bridges.IndexOf(cameraRayInfo.collider.gameObject) > 0)
                {
                    Destroy(bridges[0]);
                    bridges.RemoveAt(0);
                }
            }
        }
    }
    void LoadScence()
    {
        if (VirtualInputManager.Instance.SwitchScene)
        {
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(spawnTime);
        canSpawnNextBridge = true;
    }
}
