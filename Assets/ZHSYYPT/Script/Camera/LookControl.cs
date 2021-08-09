using UnityEngine;
using System;
using System.Collections;
/**********************************************
名    称: 角度控制
功能描述：摄像机旋转角度控制

修订记录：

版本号    编辑时间      编辑人        修改描述
-----------------------------------------------
1.0.0     2016-09-10    zww          xxxx

***********************************************/
[AddComponentMenu("Camera Control/LookControl")]
public class LookControl : MonoBehaviour
{
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;

    //按着鼠标滚轮或者左键的移动速度
    public float sensitivityX = 15F;
	public float sensitivityY = 15F;

    
	public float minimumX = -180F;
	public float maximumX = 180F;

    //限制y轴的视角范围
	public float minimumY = -60F;
	public float maximumY = 60F;

	private Vector2 euler= new Vector2(0,0);

	void Start (){
		InitialiseRotation();
	}
	public void Update (){
		if(!Input.GetMouseButton(1))
			return;
		if (axes == RotationAxes.MouseXAndY)
        {

            euler.x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            euler.y += Input.GetAxis("Mouse Y") * sensitivityY;
            euler.y = Mathf.Clamp(euler.y, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(-euler.y, euler.x, transform.localEulerAngles.z);
          }
          else if (axes == RotationAxes.MouseX)
        {
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
        else
        {
            euler.y += Input.GetAxis("Mouse Y") * sensitivityY;
            euler.y = Mathf.Clamp(euler.y, minimumY, maximumY);
			transform.localEulerAngles = new Vector3(-euler.y, transform.localEulerAngles.y, 0);
		}
	}
	//摄像机初始角度更新
	public void InitialiseRotation()
    {
		Vector3 angles = transform.eulerAngles;
	    euler.x = angles.y;
	    euler.y = -angles.x;
	}
	 float ClampAngle(float angle, float min, float max){
		if(angle < -360f) angle += 360f;
		if(angle > 360f)	angle -= 360f;
		return Mathf.Clamp(angle, min, max);
	}
}