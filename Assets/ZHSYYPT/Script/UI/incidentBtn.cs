using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class incidentBtn : MonoBehaviour
{
    private bool shengHuoLaJi = false;//生活垃圾
    private bool shuiCaoHaiZao = false;//水草海藻
    private bool shuiMianYouWu = false;//水面油污
    private bool diBaAnQuan = false;//堤坝安全
    private bool weiGuiSheShi = false;//违规设施
    private bool anBianLaJi = false;//岸边垃圾
    private bool zuShuiShengWu = false;//阻水生物
    private bool yeJianShiBie= false;//夜间识别
    private bool feiFaBuYu = false;//非法捕鱼
    private bool feiFaFangMu = false;//非法放牧
    private bool feiFaFangSheng = false;//非法放生
    private bool feiFaKaiCai = false;//非法开采
    private bool feiFaChuanZhi = false;//非法船只
    private bool weiGuiHuoDong = false;//违规活动
    private bool renWeiQingDao = false;//人为倾倒
    private bool heDaoQinZhan = false;//河道侵占
    private bool feiFaXiTiao = false;//非法洗涤
    private bool youYongXiZao = false; //游泳洗澡
    private bool renYuanLuoShui= false;//人员落水
    private bool qingShengZiSha = false;//轻生自杀
    private bool anShangChiXie = false;//岸上持械
    private bool anGuanTanCe = false;//暗管探测
    private bool shuiXiaYiWu = false;//水下异物
    [SerializeField]
    [Tooltip("事件父物体")]
    Transform  IncidentBtn;

    void Start()
    {
      //shengHuoLaJi = true;//生活垃圾
      //shuiCaoHaiZao = true;//水草海藻
      //shuiMianYouWu = true;//水面油污
      //diBaAnQuan = true;//堤坝安全
      //weiGuiSheShi = true;//违规设施
      //anBianLaJi = true;//岸边垃圾
      //zuShuiShengWu = true;//阻水生物
      //yeJianShiBie = true;//夜间识别
      //feiFaBuYu = true;//非法捕鱼
      //feiFaFangMu = true;//非法放牧
      //feiFaFangSheng = true;//非法放生
      //feiFaKaiCai = true;//非法开采
      //feiFaChuanZhi = true;//非法船只
      //weiGuiHuoDong = true;//违规活动
      //renWeiQingDao = true;//人为倾倒
      //heDaoQinZhan = true;//河道侵占
      //feiFaXiTiao = true;//非法洗涤
      //youYongXiZao = true; //游泳洗澡
      //renYuanLuoShui = true;//人员落水
      //qingShengZiSha = true;//轻生自杀
      //anShangChiXie = true;//岸上持械
      //anGuanTanCe = true;//暗管探测
      //shuiXiaYiWu = true;//水下异物
    IncidentBtn = GameObject.Find("Incident").transform;
    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 事件点位UI点击事件
    /// </summary>
    /// <param name="data"></param>
    public void IncidentOnClick(string data)
    {
        switch (data)
        {
            case "生活垃圾":
                if (shengHuoLaJi.Equals(false))
                {
                    IncidentBtn.GetChild(0).gameObject.SetActive(true);
                    shengHuoLaJi = true;
                }
                else
                {
                    IncidentBtn.GetChild(0).gameObject.SetActive(false);
                    shengHuoLaJi = false;
                }
                break;
            case "水草海藻":
                if (shuiCaoHaiZao.Equals(false))
                {
                    IncidentBtn.GetChild(1).gameObject.SetActive(true);
                    shuiCaoHaiZao = true;
                }
                else
                {
                    IncidentBtn.GetChild(1).gameObject.SetActive(false);
                    shuiCaoHaiZao = false;
                }
                break;
            case "水面油污":
                if (shuiMianYouWu.Equals(false))
                {
                    IncidentBtn.GetChild(2).gameObject.SetActive(true);
                    shuiMianYouWu = true;
                }
                else
                {
                    IncidentBtn.GetChild(2).gameObject.SetActive(false);
                    shuiMianYouWu = false;
                }
                break;
            case "堤坝安全":
                if (diBaAnQuan.Equals(false))
                {
                    IncidentBtn.GetChild(3).gameObject.SetActive(true);
                    diBaAnQuan = true;
                }
                else
                {
                    IncidentBtn.GetChild(3).gameObject.SetActive(false);
                    diBaAnQuan = false;
                }
                break;
            case "违规设施":
                if (weiGuiSheShi.Equals(false))
                {
                    IncidentBtn.GetChild(4).gameObject.SetActive(true);
                    weiGuiSheShi = true;
                }
                else
                {
                    IncidentBtn.GetChild(4).gameObject.SetActive(false);
                    weiGuiSheShi = false;
                }
                break;
            case "岸边垃圾":
                if (anBianLaJi.Equals(false))
                {
                    IncidentBtn.GetChild(5).gameObject.SetActive(true);
                    anBianLaJi = true;
                }
                else
                {
                    IncidentBtn.GetChild(5).gameObject.SetActive(false);
                    anBianLaJi = false;
                }
                break;
            case "阻水生物":
                if (zuShuiShengWu.Equals(false))
                {
                    IncidentBtn.GetChild(6).gameObject.SetActive(true);
                    zuShuiShengWu = true;
                }
                else
                {
                    IncidentBtn.GetChild(6).gameObject.SetActive(false);
                    zuShuiShengWu = false;
                }
                break;
            case "夜间识别":
                if (yeJianShiBie.Equals(false))
                {
                    IncidentBtn.GetChild(7).gameObject.SetActive(true);
                    yeJianShiBie = true;
                }
                else
                {
                    IncidentBtn.GetChild(7).gameObject.SetActive(false);
                    yeJianShiBie = false;
                }
                break;
            case "非法捕鱼":
                if (feiFaBuYu .Equals(false))
                {
                    IncidentBtn.GetChild(8).gameObject.SetActive(true);
                    feiFaBuYu = true;
                }
                else
                {
                    IncidentBtn.GetChild(8).gameObject.SetActive(false);
                    feiFaBuYu = false;
                }
                break;
            case "非法放牧":
                if (feiFaFangMu.Equals(false))
                {
                    IncidentBtn.GetChild(9).gameObject.SetActive(true);
                    feiFaFangMu = true;
                }
                else
                {
                    IncidentBtn.GetChild(9).gameObject.SetActive(false);
                    feiFaFangMu = false;
                }
                break;
            case "非法放生":
                if (feiFaFangSheng.Equals(false))
                {
                    IncidentBtn.GetChild(10).gameObject.SetActive(true);
                    feiFaFangSheng = true;
                }
                else
                {
                    IncidentBtn.GetChild(10).gameObject.SetActive(false);
                    feiFaFangSheng = false;
                }
                break;
            case "非法开采":
                if (feiFaKaiCai.Equals(false))
                {
                    IncidentBtn.GetChild(11).gameObject.SetActive(true);
                    feiFaKaiCai = true;
                }
                else
                {
                    IncidentBtn.GetChild(11).gameObject.SetActive(false);
                    feiFaKaiCai = false;
                }
                break;
            case "非法船只":
                if (feiFaChuanZhi.Equals(false))
                {
                    IncidentBtn.GetChild(12).gameObject.SetActive(true);
                    feiFaChuanZhi = true;
                }
                else
                {
                    IncidentBtn.GetChild(12).gameObject.SetActive(false);
                    feiFaChuanZhi = false;
                }
                break;
            case "违规活动":
                if (weiGuiHuoDong.Equals(false))
                {
                    IncidentBtn.GetChild(13).gameObject.SetActive(true);
                    weiGuiHuoDong = true;
                }
                else
                {
                    IncidentBtn.GetChild(13).gameObject.SetActive(false);
                    weiGuiHuoDong = false;
                }
                break;
            case "人为倾倒":
                if (renWeiQingDao.Equals(false))
                {
                    IncidentBtn.GetChild(14).gameObject.SetActive(true);
                    renWeiQingDao = true;
                }
                else
                {
                    IncidentBtn.GetChild(14).gameObject.SetActive(false);
                    renWeiQingDao = false;
                }
                break;
            case "河道侵占":
                if (heDaoQinZhan.Equals(false))
                {
                    IncidentBtn.GetChild(15).gameObject.SetActive(true);
                    heDaoQinZhan = true;
                }
                else
                {
                    IncidentBtn.GetChild(15).gameObject.SetActive(false);
                    heDaoQinZhan = false;
                }
                break;
            case "非法洗涤":
                if (feiFaXiTiao.Equals(false))
                {
                    IncidentBtn.GetChild(16).gameObject.SetActive(true);
                    feiFaXiTiao = true;
                }
                else
                {
                    IncidentBtn.GetChild(16).gameObject.SetActive(false);
                    feiFaXiTiao = false;
                }
                break;
            case "游泳洗澡":
                if (youYongXiZao.Equals(false))
                {
                    IncidentBtn.GetChild(17).gameObject.SetActive(true);
                    youYongXiZao = true;
                }
                else
                {
                    IncidentBtn.GetChild(17).gameObject.SetActive(false);
                    youYongXiZao = false;
                }
                break;
            case "人员落水":
                if (renYuanLuoShui.Equals(false))
                {
                    IncidentBtn.GetChild(18).gameObject.SetActive(true);
                    renYuanLuoShui = true;
                }
                else
                {
                    IncidentBtn.GetChild(18).gameObject.SetActive(false);
                    renYuanLuoShui = false;
                }
                break;
            case "轻生自杀":
                if (qingShengZiSha.Equals(false))
                {
                    IncidentBtn.GetChild(19).gameObject.SetActive(true);
                    qingShengZiSha = true;
                }
                else
                {
                    IncidentBtn.GetChild(19).gameObject.SetActive(false);
                    qingShengZiSha = false;
                }
                break;
            case "岸上持械":
                if (anShangChiXie.Equals(false))
                {
                    IncidentBtn.GetChild(20).gameObject.SetActive(true);
                    anShangChiXie = true;
                }
                else
                {
                    IncidentBtn.GetChild(20).gameObject.SetActive(false);
                    anShangChiXie = false;
                }
                break;
            case "暗管探测":
                if (anGuanTanCe.Equals(false))
                {
                    IncidentBtn.GetChild(21).gameObject.SetActive(true);
                    anGuanTanCe = true;
                }
                else
                {
                    IncidentBtn.GetChild(21).gameObject.SetActive(false);
                    anGuanTanCe = false;
                }
                break;
            case "水下异物":
                if (shuiXiaYiWu.Equals(false))
                {
                    IncidentBtn.GetChild(22).gameObject.SetActive(true);
                    shuiXiaYiWu = true;
                }
                else
                {
                    IncidentBtn.GetChild(22).gameObject.SetActive(false);
                    shuiXiaYiWu = false;
                }
                break;
        }
    }

}
