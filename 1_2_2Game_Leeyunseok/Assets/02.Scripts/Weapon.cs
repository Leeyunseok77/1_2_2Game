using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float impactForce = 10.0f; // ƨ���� ������ ��

    // �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            // ���Ϳ� Rigidbody�� �ִ��� Ȯ��
            Rigidbody monsterRigidbody = other.GetComponent<Rigidbody>();
            if (monsterRigidbody != null)
            {
                // ������ �������� ���͸� �о
                Vector3 forceDirection = transform.forward;
                monsterRigidbody.velocity = forceDirection * impactForce;
            }

            // ���⼭�� ���Ϳ��� �������� �ְų� �ı��ϴ� ���� �߰����� ó���� �� �� �ֽ��ϴ�.

        }
    }
}