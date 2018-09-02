using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

    ParticleSystem self;

    void Awake()
    {
        self = GetComponent<ParticleSystem>();
    }

    void Update () {
        if (!self.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}
