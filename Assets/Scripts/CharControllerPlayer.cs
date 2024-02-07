using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;
    private Camera camMain;

    [SerializeField] private float verticalVelocity = 0f; //ĳ������ y��
    private float gravity = 9.81f; //�߷°�
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

        //isGround = characterController.isGrounded; //������ �ν�, ����Ƽ ������ ���� �ν��� �ٸ�
    }

    private void Moving()
    {
        moveDir = new Vector3(InputHorizontal(), 0f, InputVertical());

        if (isSlope) //���� �̲����� Ʈ���Ű� �߻��ߴٸ� (���� ���� ���� ������ �� �ְ� ���� �����ϱ�)
        {
            characterController.Move(-slopeVelocity * Time.deltaTime); //�ǽð����� �̲������� ��
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
            float angle = Vector3.Angle(hit.normal, Vector3.up); //hit.normal => ���� : �ٴڿ� �ݻ�� ���� ������ ��Ÿ���� ��(?)
        
            if (angle >= characterController.slopeLimit) //������ ������ ĳ������ �̲����� �������� ������
            {
                isSlope = true; //�̲������� �ϱ�
                slopeVelocity = Vector3.ProjectOnPlane(new Vector3(0f, gravity, 0f), hit.normal); //������ ������ �� �������� ������ ������ִ� �Լ�
            }

            else
            {
                isSlope = false;
            }
        }
    }
}
