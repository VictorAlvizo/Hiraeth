using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSafe : MonoBehaviour {

    public static bool inWorld = false;

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
}
