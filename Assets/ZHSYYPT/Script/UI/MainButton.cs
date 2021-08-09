using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour
{
    public Transform player;//主摄像机

    protected static MainButton _instance;
    public static MainButton Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            _instance = FindObjectOfType<MainButton>();
            if (_instance != null)
                return _instance;
            GameObject obj = new GameObject("_Data");
            _instance = obj.AddComponent<MainButton>();
            return _instance;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 摄像机视角转移方法
    /// </summary>
    /// <param name="POS"></param>
    public void DW(GameObject POS)
    {
        player.transform.position = POS.transform.position;
        player.transform.rotation = POS.transform.rotation;
        player.transform.GetComponent<LookControl>().InitialiseRotation();
    }
}
