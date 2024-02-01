using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Cams //미리 정의되어야만 하는 데이터
{
    MainCam,
    SubCam01,
    SubCam02,
    SubCam03,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    //private List<Camera> listCam = new List<Camera>(); //직렬화를 하거나 공개 -> 인스펙터 생성
    //private로 막아놨을 경우 new를 이용하여 동적할당 추가하기

    [SerializeField] private List<Camera> listCam; //카메라를 넣을 리스트
    [SerializeField] private List<Button> listBtns; //버튼을 넣을 리스트

    private UnityAction _action = null;
    public UnityAction Action { set => _action = value; }

    public void AddAction(UnityAction _addAction)
    {
        _action += _addAction;
    }

    public void RemoveAction(UnityAction _removeAction)
    {
        _action -= _removeAction;
    }

    [SerializeField] private bool forTest;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        //Camera[] arrCam = FindObjectsOfType<Camera>(); //GameObject.Find
        //listCam.AddRange(arrCam); //카메라 배열함수를 만들어 넣은 다음에 리스트에 그대로 추가하기
        //=> 직렬화 또는 공개가 아니었을 경우에만 사용

        //int enumCount = System.Enum.GetValues(typeof(Camera)).Length;
        ////Enum안의 데이터를 가져오기 때문에 Camera를 단순히 넣지 않고 옆에 typeof를 넣어 형태를 만든다.
        //int intEnum = (int)Cams.MainCam; //Enum은 숫자 데이터도 갖고 있기 때문에 옆에 (int)를 넣어 형변환 가능하다.
        //Cams enumData = (Cams)intEnum;

        //string stringEnum = Cams.MainCam.ToString();
        //enumData = (Cams)System.Enum.Parse(typeof(Cams), stringEnum);

        SwitchCamera(Cams.MainCam);
        InitBts();
    }

    private void InitBts()
    {
        //람다식 for문을 만났을 때 조건이 되는 변수가 계속 변하는게 그 변하는
        //데이터의 주소를 계속 전달하기 때문에 문제를 야기
        //람다식 -> 무명함수
        //델리게이트 -> 대리자 혹은 나중에 실행될 예약기능(단순히 예약이라고도 함)
        int count = listBtns.Count;

        for (int iNum = 0; iNum < count; iNum++)
        {
            int num = iNum;
            listBtns[iNum].onClick.AddListener(() => SwitchCamera(num));
            //=> 이런식으로 for문 안에 람다식이 있으면 람다식 안에 있는 조건 변수가 코드가 지나가도 지속적으로 변한다.
            //따라서 람다식안에 넣을 변수를 다시 만들어서 넣는 방법을 사용하는 것이 좋다.
        }
        //listBtns[0].onClick.AddListener(() => SwitchCamera(0));
        //listBtns[1].onClick.AddListener(() => SwitchCamera(1));
        //listBtns[2].onClick.AddListener(() => SwitchCamera(2));
        //listBtns[3].onClick.AddListener(() => SwitchCamera(3));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SwitchCamera(Cams.MainCam);
            SwitchCamera(0);
        }

        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            //SwitchCamera(Cams.SubCam01);
            SwitchCamera(1);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //SwitchCamera(Cams.SubCam02);
            SwitchCamera(2);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            //SwitchCamera(Cams.SubCam03);
            SwitchCamera(3);
        }

        if (forTest)
        {
            forTest = false;

            if (_action == null)
            {
                Debug.Log("저는 아무런 액션도 가지고 있지 않습니다.");
            }

            else
            {
                _action.Invoke();
            }
        }
    }

    /// <summary>
    /// 카메라 씬을 변경하기 위한 함수
    /// 기능 : 매개변수로 전달받은 카메라는 켜주고, 나머지 카메라는 꺼줍니다.
    /// </summary>
    /// <param name="_value">현재 사용할 캠</param>
    private void SwitchCamera(Cams _value)
    {
        int Count = listCam.Count;
        int findNum = (int)_value; //Cams를 int형으로 변환

        for (int iNum = 0; iNum < Count; iNum++)
        {
            Camera cam = listCam[iNum]; //각 캠을 저장

            //if (iNum == findNum) //iNum이 현재 찾고 싶은 캠의 번호이면
            //{
            //    cam.enabled = true; //캠 컴포넌트를 킨다.
            //}

            //else //아니면 끈다.
            //{
            //    cam.enabled = false;
            //}=> 요약본

            cam.enabled = iNum == findNum;
        }
    }

    /// <summary>
    /// 오버로드 함수
    /// </summary>
    /// <param name="_value"></param>
    private void SwitchCamera(int _value)
    {
        int Count = listCam.Count;

        for (int iNum = 0; iNum < Count; iNum++)
        {
            Camera cam = listCam[iNum]; //각 캠을 저장
            cam.enabled = iNum == _value;
        }
    }
}
