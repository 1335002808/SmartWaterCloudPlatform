using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**********************************************
名    称: 摄像机自主漫游
功能描述：场景自主移动的所有操作

修订记录：

版本号    编辑时间      编辑人        修改描述
-----------------------------------------------
1.0.0     2020-08-25   cxp         xxxx
1.0.1     2021-04-26   cxp         将原来的两个脚本合并为一个

***********************************************/
public class CamereMove : MonoBehaviour
{
    public GUIStyle skin;
    public float speed = 20f;

    public Transform moudle;
    private bool isPress;//是否按下
    private Vector3 startPos;//开始位置
    private Vector3 endPos;//结束位置
    public float dis;//距离

    private int scale;
    private bool firstMove;
    private bool startTime;
    private float currentTime;
    public GameObject VectorCamera;//摄像机的位置

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

    private Vector2 euler = new Vector2(0, 0);
    private Scene scene;
    public Transform waterCirculation;//水循环UI父物体

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Initialize();
    }

    /// <summary>
    /// 参数初始化
    /// </summary>
    private void Initialize()
    {
        InitialiseRotation();
        scale = 3;
        firstMove = true;
        startTime = false;
        currentTime = 0;
        Camera camer = VectorCamera.GetComponent<Camera>();
    }

    public void Update()
    {
        MoudleRotate();
        if (scene.name.Equals("Main"))
        {
            if (this.transform.position.z < -2573 && this.transform.position.x > -2398)
            {
                waterCirculation.GetChild(1).gameObject.SetActive(true);
                waterCirculation.GetChild(0).gameObject.SetActive(false);
            }
            else if (this.transform.position.z > -2573 && this.transform.position.x < -2398)
            {
                waterCirculation.GetChild(1).gameObject.SetActive(false);
                waterCirculation.GetChild(0).gameObject.SetActive(true);
            }
        }
        CamereMoves();
    }
    /// <summary>
    /// 摄像机初始角度更新
    /// </summary>
    public void InitialiseRotation()
    {
        Vector3 angles = transform.eulerAngles;
        euler.x = angles.y;
        euler.y = -angles.x;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    /// <summary>
    /// 飞行摄像机操作事件
    /// </summary>
	private void CamereMoves()
    {
        //print(this.transform.position);
        //Imposp_Camera();//限制摄像机
        float fSencitive = 1 + transform.position.y / 2000.0f;
        //按住左键上下左右移动
        float mDeltaX = Input.GetAxis("Mouse X");
        float mDeltaY = Input.GetAxis("Mouse Y");
        //鼠标左键控制地图的旋转
        if (Input.GetMouseButton(2))
        {
            Vector3 v = -transform.right * mDeltaX - transform.up * mDeltaY;
            //transform.position = Vector3.Lerp(transform.position, transform.position += v * fSencitive * speed, Time.deltaTime * 20);
            transform.position += v * fSencitive * speed;
        }
        //前后控制
        float delta = Input.GetAxis("Vertical") / 10;

        // delta += Input.GetAxis("Mouse ScrollWheel") / 50;//鼠标滚轮进行
        delta += Input.GetAxis("Mouse ScrollWheel") * 10f;//鼠标滚轮进行缩放
        //delta += Input.GetAxis("Mouse ScrollWheel")*10;//鼠标滚轮进行缩放
        transform.position += transform.forward * delta * speed * fSencitive * 6;
        //左右控制
        delta = Input.GetAxis("Horizontal") / 10.0f;
        ///transform.position = Vector3.Lerp(transform.position, transform.position += transform.right * delta * speed * fSencitive * 6, Time.deltaTime * 20);
        transform.position += transform.right * delta * speed * fSencitive * 6;

        if (Input.GetKeyDown("=") || Input.GetKeyDown("[+]"))
        {
            startTime = true;
            firstMove = true;
            if (scale <= 6)
            {
                scale++;
                speed = speed * 2;
            }
            else
                return;
        }
        if (Input.GetAxis("Vertical") != 0 && firstMove)
        {
            startTime = true;
        }
        if (Input.GetKeyDown("-") || Input.GetKeyDown("[-]"))
        {
            startTime = true;
            firstMove = true;
            if (scale >= 1)
            {
                scale--;
                speed = speed / 2;
            }
            else
                return;
        }
        if (startTime)
        {
            if (currentTime < 5f)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                startTime = false;
                firstMove = false;
            }
        }

        // 点击鼠标右键视角旋转的事件
        if (!Input.GetMouseButton(1))
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


      // 碰撞开始
    void OnCollisionEnter(Collision collision)
    {
        //print("碰撞开始");
       // transform.localEulerAngles = new Vector3(80.135f, -4.238f, -0.013f);
    }

  // 碰撞结束
    void OnCollisionExit(Collision collision)
    {
        //print("碰撞结束");
     }
 
  // 碰撞持续中
   void OnCollisionStay(Collision collision)
    {
        //print("碰撞中");
        if (collision.transform.tag.Equals("MapBox"))
        {
            transform.localEulerAngles = new Vector3(80.135f, -4.238f, -0.013f);
        }
    }

    /// <summary>
    /// 限制摄像机移动范围
    /// </summary>
    void Imposp_Camera()
    {
        //transform.localEulerAngles = new Vector3(80.135f, -4.238f, -0.013f);


        float speeds_y = transform.position.y;

        float speeds;

        //y是调整飞行摄像机距离地面的距离
        if (transform.position.y < 30)
            transform.position = new Vector3(transform.position.x, 30, transform.position.z);
        //if (transform.position.y > 960)
        //transform.position = new Vector3(transform.position.x, 960, transform.position.z);
        //使用 Mathf.Clamp(vlaue, min, max); 来限制模型的移动边界
        float tempY = 0f;
        float tempX = 0f;
        float tempZ = 0f;
        tempZ = Mathf.Clamp(transform.position.z, -12079, 11005);//Y轴旋转为0时
        tempX = Mathf.Clamp(transform.position.x, -13442, 12141);
        tempY = Mathf.Clamp(transform.position.y, 200, 17334);
        //tempZ = Mathf.Clamp(transform.position.z, -9720, 12450);//Y轴旋转为90时
        //tempX = Mathf.Clamp(transform.position.x, -2831, 3071);
        //tempY = Mathf.Clamp(transform.position.y, 200, 7334);
        //print("x坐标：" + tempX + "y的坐标：" + tempY + "z的坐标：" + tempZ);
        transform.position = new Vector3(tempX, tempY, tempZ);
    }

    void MoudleRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPress = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPress = false;
        }

        startPos = Input.mousePosition;
        if (isPress)
        {
            Vector2 offset = endPos - startPos;
            if (Mathf.Abs(offset.y) < Mathf.Abs(offset.x) && Mathf.Abs(offset.x) > dis)
            {
                moudle.Rotate(Vector2.up * Time.deltaTime * offset.x * 10);
            }
        }
        endPos = Input.mousePosition;
    }
}
