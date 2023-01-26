using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ObjectDebug : MonoBehaviour
{
    public UnityEngine.Object Object;
    public ObjectField ObjectField;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(ObjectField.GetType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
