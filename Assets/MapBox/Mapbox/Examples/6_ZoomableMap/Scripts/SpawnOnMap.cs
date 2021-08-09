namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;
    using System.Collections;
    using LitJson;
    using UnityEngine.UI;
    using System.Text.RegularExpressions;

    public class SpawnOnMap : MonoBehaviour
    {
        private List<string> shiplongitudeAndLatitude = new List<string>();//船经纬度存储和船编号存储
        private List<string> shipNUM = new List<string>();//船编号存储 
        private List<int> incidentID = new List<int>();//事件ID存储
        private List<string> incidentMessage = new List<string>();//事件信息存储
        [SerializeField]
        AbstractMap _map;

        [SerializeField]
        [Geocode]
        string[] _shipLocationStrings;//无人船点位坐标数组存储
        Vector2d[] _locations;//无人船点位坐标转化存储
        string[] _incidentLocationStrings;//事件点位坐标数组存储

        List<Vector2d> _location = new List<Vector2d>();//无人船点位位置存储

        [SerializeField]
        float _spawnScale = 100f;

        [SerializeField][Tooltip("预制体")]
        GameObject _markerPrefab;//要加载的预制体
        [SerializeField][Tooltip("无人船父物体")]
        public Transform shipPreFab;//无人船父物体
        [SerializeField][Tooltip("事件父物体")]
        public Transform incidentPre;//事件父物体
        List<GameObject> _shipSpawnedObjects = new List<GameObject>();//无人船存储物体的容器
        List<GameObject> _incidentSpawnedObjects = new List<GameObject>();//事件船存储物体的容器

        void Start()
        {
            shiplongitudeAndLatitude.Clear();
            shipNUM.Clear();
            //StartCoroutine(LoadIPtxt());
            // InvokeRepeating("_SP", 2, 0.1F);

            //_locations = new Vector2d[_locationStrings.Length];
            //_spawnedObjects = new List<GameObject>();
            //for (int i = 0; i < _locationStrings.Length; i++)
            //{
            //    string type  = _locationStrings[i].Split('_')[1];
            //    var locationStrings = _locationStrings[i].Split('_')[0];
            //    string lt = locationStrings.Split(',')[0];
            //    string lg = locationStrings.Split(',')[1];
            //    double lts = double.Parse(lt) - 0.004f;//减向下、加向上
            //    double lgs = double.Parse(lg) - 0.0106f;//减向左、加向右
            //    var locationString = lts + "," + lgs;
            //    _locations[i] = Conversions.StringToLatLon(locationString);
            //    GameObject instance = Instantiate(_markerPrefab);

            //    instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            //    instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            //    if (type.Equals("0"))
            //    {
            //        instance.transform.parent = shipPreFab;
            //        instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/在线", typeof(Sprite)) as Sprite;
            //        _spawnedObjects.Add(instance);
            //    }
            //    else
            //    {
            //        instance.transform.parent = incident;
            //        instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/incident/非法行为/非法捕鱼", typeof(Sprite)) as Sprite;
            //        instance.transform.GetChild(0).GetComponent<TextMesh>().text = "非法捕鱼";
            //        _spawnedObjects.Add(instance);
            //    }
            //}
            //shipPreFab.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/离线", typeof(Sprite)) as Sprite;
            //shipPreFab.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/预警", typeof(Sprite)) as Sprite;
            //shipPreFab.GetChild(5).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/故障", typeof(Sprite)) as Sprite;
            //incident.GetChild(1).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/incident/水上识别/生活垃圾", typeof(Sprite)) as Sprite;
            //incident.GetChild(1).GetChild(0).GetComponent<TextMesh>().text = "生活垃圾";
            //incident.GetChild(3).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/incident/异常行为/人员落水", typeof(Sprite)) as Sprite;
            //incident.GetChild(3).GetChild(0).GetComponent<TextMesh>().text = "人员落水";
        }


        /// <summary>
        /// 无人船.json获取
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadIPtxt()
        {
            string url = Application.dataPath + "/StreamingAssets/无人船.json";
            WWW bundle = new WWW(url);
            yield return bundle;
            if (bundle.error != null)
            {
                print("报错" + bundle.error);
                yield break;
            }
            else
            {
                //JsonData jsons = JsonMapper.ToObject(bundle.text);
                LocationShip(bundle.text);
            }
        }

        /// <summary>
        /// 无人船.json测试事件
        /// </summary>
        public void ce2()
        {
            StartCoroutine(LoadIPtxt());
        }

        /// <summary>
        /// 无人船 - 副本.json测试事件
        /// </summary>
        public void ce()
        {
            StartCoroutine(Ceshi());
        }

        /// <summary>
        /// 无人船 - 副本.json获取
        /// </summary>
        /// <returns></returns>
        private IEnumerator Ceshi()
        {
            string url = Application.dataPath + "/StreamingAssets/无人船 - 副本.json";
            WWW bundle = new WWW(url);
            yield return bundle;
            if (bundle.error != null)
            {
                print("报错" + bundle.error);
                yield break;
            }
            else
            {
                //JsonData jsons = JsonMapper.ToObject(bundle.text);
                LocationShip(bundle.text);
            }
        }

        /// <summary>
        /// 事件.json测试事件
        /// </summary>
        public void shijian()
        {
            StartCoroutine(shijians());
        }

        /// <summary>
        /// 事件.json获取
        /// </summary>
        /// <returns></returns>
        private IEnumerator shijians()
        {
            string url = Application.dataPath + "/StreamingAssets/事件.json";
            WWW bundle = new WWW(url);
            yield return bundle;
            if (bundle.error != null)
            {
                print("报错" + bundle.error);
                yield break;
            }
            else
            {
                Debug.Log(bundle.text);
                Incident(bundle.text);
            }
        }


        /// <summary>
        /// 无人船位置接口
        /// </summary>
        public void LocationShip(string data)
        {
            JsonData jsons = JsonMapper.ToObject(data/*.ToString()*/);
            print(jsons.ToJson().ToString());
            for (int j = 0; j < jsons.Count; j++)
            {
                double longitude = double.Parse(jsons[j]["longitude"].ToJson().ToString());//经度
                double latitude = double.Parse(jsons[j]["latitude"].ToJson().ToString());//纬度
                string shipNum = jsons[j]["shipNum"].ToJson().ToString();//船的编号
                shipNum = shipNum.Replace('"', ' ');
                shipNum = shipNum.Replace(" ", "");
                int waterDistanceWarn = int.Parse(jsons[j]["waterDistanceWarn"].ToJson().ToString());//船的状态
                                                                                                     //waterDistanceWarn = waterDistanceWarn.Replace('"', ' ');
                                                                                                     //waterDistanceWarn = waterDistanceWarn.Replace(" ", "");
                double lts = latitude - 0.003952f;//减向下、加向上(纬度)
                double lgs = longitude - 0.01122f;//减向左、加向右（经度）
                string gt = lts + "," + lgs + "_" + shipNum + "_" + waterDistanceWarn;//校准之后的坐标
                print(gt);
                if (shipNUM.Contains(shipNum))//新传过来的点如果存在list中
                {
                    for (int i = 0; i < shiplongitudeAndLatitude.Count; i++)
                    {
                        if (shiplongitudeAndLatitude[i].Split('_')[1].ToString().Equals(shipNum))
                        {
                            shiplongitudeAndLatitude.Remove(shiplongitudeAndLatitude[i]);
                        }
                    }
                    shiplongitudeAndLatitude.Add(gt);//船的经纬度和编号
                    _shipLocationStrings = shiplongitudeAndLatitude.ToArray();
                }
                else
                {
                    shiplongitudeAndLatitude.Add(gt);//船的经纬度和编号
                    shipNUM.Add(shipNum);//存储船编号
                    _shipLocationStrings = shiplongitudeAndLatitude.ToArray();
                }
            }
            //清空列表
            for (int i = 0; i < shipPreFab.childCount; i++)
            {
                for (int j = 0; j < shipPreFab.GetChild(i).childCount; j++)
                {
                    if (shipPreFab.GetChild(i).childCount!=0)
                    {
                        Destroy(shipPreFab.GetChild(i).GetChild(j).gameObject);
                    }
                }
            }
            for (int i = 0; i < _shipLocationStrings.Length; i++)
            {
                string lglt = _shipLocationStrings[i].Split('_')[0];
                print(_shipLocationStrings.Length + "坐标的长度");
                _locations = new Vector2d[_shipLocationStrings.Length];
                var locationStrings = lglt;
                _locations[i] = Conversions.StringToLatLon(locationStrings);
                //print(_locations[i]);
                GameObject instance = Instantiate(_markerPrefab);
                instance.transform.position = _map.GeoToWorldPosition(_locations[i], true);

                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
               
                instance.transform.name = _shipLocationStrings[i].Split('_')[1];
                instance.transform.GetChild(2).GetChild(0).name = _shipLocationStrings[i].Split('_')[1];//点击物体的名字改为编号传给前端
                instance.transform.GetChild(0).GetComponent<TextMesh>().text = _shipLocationStrings[i].Split('_')[1];//将船的编号赋值给文本
                print(instance.transform.position + instance.transform.GetChild(2).GetChild(0).name);
                if (int.Parse(_shipLocationStrings[i].Split('_')[2]).Equals(0))
                {
                    print(_shipLocationStrings[i].Split('_')[2] + "船的状态");
                    instance.transform.parent = shipPreFab.GetChild(0);
                    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/在线", typeof(Sprite)) as Sprite;
                }
                else if (int.Parse(_shipLocationStrings[i].Split('_')[2]).Equals(1))
                {
                    print(_shipLocationStrings[i].Split('_')[2] + "船的状态");
                    instance.transform.parent = shipPreFab.GetChild(1);
                    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/离线", typeof(Sprite)) as Sprite;
                }
                else if (int.Parse(_shipLocationStrings[i].Split('_')[2]).Equals(2))
                {
                    print(_shipLocationStrings[i].Split('_')[2] + "船的状态");
                    instance.transform.parent = shipPreFab.GetChild(2);
                    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/故障", typeof(Sprite)) as Sprite;
                }
                else if (int.Parse(_shipLocationStrings[i].Split('_')[2]).Equals(3))
                {
                    print(_shipLocationStrings[i].Split('_')[2] + "船的状态");
                    instance.transform.parent = shipPreFab.GetChild(3);
                    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/预警", typeof(Sprite)) as Sprite;
                }
                for (int k = 0; k < _shipSpawnedObjects.Count; k++)
                {
                    //print(_spawnedObjects[k].transform.name);
                    if (_shipSpawnedObjects.Count < _shipLocationStrings.Length)
                    {
                        //print("物体数小于坐标数");
                    }
                    else
                    {
                        _shipSpawnedObjects.Remove(_shipSpawnedObjects[k]);
                    }
                }
                _shipSpawnedObjects.Add(instance);
                print(_shipSpawnedObjects.Count + "物体的个数");
            }

            #region 路径刷新点位
            //if (shipPreFab.childCount < _locationStrings.Length)
            //{
            //    for (int i = 0; i < shipPreFab.childCount; i++)
            //    {
            //        Destroy(shipPreFab.GetChild(i).gameObject);//如果新传过来的船的个数大于现有的就先清空
            //    }
            //    for (int i = 0; i < _locationStrings.Length; i++)
            //    {
            //        string lglt = _locationStrings[i].Split('_')[0];
            //        print(_locationStrings[i].Split('_')[1]);
            //        print(_locationStrings.Length + "坐标的长度");
            //        _locations = new Vector2d[_locationStrings.Length];
            //        var locationStrings = lglt;
            //        _locations[i] = Conversions.StringToLatLon(locationStrings);
            //        //print(_locations[i]);
            //        GameObject instance = Instantiate(_markerPrefab);
            //        instance.transform.position = _map.GeoToWorldPosition(_locations[i], true);

            //        instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            //        instance.transform.parent = shipPreFab;
            //        instance.transform.name = _locationStrings[i].Split('_')[1];
            //        instance.transform.GetChild(2).GetChild(0).name = _locationStrings[i].Split('_')[1];//点击物体的名字改为编号传给前端
            //        instance.transform.GetChild(0).GetComponent<TextMesh>().text = _locationStrings[i].Split('_')[1];//将船的编号赋值给文本
            //        print(instance.transform.position + instance.transform.GetChild(2).GetChild(0).name);
            //        if (int.Parse(_locationStrings[i].Split('_')[2]).Equals(0))
            //        {
            //            print(_locationStrings[i].Split('_')[2] + "船的状态");
            //            instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/在线", typeof(Sprite)) as Sprite;
            //        }
            //        else if (int.Parse(_locationStrings[i].Split('_')[2]).Equals(1))
            //        {
            //            print(_locationStrings[i].Split('_')[2] + "船的状态");
            //            instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/离线", typeof(Sprite)) as Sprite;
            //        }
            //        for (int k = 0; k < _spawnedObjects.Count; k++)
            //        {
            //            //print(_spawnedObjects[k].transform.name);
            //            if (_spawnedObjects.Count < _locationStrings.Length)
            //            {
            //                //print("物体数小于坐标数");
            //            }
            //            else
            //            {
            //                _spawnedObjects.Remove(_spawnedObjects[k]);
            //            }
            //        }
            //        _spawnedObjects.Add(instance);
            //        print(_spawnedObjects.Count + "物体的个数");
            //    }
            //}
            //else if (shipPreFab.childCount!=0)
            //{
            //    for (int i = 0; i < _locationStrings.Length; i++)
            //    {
            //        string lglt = _locationStrings[i].Split('_')[0];

            //        for (int j = 0; j < shipPreFab.childCount; j++)
            //        {
            //            print(shipPreFab.childCount);
            //            if (shipPreFab.GetChild(j).name.Equals(_locationStrings[i].Split('_')[1]))
            //            {
            //                print(_locationStrings[i].Split('_')[1]);
            //                print(_locationStrings.Length + "坐标的长度");
            //                _locations = new Vector2d[_locationStrings.Length];
            //                var locationStrings = lglt;
            //                _locations[i] = Conversions.StringToLatLon(locationStrings);
            //                ShipPath(shipPreFab.GetChild(j).gameObject,10f, _map.GeoToWorldPosition(_locations[i], true));
            //                shipPreFab.GetChild(j).localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            //                ////shipPreFab.GetChild(j).parent = shipPreFab;
            //                shipPreFab.GetChild(j).name = _locationStrings[i].Split('_')[1];
            //                shipPreFab.GetChild(j).GetChild(2).GetChild(0).name = _locationStrings[i].Split('_')[1];//点击物体的名字改为编号传给前端
            //                shipPreFab.GetChild(j).GetChild(0).GetComponent<TextMesh>().text = _locationStrings[i].Split('_')[1];//将船的编号赋值给文本
            //                print(shipPreFab.GetChild(j).position + shipPreFab.GetChild(j).GetChild(2).GetChild(0).name);
            //                if (int.Parse(_locationStrings[i].Split('_')[2]).Equals(0))
            //                {
            //                    print(_locationStrings[i].Split('_')[2] + "船的状态");
            //                    shipPreFab.GetChild(j).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/在线", typeof(Sprite)) as Sprite;
            //                }
            //                else if (int.Parse(_locationStrings[i].Split('_')[2]).Equals(1))
            //                {
            //                    print(_locationStrings[i].Split('_')[2] + "船的状态");
            //                    shipPreFab.GetChild(j).GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/离线", typeof(Sprite)) as Sprite;
            //                }
            //                for (int k = 0; k < _spawnedObjects.Count; k++)
            //                {
            //                    if (_spawnedObjects.Count < _locationStrings.Length)
            //                    {
            //                        //print("物体数小于坐标数");
            //                    }
            //                    else
            //                    {
            //                        _spawnedObjects.Remove(_spawnedObjects[k]);
            //                    }
            //                }
            //                _spawnedObjects.Add(shipPreFab.GetChild(j).gameObject);
            //                print(_spawnedObjects.Count + "物体的个数");
            //            }
            //            else
            //            {
            //                //print(_locationStrings[i].Split('_')[1]);
            //                //print(_locationStrings.Length + "坐标的长度");
            //                //_locations = new Vector2d[_locationStrings.Length];
            //                //var locationStrings = lglt;
            //                //_locations[i] = Conversions.StringToLatLon(locationStrings);
            //                ////print(_locations[i]);
            //                //GameObject instance = Instantiate(_markerPrefab);
            //                //instance.transform.position = _map.GeoToWorldPosition(_locations[i], true);

            //                //instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            //                //instance.transform.parent = shipPreFab;
            //                //instance.transform.name = _locationStrings[i].Split('_')[1];
            //                //instance.transform.GetChild(2).GetChild(0).name = _locationStrings[i].Split('_')[1];//点击物体的名字改为编号传给前端
            //                //instance.transform.GetChild(0).GetComponent<TextMesh>().text = _locationStrings[i].Split('_')[1];//将船的编号赋值给文本
            //                //print(instance.transform.position + instance.transform.GetChild(2).GetChild(0).name);
            //                //if (int.Parse(_locationStrings[i].Split('_')[2]).Equals(0))
            //                //{
            //                //    print(_locationStrings[i].Split('_')[2] + "船的状态");
            //                //    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/在线", typeof(Sprite)) as Sprite;
            //                //}
            //                //else if (int.Parse(_locationStrings[i].Split('_')[2]).Equals(1))
            //                //{
            //                //    print(_locationStrings[i].Split('_')[2] + "船的状态");
            //                //    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/ship/离线", typeof(Sprite)) as Sprite;
            //                //}
            //                //for (int k = 0; k < _spawnedObjects.Count; k++)
            //                //{
            //                //    //print(_spawnedObjects[k].transform.name);
            //                //    if (_spawnedObjects.Count < _locationStrings.Length)
            //                //    {
            //                //        //print("物体数小于坐标数");
            //                //    }
            //                //    else
            //                //    {
            //                //        _spawnedObjects.Remove(_spawnedObjects[k]);
            //                //    }
            //                //}
            //                //_spawnedObjects.Add(instance);
            //                //print(_spawnedObjects.Count + "物体的个数");
            //            }
            //        }
            //    }
            //}
            #endregion
        }


        private void _SP()
        {
            int count = _shipSpawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                var spawnedObject = _shipSpawnedObjects[i];
                var location = _locations[i];
                spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            }
        }

        private void Update()
        {
            //船
            int count = _shipSpawnedObjects.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < shiplongitudeAndLatitude.Count; j++)
                {
                    string code = shiplongitudeAndLatitude[j].Split('_')[1];
                    string loc = shiplongitudeAndLatitude[j].Split('_')[0];
                    if (_shipSpawnedObjects[i].transform.name.Equals(code))
                    {
                        var spawnedObject = _shipSpawnedObjects[i];
                        _locations[i] = Conversions.StringToLatLon(loc);
                        var location = _locations[i];
                        spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                        spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                    }
                }
            }

            //事件
            int incidentCount = _incidentSpawnedObjects.Count;
            for (int i = 0; i < incidentCount; i++)
            {
                for (int j = 0; j < incidentMessage.Count; j++)
                {
                    string code = incidentMessage[j].Split('_')[1];
                    string loc = incidentMessage[j].Split('_')[0];
                    if (_incidentSpawnedObjects[i].transform.name.Equals(code))
                    {
                        var spawnedObject = _incidentSpawnedObjects[i];
                        _locations[i] = Conversions.StringToLatLon(loc);
                        var location = _locations[i];
                        spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
                        spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                    }
                }
            }
        }

        /// <summary>
        /// 无人船视角跳转
        /// </summary>
        /// <param name="data">船的编号</param>
        public void SkipShip(string data)
        {
            if (shiplongitudeAndLatitude.Count>0)
            {
                for (int i = 0; i < shiplongitudeAndLatitude.Count; i++)
                {
                    string shipNum = shiplongitudeAndLatitude[i].Split('_')[1];
                    if (shipNum.Equals(data))
                    {
                        string lglt = shiplongitudeAndLatitude[i].Split('_')[0];
                        double lg = double.Parse(lglt.Split(',')[0]);
                        print(lg + "lg");
                        double lt = double.Parse(lglt.Split(',')[1]);
                         _map.Initialize(new Vector2d(lg, lt), 16.5f);//位置定位方法接口
                    }
                }
            }
        }

        /// <summary>
        /// 事件数据信息接口
        /// </summary>
        /// <param name="str">事件数据信息</param>
        public void Incident(string str)
        {
            JsonData jsons = JsonMapper.ToObject(str);
            JsonData data = JsonMapper.ToObject(jsons["data"].ToJson().ToString());
            for (int i = 0; i < data.Count; i++)
            {
                Debug.Log(data[i]["longitude"].ToJson());
                string longitudes = data[i]["longitude"].ToJson().ToString();
                longitudes = longitudes.Replace('"', ' ');
                double longitude = double.Parse(longitudes);//经度
                string latitudes = data[i]["latitude"].ToJson().ToString();
                latitudes = latitudes.Replace('"', ' ');
                double latitude = double.Parse(latitudes);//纬度
                int shincidentId = int.Parse(data[i]["id"].ToJson().ToString());//ID
                JsonData detail = JsonMapper.ToObject(data[i]["detail"].ToJson().ToString());
                string desc = detail["desc"].ToJson().ToString();//时间Name
                desc = desc.Replace('"', ' ');
                desc = desc.Replace(" ", "");
                desc = Regex.Unescape(desc);//Unicode转汉字
                double lts = latitude - 0.003952f;//减向下、加向上(经度)
                double lgs = longitude - 0.01122f;//减向左、加向右（纬度）
                string gt = lts + "," + lgs + "_" + shincidentId + "_" + desc;//校准之后的坐标
                print(gt);
                if (incidentID.Contains(shincidentId))//新传过来的点如果存在list中
                {
                    for (int j = 0; j < incidentMessage.Count; j++)
                    {
                        if (incidentMessage[j].Split('_')[1].ToString().Equals(shincidentId))
                        {
                            incidentMessage.Remove(incidentMessage[j]);
                        }
                    }
                    incidentMessage.Add(gt);//存储事件信息
                    _incidentLocationStrings = incidentMessage.ToArray();
                }
                else
                {
                    incidentMessage.Add(gt);//存储事件信息
                    incidentID.Add(shincidentId);
                    _incidentLocationStrings = incidentMessage.ToArray();
                }
            }
            for (int i = 0; i < incidentPre.childCount; i++)
            {
                for (int j = 0; j < incidentPre.GetChild(i).childCount; j++)
                {
                    if (incidentPre.GetChild(i).childCount!=0)
                    {
                        Destroy(incidentPre.GetChild(i).GetChild(j).gameObject);
                    }
                }
            }
            for (int i = 0; i < _incidentLocationStrings.Length; i++)
            {
                string lglt = _incidentLocationStrings[i].Split('_')[0];
               // print(_incidentLocationStrings.Length + "坐标的长度");
                _locations = new Vector2d[_incidentLocationStrings.Length];
                var locationStrings = lglt;
                _locations[i] = Conversions.StringToLatLon(locationStrings);
                GameObject instance = Instantiate(_markerPrefab);
                instance.transform.position = _map.GeoToWorldPosition(_locations[i], true);
                instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
                instance.transform.name = _incidentLocationStrings[i].Split('_')[1];
                instance.transform.tag = _incidentLocationStrings[i].Split('_')[2];
                instance.transform.GetChild(2).GetChild(0).name = _incidentLocationStrings[i].Split('_')[1];//点击物体的名字改为编号传给前端用
                instance.transform.GetChild(0).GetComponent<TextMesh>().text = _incidentLocationStrings[i].Split('_')[2];//将事件名赋值给文本
                IncidentType(_incidentLocationStrings[i].Split('_')[2], instance.transform);
                for (int k = 0; k < _incidentSpawnedObjects.Count; k++)
                {
                    if (_incidentSpawnedObjects.Count < _incidentLocationStrings.Length)
                    {
                        //print("物体数小于坐标数");
                    }
                    else
                    {
                        _incidentSpawnedObjects.Remove(_incidentSpawnedObjects[k]);
                    }
                }
                _incidentSpawnedObjects.Add(instance);
                print(_incidentSpawnedObjects.Count + "物体的个数");
            }

        }

        /// <summary>
        /// 事件类型
        /// </summary>
        /// <param name="data">传过来的事件类型</param>
        private void IncidentType(string data,Transform instance)
        {
            for (int i = 0; i < incidentPre.childCount; i++)
            {
                if (data.Equals(incidentPre.GetChild(i).tag))
                {
                    instance.transform.parent = incidentPre.GetChild(i);
                    instance.transform.GetChild(2).GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load("Image/incident/" + instance.tag, typeof(Sprite)) as Sprite;
                }
            }
        }

        /// <summary>
        /// 船移动事件
        /// </summary>
        /// <param name="go">要移动的船</param>
        /// <param name="speed">船的移动速度</param>
        /// <param name="vec">移动的目标点</param>
        public void ShipPath(GameObject go, float speed, Vector3 vec)
        {
            print("开场动画");
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
            //args.Add("speed", speed);
            //移动的整体时间。如果与speed共存那么优先speed
             args.Add("time", speed);
            //这个是处理颜色的。可以看源码的那个枚举。
            args.Add("NamedValueColor", "_SpecColor");
            //延迟执行时间
            //args.Add("delay", 0f);

            //是否让游戏对象始终面朝路径行进的方向，拐弯的地方会自动旋转。
            //args.Add("orienttopath", true);
            //移动的过程中面朝一个点，当“orienttopath”为true时该参数失效
            //args.Add("looktarget", Vector3.zero);

            args.Add("looktarget", new Vector3(0f, -200f, 0f));
            //游戏对象看向“looktarget”的秒数。
            args.Add("looktime", 0f);

            //游戏对象移动的路径可以为Vector3[]或Transform[] 类型。可通过iTweenPath编辑获取路径
            Vector3[] testPath = { go.transform.position, vec };
            //new Vector3(-3725f, 2086f, 4039f
            args.Add("path", testPath);
            //是否移动到路径的起始位置（false：游戏对象立即处于路径的起始点，true：游戏对象将从原是位置移动到路径的起始点）
            args.Add("movetopath", false);

            //当包含“path”参数且“orienttopath”为true时，该值用于计算“looktarget”的值，表示游戏物体看着前方点的位置，（百分比，默认0.05）
            args.Add("lookahead", 0.05f);

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
            args.Add("onstarttarget", go);


            //移动结束时调用，参数和上面类似
            args.Add("oncomplete", "AnimationEnd");
            args.Add("oncompleteparams", "end");
            args.Add("oncompletetarget", go);

            //移动中调用，参数和上面类似
            args.Add("onupdate", "AnimationUpdate");
            args.Add("onupdatetarget", go);
            args.Add("onupdateparams", true);

            // x y z 标示移动的位置。
            //args.Add("x", -5);
            //args.Add("y", 1);
            //args.Add("z", 1);
            //当然也可以写Vector3
            //args.Add("position",Vectoe3.zero);
            //最终让改对象开始移动
            iTween.MoveTo(go, args);
        }

    }
}
















