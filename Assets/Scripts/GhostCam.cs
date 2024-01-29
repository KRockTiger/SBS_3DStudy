using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f; //마우스 감도
    [SerializeField] private float mouseMoveSpeed = 5f; //마우스 이동속도
    private Vector3 rotateValue; //로테이트 값을 담을 변수

    private void Start()
    {
        rotateValue = transform.rotation.eulerAngles; //쿼터니언을 Vector3로 형변환
    }

    private void Update()
    {
        Moving(); //이동기능
        Rotating(); //회전기능
        //CheckFrame();
    }

    /// <summary>
    /// 이동기능을 넣은 함수
    /// </summary>
    private void Moving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position +=
            //  transform.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;
                Vector3.forward * mouseMoveSpeed * Time.deltaTime; //글로벌
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position +=
            //  -transform.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
                transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position +=
            //  -transform.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
                transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position +=
            //  transform.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
                transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //transform.position +=
            //   Vector3.up * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //transform.position +=
            //   Vector3.down * mouseMoveSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// 회전기능을 넣은 함수
    /// </summary>
    private void Rotating()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotateValue += new Vector3(-mouseY, mouseX);
        transform.rotation = Quaternion.Euler(rotateValue); //Vector3값을 Quaternion값으로 전환
    }

    /// <summary>
    /// 프레임을 확인하는 bool형 함수
    /// </summary>
    /// <returns></returns>
    private bool CheckFrame(int _limitFrame)
    {
        //대충 1프레임당 0.016초가 걸린다.
        int curFrame = (int)(1 / Time.deltaTime); //현재 프레임을 나타냄
                                                  //==> int형으로 만들어 소수점을 버리게 만든다.

        //if (_limitFrame < curFrame)
        //{
        //    return true;
        //}

        //else
        //{
        //    return false;
        //}=>

        return _limitFrame < curFrame;
    }
}
