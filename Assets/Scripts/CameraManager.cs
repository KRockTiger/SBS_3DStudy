using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Cams //�̸� ���ǵǾ�߸� �ϴ� ������
{
    MainCam,
    SubCam01,
    SubCam02,
    SubCam03,
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    //private List<Camera> listCam = new List<Camera>(); //����ȭ�� �ϰų� ���� -> �ν����� ����
    //private�� ���Ƴ��� ��� new�� �̿��Ͽ� �����Ҵ� �߰��ϱ�

    [SerializeField] private List<Camera> listCam; //ī�޶� ���� ����Ʈ
    [SerializeField] private List<Button> listBtns; //��ư�� ���� ����Ʈ

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
        //listCam.AddRange(arrCam); //ī�޶� �迭�Լ��� ����� ���� ������ ����Ʈ�� �״�� �߰��ϱ�
        //=> ����ȭ �Ǵ� ������ �ƴϾ��� ��쿡�� ���

        //int enumCount = System.Enum.GetValues(typeof(Camera)).Length;
        ////Enum���� �����͸� �������� ������ Camera�� �ܼ��� ���� �ʰ� ���� typeof�� �־� ���¸� �����.
        //int intEnum = (int)Cams.MainCam; //Enum�� ���� �����͵� ���� �ֱ� ������ ���� (int)�� �־� ����ȯ �����ϴ�.
        //Cams enumData = (Cams)intEnum;

        //string stringEnum = Cams.MainCam.ToString();
        //enumData = (Cams)System.Enum.Parse(typeof(Cams), stringEnum);

        SwitchCamera(Cams.MainCam);
        InitBts();
    }

    private void InitBts()
    {
        //���ٽ� for���� ������ �� ������ �Ǵ� ������ ��� ���ϴ°� �� ���ϴ�
        //�������� �ּҸ� ��� �����ϱ� ������ ������ �߱�
        //���ٽ� -> �����Լ�
        //��������Ʈ -> �븮�� Ȥ�� ���߿� ����� ������(�ܼ��� �����̶�� ��)
        int count = listBtns.Count;

        for (int iNum = 0; iNum < count; iNum++)
        {
            int num = iNum;
            listBtns[iNum].onClick.AddListener(() => SwitchCamera(num));
            //=> �̷������� for�� �ȿ� ���ٽ��� ������ ���ٽ� �ȿ� �ִ� ���� ������ �ڵ尡 �������� ���������� ���Ѵ�.
            //���� ���ٽľȿ� ���� ������ �ٽ� ���� �ִ� ����� ����ϴ� ���� ����.
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
                Debug.Log("���� �ƹ��� �׼ǵ� ������ ���� �ʽ��ϴ�.");
            }

            else
            {
                _action.Invoke();
            }
        }
    }

    /// <summary>
    /// ī�޶� ���� �����ϱ� ���� �Լ�
    /// ��� : �Ű������� ���޹��� ī�޶�� ���ְ�, ������ ī�޶�� ���ݴϴ�.
    /// </summary>
    /// <param name="_value">���� ����� ķ</param>
    private void SwitchCamera(Cams _value)
    {
        int Count = listCam.Count;
        int findNum = (int)_value; //Cams�� int������ ��ȯ

        for (int iNum = 0; iNum < Count; iNum++)
        {
            Camera cam = listCam[iNum]; //�� ķ�� ����

            //if (iNum == findNum) //iNum�� ���� ã�� ���� ķ�� ��ȣ�̸�
            //{
            //    cam.enabled = true; //ķ ������Ʈ�� Ų��.
            //}

            //else //�ƴϸ� ����.
            //{
            //    cam.enabled = false;
            //}=> ��ົ

            cam.enabled = iNum == findNum;
        }
    }

    /// <summary>
    /// �����ε� �Լ�
    /// </summary>
    /// <param name="_value"></param>
    private void SwitchCamera(int _value)
    {
        int Count = listCam.Count;

        for (int iNum = 0; iNum < Count; iNum++)
        {
            Camera cam = listCam[iNum]; //�� ķ�� ����
            cam.enabled = iNum == _value;
        }
    }
}
