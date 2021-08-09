//-----------------------------------------------------------------------
// <copyright file="HeroBuildingSelectionUserInput.cs" company="Mapbox">
//     Copyright (c) 2018 Mapbox. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Mapbox.Examples
{
	using Mapbox.Unity;
	using UnityEngine;
	using UnityEngine.UI;
	using System;
	using Mapbox.Geocoding;
	using Mapbox.Utils;
	using Mapbox.Unity.Location;
	using Mapbox.Unity.Utilities;

	public class HeroBuildingSelectionUserInput : MonoBehaviour
	{
		
		[Geocode]
		public string location;

		[SerializeField]
		private Vector3 _cameraPosition;
		[SerializeField]
		private Vector3 _cameraRotation;

		private Camera _camera;

		Button _button;

		ForwardGeocodeResource _resource;

		bool _hasResponse;
		public bool HasResponse
		{
			get
			{
				return _hasResponse;
			}
		}

		public ForwardGeocodeResponse Response { get; private set; }

		public event Action<ForwardGeocodeResponse, bool> OnGeocoderResponse = delegate { };

		void Awake()
		{
			_button = GetComponent<Button>();//获取组件
			_button.onClick.AddListener(HandleUserInput);//添加点击事件
			_resource = new ForwardGeocodeResource("");
			_camera = Camera.main;//获取主摄像机
		}

		void TransformCamera()
		{
			_camera.transform.position = _cameraPosition;
			_camera.transform.localEulerAngles = _cameraRotation;	
		}

		void HandleUserInput()
		{
			_hasResponse = false;
			if (!string.IsNullOrEmpty(location))
			{
				_resource.Query = location;//加载跳转到新的点位后的地图
				MapboxAccess.Instance.Geocoder.Geocode(_resource, HandleGeocoderResponse);//加载新的地图后跳转摄像机方向
			}
		}

		void HandleGeocoderResponse(ForwardGeocodeResponse res)
		{
			_hasResponse = true;
			Response = res;
			TransformCamera();
			OnGeocoderResponse(res, false);
		}

		public void BakeCameraTransform()
		{
			_cameraPosition = _camera.transform.position;
			_cameraRotation = _camera.transform.localEulerAngles;
		}

	}
}
