using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Animator anim;
    public float moveSpeed = 10.0f;
   // private bool isMoving = false;

    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        //    if (moveDirection != Vector3.zero)
        //    {
        //        if (!isMoving)
        //        {
        //            anim.SetBool("Run", true); // "Run" 파라미터를 true로 설정
        //            isMoving = true;
        //        }

        // 캐릭터를 입력 방향으로 회전
        float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.Euler(0, targetAngle, 0);

        tr.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        //    }
        //    else
        //    {
        //        if (isMoving)
        //        {
        //            anim.SetBool("Run", false); // "Run" 파라미터를 false로 설정
        //            isMoving = false;
        //        }
        //    }
    }
}