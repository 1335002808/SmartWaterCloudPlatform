using UnityEngine;
using System.Collections;
/**********************************************
��    ��: UI����
�������������� UIʼ�����������

�޶���¼��

�汾��    �༭ʱ��      �༭��        �޸�����
-----------------------------------------------
1.0.0     2016-09-10    zww          xxxx
2.0.0     2020-09-08    cxp          xxxx

***********************************************/
public class CameraFacingBillboard : MonoBehaviour
{
    private Transform cameraLookt;
    private Camera cameraToLookAt;
    public static bool planeShip = false;
    //
    void Start()
    {
        //cameraLookt = GameObject.Find("Lookt").transform;
        cameraToLookAt = Camera.main;
        InvokeRepeating("Qu", 0f, 0.1f);
    }
    /// <summary>
    /// ÿ֡����
    /// ʼ�ճ��������
    /// </summary>
    void Update()
    {
        //if (Time.frameCount % 1 == 0)
        //{
        //cameraToLookAt = Camera.main;
        //Vector3 v = cameraToLookAt.transform.position - transform.position;
        //v.x = v.z = 0.0f;
        //transform.LookAt(cameraToLookAt.transform.position - v);//���ʹ��LookAt�������෴��
        //}
       

        //��Ŀ������˶�ʱ��ʼ����������

        //transform.LookAt(target);

        //��Ŀ������˶�ʱ��ʼ��ת������

        //���Ǿ���Y������ת��������������ת

        //�����������б

        //cameraLookt.rotation= Quaternion.LookRotation(this.transform.position - cameraLookt.transform.position);
    }
    private void Qu()
    {
        if (planeShip.Equals(false))
        {
            transform.rotation = Quaternion.LookRotation(transform.position - cameraToLookAt.transform.position);
        }
       
    }
}

