using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using UnityEngine.UI;

//luaµ÷ÓÃcsharp

[Hotfix]
public class luacallcsharp : MonoBehaviour
{
    private LuaEnv luaEnv;
    private Button btn1;
    private Button btn2;
    private Transform cube;
    private Transform sphere;
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.Find("Cube").transform;
        sphere = GameObject.Find("Sphere").transform;
        btn1 = transform.Find("Button").GetComponent<Button>();
        btn2 = transform.Find("Button1").GetComponent<Button>();

        btn1.onClick.AddListener(OnClickBtn1);
        btn2.onClick.AddListener(OnClickBtn2);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [LuaCallCSharp]
    void OnClickBtn1()
    {

    }

    [LuaCallCSharp]
    void OnClickBtn2()
    {

    }
}
