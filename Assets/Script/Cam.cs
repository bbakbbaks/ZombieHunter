using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    public GameObject player;
    public float offsetX = 0f;
    public float offsetY = 10f;
    public float offsetZ = -20f;

    Vector3 Camposition;

    void LateUpdate()
    {
        Camposition.x = player.transform.position.x + offsetX;
        Camposition.y = player.transform.position.y + offsetY;
        Camposition.z = player.transform.position.z + offsetZ;

        transform.position = Camposition;
    }

}
