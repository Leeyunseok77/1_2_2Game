using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 몬스터 이동 속도
    public float knockbackForce = 10.0f; // 무기에 맞았을 때 튕겨져 나가는 힘

    // 몬스터가 사라질 때 호출될 이벤트
    public UnityEvent OnMonsterDisappear = new UnityEvent();

    private void Start()
    {
        // 몬스터가 생성될 때마다 플레이어 방향으로 직선 이동하도록 설정
        MoveInPlayerDirection();

        // 5초 후에 DestroyMonster 함수 호출
        Invoke("DestroyMonster", 3.0f);
    }

    void MoveInPlayerDirection()
    {
        // 플레이어 오브젝트를 찾음
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 플레이어의 위치에서 몬스터가 생성된 위치로 향하는 방향을 계산
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // 몬스터의 방향 설정
            transform.forward = directionToPlayer;

            // 몬스터에게 이동 명령 추가 (Rigidbody 사용)
            Rigidbody monsterRigidbody = GetComponent<Rigidbody>();
            if (monsterRigidbody != null)
            {
                monsterRigidbody.velocity = directionToPlayer * moveSpeed;
            }
        }
        else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    void DestroyMonster()
    {
        // 이벤트 호출
        OnMonsterDisappear.Invoke();

        // 5초 후에 호출되어 몬스터 오브젝트를 삭제
        Destroy(gameObject);
    }

    // 충돌이 감지될 때 호출되는 콜백 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Monster Collision with Player!");

            // 몬스터 오브젝트를 삭제
            DestroyMonster();
        }
        else if (other.CompareTag("Weapon"))
        {
            Debug.Log("Monster Collision with Weapon!");

            // 무기와 충돌하면 몬스터를 튕겨나가게 함
            Rigidbody monsterRigidbody = GetComponent<Rigidbody>();
            if (monsterRigidbody != null)
            {
                // 무기의 방향으로 힘을 가함
                Vector3 weaponDirection = (other.transform.position - transform.position).normalized;
                monsterRigidbody.AddForce(weaponDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}