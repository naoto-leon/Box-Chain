using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enumtest : MonoBehaviour
{


    protected enum fru
    {
        apple,
        orange,
        banana
    };

    [SerializeField]
    protected fru _fru = new fru();



    // Update is called once per frame
    void update()
    {
        switch (_fru)
        {
            case fru.apple:
                Debug.Log("apple");
                break;

            case fru.orange:
                Debug.Log("Orange");
                break;

            case fru.banana:
                Debug.Log("banana");
                break;


            default:
                Debug.Log("Non");
                break;

        }

    }
}
