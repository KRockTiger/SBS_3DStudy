using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    [SerializeField] int testNumber;

    private void OnDestroy() //������ �� �ҷ����� �Լ�
    {
        CameraManager.Instance.RemoveAction(() =>
        {
            Debug.Log($"���� �׽�Ʈ�׼��̰� ���� ���� {testNumber}�� ������ �ֽ��ϴ�.");
        });
    }

    private void Start()
    {
        CameraManager.Instance.Action = () => 
        {
            Debug.Log($"���� �׽�Ʈ�׼��̰� ���� ���� {testNumber}�� ������ �ֽ��ϴ�.");
        };
    }
}
