using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float impactForce = 10.0f; // 튕겨져 나가는 힘

    // 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            // 몬스터에 Rigidbody가 있는지 확인
            Rigidbody monsterRigidbody = other.GetComponent<Rigidbody>();
            if (monsterRigidbody != null)
            {
                // 무기의 방향으로 몬스터를 밀어냄
                Vector3 forceDirection = transform.forward;
                monsterRigidbody.velocity = forceDirection * impactForce;
            }

            // 여기서는 몬스터에게 데미지를 주거나 파괴하는 등의 추가적인 처리를 할 수 있습니다.

        }
    }
}