using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    public GameObject player;
    public Player c_player;
    public float offsetX = 0f;
    public float offsetY = -50f;
    public float offsetZ = 0f;

    Vector3 Camposition;

    void LateUpdate()//lateUpdate는 update가 끝난뒤에 호출
    {
        Camposition.x = player.transform.position.x + offsetX;
        Camposition.y = player.transform.position.y + offsetY;
        Camposition.z = player.transform.position.z + offsetZ;

        transform.position = Camposition;
    }

    void Update()
    {
        if (c_player.b_MapCheck)
        {
            offsetY = -150f;
            if (Input.GetKey(KeyCode.UpArrow))//키가 눌렸는지 확인하는 함수
            {
                offsetZ += 20 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                offsetX += 20 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                offsetX -= 20 * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                offsetZ -= 20 * Time.deltaTime;
            }
        }
        else
        {
            offsetX = 0;
            offsetY = -50f;
            offsetZ = 0;
        }
        transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
    //미니맵 카메라가 플레이어 캐릭터 추적
}
