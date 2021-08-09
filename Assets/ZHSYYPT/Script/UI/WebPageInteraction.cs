using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebPageInteraction : MonoBehaviour
{
    [SerializeField]
    [Tooltip("事件父物体")]
    GameObject incident;//事件父物体
    [SerializeField]
    [Tooltip("船父物体")]
    Transform ship;//事件父物体
    private bool onLine = true;//在线
    private bool offLine = true;//离线
    private bool malfunction = true;//故障
    private bool earlyWarning = true;//预警
    private bool showIncident = false;//事件父物体

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShipCondition(string data)
    {
        switch (data)
        {
            case "0":
                if (onLine.Equals(false))
                {
                    ship.GetChild(0).gameObject.SetActive(true);
                    onLine = true;
                }
                else
                {
                    ship.GetChild(0).gameObject.SetActive(false);
                    onLine = false;
                }
                break;
            case "1":
                if (offLine.Equals(false))
                {
                    ship.GetChild(1).gameObject.SetActive(true);
                    offLine = true;
                }
                else
                {
                    ship.GetChild(1).gameObject.SetActive(false);
                    offLine = false;
                }
                break;
            case "2":
                if (malfunction.Equals(false))
                {
                    ship.GetChild(2).gameObject.SetActive(true);
                    malfunction = true;
                } 
                else
                {
                    ship.GetChild(2).gameObject.SetActive(false);
                    malfunction = false;
                }
                break;
            case "3":
                if (earlyWarning.Equals(false))
                {
                    ship.GetChild(3).gameObject.SetActive(true);
                    earlyWarning = true;
                }
                else
                {
                    ship.GetChild(3).gameObject.SetActive(false);
                    earlyWarning = false;
                }
                break;
            case "incident":
                if (showIncident.Equals(false))
                {
                    incident.SetActive(true);
                    showIncident = true;
                }
                else
                {
                    incident.SetActive(false);
                    showIncident = false;
                }
                break;
        }
    }
}
