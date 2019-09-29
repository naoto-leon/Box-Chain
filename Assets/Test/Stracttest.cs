using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stracttest : MonoBehaviour
{


    public struct BoxMoveDevelop
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }
       
    }

    public GameObject[] chposition;


    // Start is called before the first frame update
    void Start()
    {


        chposition = new GameObject[transform.childCount];
        chposition[0] = transform.GetChild(0).gameObject;
        chposition[1] = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        BoxMoveDevelop BMD = new BoxMoveDevelop();

        BMD.StartPosition = chposition[0].transform.position;
        BMD.EndPosition = chposition[1].transform.position;
        BMD.Direction = (BMD.StartPosition - BMD.EndPosition).normalized;

        Debug.Log(BMD.StartPosition);
        Debug.Log(BMD.EndPosition);
        Debug.Log(BMD.Direction);

    }
}