//for (int j = 0; j < longitudeAndLatitude.Count; j++)
//{
//    for (int i = 0; i < shipPreFab.childCount; i++)
//    {
//        if (shipNUM.Contains(shipPreFab.GetChild(i).name) /*&&shipNum.Equals(shipPreFab.GetChild(i).name)*/)
//        {
//            if (shipNum.Equals(shipPreFab.GetChild(i).name))
//            {
//                print("重复克隆");
//                _locationStrings = longitudeAndLatitude[j].Split('_');
//                _locations = new Vector2d[_locationStrings.Length - 1];
//                var locationStrings = _locationStrings[0];
//                _spawnedObjects = new List<GameObject>();
//                _locations[j] = Conversions.StringToLatLon(locationStrings);
//                print(_locations[j]);
//                ShipPath(shipPreFab.GetChild(i).gameObject, 1000, _map.GeoToWorldPosition(_locations[j], true));//调用移动的方法
//                shipPreFab.GetChild(i).transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
//                _spawnedObjects.Add(shipPreFab.GetChild(i).gameObject);
//                print(_spawnedObjects.Count);
//            }
//        }
//        else
//        {
//            _locationStrings = longitudeAndLatitude[j].Split('_');
//            _locations = new Vector2d[_locationStrings.Length - 1];
//            var locationStrings = _locationStrings[j];
//            _spawnedObjects = new List<GameObject>();
//            _locations[j] = Conversions.StringToLatLon(locationStrings);
//            print(_locations[j]);
//            GameObject instance = Instantiate(_markerPrefab);
//            instance.transform.position = _map.GeoToWorldPosition(_locations[j], true);
//            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
//            instance.transform.parent = shipPreFab;
//            instance.transform.name = _locationStrings[1];
//            _spawnedObjects.Add(instance);
//            print(shipPreFab.childCount + "结束循环");
//            break;
//        }
//    }
//}