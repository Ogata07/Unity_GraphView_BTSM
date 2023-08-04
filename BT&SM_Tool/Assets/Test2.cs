using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public List<System.Object> list = new List<System.Object>();
    private int valuHp = 0;
    // Start is called before the first frame update
    void Start()
    {
        list.Add(10);
        list.Add(20);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 1000; i++) { 
        int value = valuHp;
        Debug.Log(i);
        valuHp = i;
        }
        //value =list[1] ;
    }
}
