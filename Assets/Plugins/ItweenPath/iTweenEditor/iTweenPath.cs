//by Bob Berkebile : Pixelplacement : http://www.pixelplacement.com
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/**********************************************
名    称: 顶点移动
功能描述：定点移动脚本

修订记录：

版本号    编辑时间      编辑人        修改描述
-----------------------------------------------
1.0.0     2020-08-25   cxp         xxxx

***********************************************/
public class iTweenPath : MonoBehaviour
{
	public string pathName ="";
	public Color pathColor = Color.cyan;
	public List<Vector3> nodes = new List<Vector3>(){Vector3.zero, Vector3.zero};
	public int nodeCount;
	public static Dictionary<string, iTweenPath> paths = new Dictionary<string, iTweenPath>();
	public bool initialized = false;
	public string initialName = "";
	
	void OnEnable(){
		//paths.Add(pathName.ToLower(), this);
	}

    void OnDrawGizmosSelected()
    {
        if (enabled)
        { // dkoontz
            if (nodes.Count > 0)
            {
                iTween.DrawPath(nodes.ToArray(), pathColor);
            }
        } // dkoontz
    }

    public static Vector3[] GetPath(string requestedName)
    {
        requestedName = requestedName.ToLower();
        if (paths.ContainsKey(requestedName))
        {
            return paths[requestedName].nodes.ToArray();
        }
        else
        {
            Debug.Log("No path with that name exists! Are you sure you wrote it correctly?");
            return null;
        }
    }
    public void spead()
    {
        // iTween.MoveAdd(gameObject, iTween.Hash("time", 3, "easetype", iTween.EaseType.linear, "amount", new Vector3(2, 0, 3), "space", Space.Self/*"space",Space.World*/));
        //键值对儿的形式保存iTween所用到的参数
        Hashtable args = new Hashtable();

        //这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
        //例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
        //args.Add("easeType", iTween.EaseType.easeInOutExpo);
        //args.Add("easeType", iTween.EaseType.easeInQuad);
        // args.Add("easeType", iTween.EaseType.easeOutQuad);
        //args.Add("easeType", iTween.EaseType.easeInOutQuad);
        //args.Add("easeType", iTween.EaseType.easeInCubic);
        // args.Add("easeType", iTween.EaseType.easeOutCubic);
        // args.Add("easeType", iTween.EaseType.easeInOutCubic);
        // args.Add("easeType", iTween.EaseType.easeInQuart);
        // args.Add("easeType", iTween.EaseType.easeOutQuart);
        // args.Add("easeType", iTween.EaseType.easeInOutQuart);
        // args.Add("easeType", iTween.EaseType.easeInQuint);
        // args.Add("easeType", iTween.EaseType.easeOutQuint);
        // args.Add("easeType", iTween.EaseType.easeInOutQuint);
        // args.Add("easeType", iTween.EaseType.easeInSine);
        // args.Add("easeType", iTween.EaseType.easeOutSine);
        // args.Add("easeType", iTween.EaseType.easeInOutSine);
        // args.Add("easeType", iTween.EaseType.easeInExpo);
        // args.Add("easeType", iTween.EaseType.easeOutExpo);
        // args.Add("easeType", iTween.EaseType.easeInCirc);
        // args.Add("easeType", iTween.EaseType.easeOutCirc);
        // args.Add("easeType", iTween.EaseType.easeInOutCirc);
        args.Add("easeType", iTween.EaseType.linear);
        // args.Add("easeType", iTween.EaseType.spring);

        //移动的速度，
        //args.Add("speed", 3000f);
        //移动的整体时间。如果与speed共存那么优先speed
        args.Add("time", 5f);
        //这个是处理颜色的。可以看源码的那个枚举。
        args.Add("NamedValueColor", "_SpecColor");
        //延迟执行时间
        //args.Add("delay", 0f);

        //是否让游戏对象始终面朝路径行进的方向，拐弯的地方会自动旋转。
        args.Add("orienttopath", false);
        //移动的过程中面朝一个点，当“orienttopath”为true时该参数失效
        args.Add("looktarget", Vector3.zero);
        //游戏对象看向“looktarget”的秒数。
        args.Add("looktime",0.01);


        //游戏对象移动的路径可以为Vector3[]或Transform[] 类型。可通过iTweenPath编辑获取路径
        Vector3[] testPath = { new Vector3(-5054f, 3664f, 2637f), new Vector3(-1509f, 2315f, -3494f), new Vector3(3802f, 2123f, -3402f), new Vector3(6387f,1945f, 1483f), new Vector3(5065f, 1386f,5528f)/*, new Vector3(-4, 0, 2), new Vector3(-5, 0, -3) */};
        args.Add("path", testPath);
        //是否移动到路径的起始位置（false：游戏对象立即处于路径的起始点，true：游戏对象将从原是位置移动到路径的起始点）
        args.Add("movetopath", false);

        //当包含“path”参数且“orienttopath”为true时，该值用于计算“looktarget”的值，表示游戏物体看着前方点的位置，（百分比，默认0.05）
        args.Add("lookahead", 0.01);

        //限制仅在指定的轴上旋转
        //args.Add("axis", "y");
        //是否使用局部坐标(默认为false)
        args.Add("islocal", false);


        //三个循环类型 none loop pingPong (一般 循环 来回)	
        args.Add("loopType", "none");//一般
        //args.Add("loopType", "loop");
        //args.Add("loopType", iTween.LoopType.pingPong);

        //处理移动过程中的事件。
        //开始发生移动时调用AnimationStart方法，5.0表示它的参数
        args.Add("onstart", "AnimationStart");
        args.Add("onstartparams", 5f);
        //设置接受方法的对象，默认是自身接受，这里也可以改成别的对象接受，
        //那么就得在接收对象的脚本中实现AnimationStart方法。
        args.Add("onstarttarget", gameObject);


        //移动结束时调用，参数和上面类似
        args.Add("oncomplete", "AnimationEnd");
        args.Add("oncompleteparams", "end");
        args.Add("oncompletetarget", gameObject);

        //移动中调用，参数和上面类似
        args.Add("onupdate", "AnimationUpdate");
        args.Add("onupdatetarget", gameObject);
        args.Add("onupdateparams", true);

        // x y z 标示移动的位置。
        //args.Add("x", -5);
        //args.Add("y", 1);
        //args.Add("z", 1);
        //当然也可以写Vector3
        //args.Add("position",Vectoe3.zero);
        //最终让改对象开始移动
        iTween.MoveTo(gameObject, args);
    }
}

