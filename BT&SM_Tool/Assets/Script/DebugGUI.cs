using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGUI : MonoBehaviour
{
    [SerializeField,Header("SM用監視対象をおいてください")]
    private SMManager sMManager = null;
    [SerializeField, Header("BT用監視対象をおいてください")]
    private BTManager BTManager = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        //GUILayout.Label()
    }
}
