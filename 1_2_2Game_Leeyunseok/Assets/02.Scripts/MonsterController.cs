using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 5.0f; // ���� �̵� �ӵ�
    public float knockbackForce = 10.0f; // ���⿡ �¾��� �� ƨ���� ������ ��

    // ���Ͱ� ����� �� ȣ��� �̺�Ʈ
    public UnityEvent OnMonsterDisappear = new UnityEvent();

    private void Start()
    {
        // ���Ͱ� ������ ������ �÷��̾� �������� ���� �̵��ϵ��� ����
        MoveInPlayerDirection();

        // 5�� �Ŀ� DestroyMonster �Լ� ȣ��
        Invoke("DestroyMonster", 3.0f);
    }

    void MoveInPlayerDirection()
    {
        // �÷��̾� ������Ʈ�� ã��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // �÷��̾��� ��ġ���� ���Ͱ� ������ ��ġ�� ���ϴ� ������ ���
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // ������ ���� ����
            transform.forward = directionToPlayer;

            // ���Ϳ��� �̵� ��� �߰� (Rigidbody ���)
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
        // �̺�Ʈ ȣ��
        OnMonsterDisappear.Invoke();

        // 5�� �Ŀ� ȣ��Ǿ� ���� ������Ʈ�� ����
        Destroy(gameObject);
    }

    // �浹�� ������ �� ȣ��Ǵ� �ݹ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Monster Collision with Player!");

            // ���� ������Ʈ�� ����
            DestroyMonster();
        }
        else if (other.CompareTag("Weapon"))
        {
            Debug.Log("Monster Collision with Weapon!");

            // ����� �浹�ϸ� ���͸� ƨ�ܳ����� ��
            Rigidbody monsterRigidbody = GetComponent<Rigidbody>();
            if (monsterRigidbody != null)
            {
                // ������ �������� ���� ����
                Vector3 weaponDirection = (other.transform.position - transform.position).normalized;
                monsterRigidbody.AddForce(weaponDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}