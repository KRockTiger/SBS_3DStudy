using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class CharControllerPlayer : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDir;

    private float verticalVelocity = 0f;
    private float gravity = 9.81f;
    private bool isSlope = false;
    private Vector3 slopeVelocity;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [SerializeField] private bool isGround = false;
    [SerializeField] private bool isJump = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CheckGround();
        Moving();
        Jump();
        CheckGravity();
    }

    private void CheckGround()
    {
        isGround = false;

        if (verticalVelocity < 0f)
        {
            isGround = Physics.Raycast(transform.position, Vector3.down,
                       characterController.height * 0.55f, LayerMask.GetMask("Ground"));
        }

        //isGround = characterController.isGrounded; //스스로 인식
    }

    private void Moving()
    {
        moveDir = new Vector3(InputHorizontal(), 0f, InputVertical());

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
        if (!isGround)
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
}
