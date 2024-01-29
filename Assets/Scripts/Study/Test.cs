using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        //MouseFunction();

    }

    /// <summary>
    /// 마우스 커서 표시 코딩
    /// </summary>
    private void MouseFunction()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Cursor.lockState == CursorLockMode.None) //마우스가 보이고 조절가능한 상태
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            else if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
/*
01.29 수업
//스크립트를 가진 "오브젝트 기준" 회전값을 기준으로 방향
transform.up;
transform.forward;
transform.right;
==> 여기서 down, back, left 코드는 없으므로 -를 붙여서 코딩한다. ex) -transform.up;

//글로벌 포지션 기준으로 방향
Vector3.up;
Vector3.forward;
Vector3.right;

//3D는 2D 상위호환?? => 결론은 아니다 : 각각 좋은 게임을 만들 수 있는 환경이 있기 때문

코드-----------------------------------------------------------------------------
//transform.position += transform.forward * Time.deltaTime; //전방으로 이동

//현업에 사용하는 코드
//transform.position += Rotation * transform.Position;
//transform.position += transform.rotation * Vector3.forward * Time.deltaTime;
//transform.position += transform.TransformDirection(Vector3.forward); 
*/
