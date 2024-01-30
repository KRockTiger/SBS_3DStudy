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

        Cursor.lockState = CursorLockMode.Locked; //게임 시작하면 마우스 잠구기
    }

    /// <summary>
    /// 마우스 커서 표시
    /// </summary>
    private void CheckMouse()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked; //마우스가 보이지 않도록 항상 마우스는 화면의 가운데 존재
            }

            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void Update()
    {
        Moving(); //이동기능
        Rotating(); //회전기능
        CheckMouse();
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
                transform.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.forward) * mouseMoveSpeed * Time.deltaTime;
            //  Vector3.forward * mouseMoveSpeed * Time.deltaTime; //글로벌
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position +=
                -transform.forward * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.back * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.back) * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.position +=
                -transform.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.left * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.left) * mouseMoveSpeed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position +=
                transform.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.rotation * Vector3.right * mouseMoveSpeed * Time.deltaTime;
            //  transform.TransformDirection(Vector3.right) * mouseMoveSpeed * Time.deltaTime;
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

        //if (rotateValue.x > 90f)
        //{
        //    rotateValue.x = 90f;
        //}

        //else if (rotateValue.x < -90f)
        //{
        //    rotateValue.x = -90f;
        //} ==> 회전값을 제한

        //위 주석문에 적힌 코드를 아래코드로 간단하게 표현 가능
        //Mathf.Clamp() == 어떠한 값을 설정한 최댓값과 최솟값사이에만 적용하도록 설정
        rotateValue.x = Mathf.Clamp(rotateValue.x, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(rotateValue); //Vector3값을 Quaternion값으로 전환

        //SetActive는 게임 오브젝트를 활성화/비활성화를 할 수 있음
        //Enabled는 컴포넌트를 활성화/비활성화를 할 수 있음
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
