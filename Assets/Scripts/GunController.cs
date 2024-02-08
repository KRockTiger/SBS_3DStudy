using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject objBullet; //생성할 총알
    [SerializeField] private Transform trsMuzzle; //생성활 위치
    [SerializeField] private Transform trsDynamic;
    private Camera camMain; //표적을 카메라 시선 기준으로 맞추기 때문에 사용
    private float distance = 250f; //사격 거리
    [SerializeField] private float gunForce = 100f; //총알을 발사할 힘

    [Space]
    [SerializeField] private bool isGrenade;

    private void Start()
    {
        camMain = Camera.main;
    }

    private void Update()
    {
        GunPointer();
        CheckFire();
        CheckGrenade();
    }

    /// <summary>
    /// 총기가 카메라 한가운데 보이는 오브젝트를 노리도록 만들어줄 함수
    /// </summary>
    private void GunPointer()
    {
        if (Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, distance, LayerMask.GetMask("Ground"))) //(발사 좌표, 발사 방향, 탐지를 담당할 ...) 
        {
            transform.LookAt(hit.point);
        }

        else //그라운드 오브젝트에 레이케스트가 닿지 않았을 때
        {
            Vector3 lookPos = camMain.transform.position +
                              camMain.transform.forward * distance;
                              //허공의 공간에서 설정한 최대거리만큼 위치에 좌표고정
            
            transform.LookAt(lookPos);
        }
    }

    /// <summary>
    /// 발사 트리거
    /// </summary>
    private void CheckFire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShotBullet();
        }
    }

    /// <summary>
    /// 유탄모드 확인
    /// </summary>
    private void CheckGrenade()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isGrenade = false;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isGrenade = true;
        }
    }

    /// <summary>
    /// 총알 생성 및 발사
    /// </summary>
    private void ShotBullet()
    {
        GameObject go = Instantiate(objBullet, trsMuzzle.position, trsMuzzle.rotation, trsDynamic);

        BulletController sc = go.GetComponent<BulletController>();

        if (isGrenade) //현재 무기가 유탄이라면
        {
            sc.P_AddForce(gunForce * 0.5f);
        }

        //밑에는 무기가 유탄이 아니라면
        //숙제 : 위 조건문과 똑같은 코드이므로 한번 축약해보기
        else if (Physics.Raycast(camMain.transform.position, camMain.transform.forward,
            out RaycastHit hit, distance, LayerMask.GetMask("Ground"))) //(발사 좌표, 발사 방향, 탐지를 담당할 ...) 
        {
            sc.P_SetDestination(hit.point, gunForce);
        }

        else //그라운드 오브젝트에 레이케스트가 닿지 않았을 때
        {
            Vector3 lookPos = camMain.transform.position +
                              camMain.transform.forward * 1000f;
            //허공의 공간에서 설정한 최대거리만큼 위치에 좌표고정

            sc.P_SetDestination(lookPos, gunForce);
        }
    }
}
