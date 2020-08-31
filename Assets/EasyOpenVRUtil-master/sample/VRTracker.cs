using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyLazyLibrary;

public class VRTracker : MonoBehaviour {

	public GameObject Tracker;
	public string serialNumber;
	EasyOpenVRUtil eou;

	public int index;
	EasyOpenVRUtil.Transform offset;

	public Action<Vector3> OnUpdatePosition;
	// Use this for initialization
	void Start () {
		eou = new EasyOpenVRUtil();
	}

	// Update is called once per frame
	void Update () {
		//VirtualMotionCapture.Calibrator.Calibrate();
		eou.Init();
		eou.AutoExitOnQuit();

		var testSerial= eou.GetSerialNumber((uint)index);
		if (string.IsNullOrEmpty(serialNumber) == true)
		{
			Debug.Log(testSerial);
			serialNumber = testSerial;
		}
		
		var t = eou.GetTransformBySerialNumber(serialNumber);
		if (t == null)
		{
			return;
		}
		
		if (offset == null)
		{
			offset = t;
		}

		if (Tracker != null)
		{
			eou.SetGameObjectLocalTransform(ref Tracker, t);
		}

		if (t != null)
		{
			OnUpdatePosition?.Invoke(t.position);
		}

		//eou.SetGameObjectLocalTransformWithOffset(ref Tracker, t, offset);
//        Debug.Log(t.ToString());
	}

}
