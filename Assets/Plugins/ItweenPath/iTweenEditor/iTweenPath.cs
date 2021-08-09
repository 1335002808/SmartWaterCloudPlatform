//by Bob Berkebile : Pixelplacement : http://www.pixelplacement.com
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/**********************************************
��    ��: �����ƶ�
���������������ƶ��ű�

�޶���¼��

�汾��    �༭ʱ��      �༭��        �޸�����
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
        //��ֵ�Զ�����ʽ����iTween���õ��Ĳ���
        Hashtable args = new Hashtable();

        //�������������ͣ�iTween�������ֺܶ��֣���Դ���е�ö��EaseType��
        //�����ƶ�����Ч���������ƶ����Ⱥ������ƶ����ȼ����ڱ��١��ȵ�
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

        //�ƶ����ٶȣ�
        //args.Add("speed", 3000f);
        //�ƶ�������ʱ�䡣�����speed������ô����speed
        args.Add("time", 5f);
        //����Ǵ�����ɫ�ġ����Կ�Դ����Ǹ�ö�١�
        args.Add("NamedValueColor", "_SpecColor");
        //�ӳ�ִ��ʱ��
        //args.Add("delay", 0f);

        //�Ƿ�����Ϸ����ʼ���泯·���н��ķ��򣬹���ĵط����Զ���ת��
        args.Add("orienttopath", false);
        //�ƶ��Ĺ������泯һ���㣬����orienttopath��Ϊtrueʱ�ò���ʧЧ
        args.Add("looktarget", Vector3.zero);
        //��Ϸ������looktarget����������
        args.Add("looktime",0.01);


        //��Ϸ�����ƶ���·������ΪVector3[]��Transform[] ���͡���ͨ��iTweenPath�༭��ȡ·��
        Vector3[] testPath = { new Vector3(-5054f, 3664f, 2637f), new Vector3(-1509f, 2315f, -3494f), new Vector3(3802f, 2123f, -3402f), new Vector3(6387f,1945f, 1483f), new Vector3(5065f, 1386f,5528f)/*, new Vector3(-4, 0, 2), new Vector3(-5, 0, -3) */};
        args.Add("path", testPath);
        //�Ƿ��ƶ���·������ʼλ�ã�false����Ϸ������������·������ʼ�㣬true����Ϸ���󽫴�ԭ��λ���ƶ���·������ʼ�㣩
        args.Add("movetopath", false);

        //��������path�������ҡ�orienttopath��Ϊtrueʱ����ֵ���ڼ��㡰looktarget����ֵ����ʾ��Ϸ���忴��ǰ�����λ�ã����ٷֱȣ�Ĭ��0.05��
        args.Add("lookahead", 0.01);

        //���ƽ���ָ����������ת
        //args.Add("axis", "y");
        //�Ƿ�ʹ�þֲ�����(Ĭ��Ϊfalse)
        args.Add("islocal", false);


        //����ѭ������ none loop pingPong (һ�� ѭ�� ����)	
        args.Add("loopType", "none");//һ��
        //args.Add("loopType", "loop");
        //args.Add("loopType", iTween.LoopType.pingPong);

        //�����ƶ������е��¼���
        //��ʼ�����ƶ�ʱ����AnimationStart������5.0��ʾ���Ĳ���
        args.Add("onstart", "AnimationStart");
        args.Add("onstartparams", 5f);
        //���ý��ܷ����Ķ���Ĭ����������ܣ�����Ҳ���Ըĳɱ�Ķ�����ܣ�
        //��ô�͵��ڽ��ն���Ľű���ʵ��AnimationStart������
        args.Add("onstarttarget", gameObject);


        //�ƶ�����ʱ���ã���������������
        args.Add("oncomplete", "AnimationEnd");
        args.Add("oncompleteparams", "end");
        args.Add("oncompletetarget", gameObject);

        //�ƶ��е��ã���������������
        args.Add("onupdate", "AnimationUpdate");
        args.Add("onupdatetarget", gameObject);
        args.Add("onupdateparams", true);

        // x y z ��ʾ�ƶ���λ�á�
        //args.Add("x", -5);
        //args.Add("y", 1);
        //args.Add("z", 1);
        //��ȻҲ����дVector3
        //args.Add("position",Vectoe3.zero);
        //�����øĶ���ʼ�ƶ�
        iTween.MoveTo(gameObject, args);
    }
}

