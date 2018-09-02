using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float cameraSpeed;

    public Transform targetLocation;

    private static bool inWorld = false;

    void Awake()
    {
        if (!inWorld)
        {
            inWorld = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 newPos = Vector2.Lerp(transform.position, targetLocation.position, Time.deltaTime * cameraSpeed);

        Vector3 cameraPos = new Vector3(newPos.x, newPos.y, -10f);

        transform.position = cameraPos;
    }
}
