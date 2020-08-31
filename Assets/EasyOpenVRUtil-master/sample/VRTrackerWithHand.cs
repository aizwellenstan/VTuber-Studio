using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyLazyLibrary;

public class VRTrackerWithHand : MonoBehaviour {
    public GameObject Tracker;
    public string serialNumber;
    EasyOpenVRUtil eou;

    [Header("手の位置にめり込まないようにベースを移動させるかどうか。Hキーでオンオフ切り替え")]
    [SerializeField]
    private bool HandCoeff = true;

    [SerializeField]
    private Transform GuiterRightHand;

    [SerializeField] private Transform RightHand;
    
    public int index;
    EasyOpenVRUtil.Transform offset;
    // Use this for initialization
    void Start () {
        eou = new EasyOpenVRUtil();
    }

    // Update is called once per frame
    void Update () {
        eou.Init();
        eou.AutoExitOnQuit();

        var testSerial= eou.GetSerialNumber((uint)index);
        if (string.IsNullOrEmpty(serialNumber) == true)
        {
            Debug.Log(testSerial);
            serialNumber = testSerial;
        }

        var t = eou.GetTransformBySerialNumber(serialNumber);
        if (offset == null)
        {
            offset = t;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            HandCoeff = !HandCoeff;
        }
        eou.SetGameObjectLocalTransform(ref Tracker, t);
        //eou.SetGameObjectLocalTransformWithOffset(ref Tracker, t, offset);
//        Debug.Log(t.ToString());
    }
    
    
    void LateUpdate()
    {
        if (GuiterRightHand != null && RightHand != null && HandCoeff)
        {
            GuiterRightHand.position = RightHand.position;
        }
    }
    
}
