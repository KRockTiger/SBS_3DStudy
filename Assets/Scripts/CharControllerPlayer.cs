using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;
    private Camera camMain;

    [SerializeField] private float verticalVelocity = 0f; //캐릭터의 y값
    private float gravity = 9.81f; //중력값
    [SerializeField] private bool isSlope = false;
    private Vector3 slopeVelocity;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        camMain = Camera.main;
    }

    private void Update()
    {
        CheckMouseLock();
        Rotation();
        CheckGround();
        Moving();
        Jump();
        CheckGravity();
        CheckSlope();
    }

    private void CheckMouseLock()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (Cursor.lockState)
            {
                case CursorLockMode.Locked:
                    Cursor.lockState = CursorLockMode.None;
                    break;

                case CursorLockMode.None:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
            }
        }
    }

    private void Rotation()
    {
        transform.rotation = Quaternion.Euler(0f, camMain.transform.eulerAngles.y, 0f);
    }

    private void CheckGround()
    {
        isGround = false;

        if (verticalVelocity <= 0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down,
                       characterController.height * 0.55f, LayerMask.GetMask("Ground"));
        }

        //isGround = characterController.isGrounded; //스스로 인식, 유니티 버전에 따라 인식이 다름
    }

    private void Moving()
    {
        moveDir = new Vector3(InputHorizontal(), 0f, InputVertical());

        if (isSlope) //만약 미끄러짐 트리거가 발생했다면 (땅에 있을 때만 적용할 수 있게 추후 수정하기)
        {
            characterController.Move(-slopeVelocity * Time.deltaTime); //실시간으로 미끄러지게 함
        }
        characterController.Move(transform.rotation * moveDir * Time.deltaTime);
    }

    private float InputHorizontal()
    {
        return Input.GetAxisRaw("Horizontal") * moveSpeed;
    }

    private float InputVertical()
    {
        return Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    private void Jump()
    {
        if (!isGround || isSlope)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void CheckGravity()
    {
        verticalVelocity -= gravity * Time.deltaTime;

        if (isGround)
        {
            verticalVelocity = 0f;
        }

        if (isJump)
        {
            isJump = false;
            verticalVelocity = jumpForce;
        }

        characterController.Move(new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    private void CheckSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit,
            characterController.height, LayerMask.GetMask("Ground")))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up); //hit.normal => 법선 : 바닥에 반사된 빛의 각도를 나타내는 선(?)
        
            if (angle >= characterController.slopeLimit) //법선의 각도가 캐릭터의 미끄러질 각도보다 높으면
            {
                isSlope = true; //미끄리지게 하기
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0f, gravity, 0f), hit.normal); //정해준 법선을 축 기준으로 각도로 계산해주는 함수
            }

            else
            {
                isSlope = false;
            }
        }
    }
}
