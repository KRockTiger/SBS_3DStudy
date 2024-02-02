using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyPlayer : MonoBehaviour
{
    //private float gravity = 9.81f; //9.81로 지구의 중력값을 표현
    //[SerializeField] private float verticalVelocity = 0f; //받는 중력의 힘
    [SerializeField] private bool isGround = false; //바닥 터치 여부
    private bool isJump = false; //점프 여부
    private Vector3 moveDir; //이동 방향
    private Rigidbody rigid; //플레이어에 붙여있는 RigidBody 사용
    private CapsuleCollider cap;

    [Header("플레이어 스텟")]
    [SerializeField] private float moveSpeed = 2f; //이동속도
    [SerializeField] private float jumpForce = 5f; //점프력
    [SerializeField, Tooltip("마우스의 감도 값")] private float mouseSensivity = 5f; //마우스 감도
    private Vector2 rotateValue; //마우스 회전 각도값

    private Transform trsCam;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        cap = GetComponent<CapsuleCollider>();
        trsCam =transform.GetChild(0);
        //trsCam = transform.Find("Main Camera");
        //trsCam = GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        CheckGround();
        Moving();
        Jumping();
        CheckGravity();

        Rotation();
    }

    /// <summary>
    /// 플레이어가 땅에 닿아 있는지 체크
    /// </summary>
    private void CheckGround()
    {
        if (rigid.velocity.y <= 0)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down,
                       cap.height * 0.55f, LayerMask.GetMask("Ground"));
        }

        else if (rigid.velocity.y > 0)
        {
            isGround = false;
        }
    }

    /// <summary>
    /// 플레이어의 움직임
    /// </summary>
    private void Moving()
    {
        //moveDir.x = InputHorizontal();
        //moveDir.y = rigid.velocity.y;
        //moveDir.z = InputVertical();
        //rigid.velocity = transform.rotation * moveDir;

        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(new Vector3(0, 0, moveSpeed), ForceMode.Force);
        }

        else if (Input.GetKey(KeyCode.S))
        {            
            rigid.AddForce(new Vector3(0, 0, -moveSpeed), ForceMode.Force);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(new Vector3(-moveSpeed, 0, 0), ForceMode.Force);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(new Vector3(moveSpeed, 0, 0), ForceMode.Force);
        }
    }

    private float InputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private float InputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    /// <summary>
    /// 플레이어 점프
    /// </summary>
    private void Jumping()
    {
        if (!isGround)
        { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            //verticalVelocity = jumpForce;
        }
    }

    /// <summary>
    /// 중력 확인
    /// </summary>
    private void CheckGravity()
    {
        #region 나의 방법
        //if (isGround && !isJump) //점프중이 아닌상태에서 땅에 붙어있으면
        //{
        //    verticalVelocity = 0f;
        //}

        //else if (!isGround) //점프 등 이유로 땅에 붙어있지 않으면
        //{
        //    verticalVelocity -= gravity * Time.deltaTime; //임의의 중력으로 눌러준 것
        //}

        //if (isJump && !isGround)
        //{
        //    if (verticalVelocity < 0f)
        //    {
        //        isJump = false;
        //    }
        //}

        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime;
        //}

        //rigid.velocity = new Vector3(rigid.velocity.x, verticalVelocity, rigid.velocity.z);
        #endregion

        if (isJump)
        {
            isJump = false;
            rigid.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse); //물리의 방법으로 적용
        }

    }

    /// <summary>
    /// 카메라 회전
    /// </summary>
    private void Rotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensivity * Time.deltaTime;

        rotateValue += new Vector2(-mouseY, mouseX);

        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(new Vector3(0, rotateValue.y, 0)); //좌우는 괜찮음
        //위아래로 움직일 때 캐릭터가 위 아래로 회전

        trsCam.rotation = Quaternion.Euler(rotateValue.x, rotateValue.y, 0); //위 아래는 괜찮음
        //좌우로 움직일 때 캐릭터가 좌우로 움직이지 않음
    }
}
