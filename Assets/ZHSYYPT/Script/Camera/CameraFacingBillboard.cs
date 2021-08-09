using UnityEngine;
using System.Collections;
/**********************************************
名    称: UI控制
功能描述：控制 UI始终面向摄像机

修订记录：

版本号    编辑时间      编辑人        修改描述
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
    /// 每帧更新
    /// 始终朝向摄像机
    /// </summary>
    void Update()
    {
        //if (Time.frameCount % 1 == 0)
        //{
        //cameraToLookAt = Camera.main;
        //Vector3 v = cameraToLookAt.transform.position - transform.position;
        //v.x = v.z = 0.0f;
        //transform.LookAt(cameraToLookAt.transform.position - v);//如果使用LookAt方向是相反的
        //}
       

        //当目标对象运动时，始终面向物体

        //transform.LookAt(target);

        //当目标对象运动时，始终转向物体

        //但是尽在Y轴上旋转，而不会上下旋转

        //不造成物体倾斜

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

