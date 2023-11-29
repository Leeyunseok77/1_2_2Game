using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 플레이어 오브젝트의 Transform 컴포넌트를 여기에 연결하세요.
    public Vector3 offset;   // 카메라와 플레이어 간의 오프셋을 조절하세요.

    void LateUpdate()
    {
        if (target != null)
        {
            // 플레이어의 위치에 오프셋을 더한 위치로 카메라를 이동시킵니다.
            transform.position = target.position + offset;
        }
    }
}