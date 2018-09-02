using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnControl : MonoBehaviour {

    public Transform[] points;

    public Color roomColor;

    public string areaName;
    public string audioName;

    private GameObject playerGO;
    private GameObject cameraGO;
    private Text infoText;

    private int spawnIndex;

    void Awake()
    {
        playerGO = GameObject.Find("Player");
        cameraGO = GameObject.Find("Main Camera");
        infoText = GameObject.Find("IntroductionText").GetComponent<Text>();

        infoText.text = areaName;

        spawnIndex = playerGO.GetComponent<PlayerControl>().pointIndex;
    }

    void Start () {
        playerGO.transform.position = points[spawnIndex].position;
        cameraGO.transform.position = new Vector3(points[spawnIndex].position.x, points[spawnIndex].position.y, -10f);
        GameObject.Find("IntroductionText").GetComponent<Animator>().Play("NewAreaActive");

        AudioManager.instance.PlaySound(audioName);

        Camera.main.backgroundColor = roomColor;
	}
}
