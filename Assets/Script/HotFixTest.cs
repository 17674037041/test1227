using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using System;

//csharp调用lua


//将lua中的table映射为类
class Tab
{
    public string name;
    public int age;
}

[CSharpCallLua]
public interface ITab//将lua中的table映射为接口  必须打标签
{
    string name { get; set; }
    int age { get; set; }
    void func();
    void func1(int a, int b);
    void func2(int a, int b);
    void func3(int a, int b);
}

//将lua中的表映射为字典  只能访问“key=value”形式

public delegate void delefunc4();//无参委托
public delegate void delefunc5(int a,int b);
[CSharpCallLua]
public delegate testFunc6 delefunc6(int a, int b, out int a1, out string a2);
//返回值为一个函数的委托
[CSharpCallLua]
public delegate Action delefunc8();
//返回一个有参的函数的委托
[CSharpCallLua]
public delegate LuaFunction delefunc10();

public class testFunc6
{
    public string nu;
    public int num;
}

public class HotFixTest : MonoBehaviour
{
    private LuaEnv luaEnv;
    // Start is called before the first frame update
    void Start()
    {
        List<int> list1 = new List<int>();
        luaEnv = new LuaEnv();
        //c#调用lua中的代码
        luaEnv.DoString("print('hello world')");
        //lua调用c#中的代码
        luaEnv.DoString("CS.UnityEngine.Debug.Log('hello world')");
        ////通过resources加载lua源文件
        //TextAsset ta = Resources.Load<TextAsset>("Lua/hotfix.lua");
        //luaEnv.DoString(ta.text);
        ////通过require加载lua源文件
        //luaEnv.DoString("require('Lua/hotfix')");

        ////自定义加载方法,通过自定义加载器实现加载指定目录的lua文件
        //luaEnv.AddLoader(MyLoader);
        //luaEnv.DoString("require 'hotfix'");

        //访问lua中的全局变量
        luaEnv.DoString("require 'hotfix'");
        //获取lua当中的全局变量  映射
        int a = luaEnv.Global.Get<int>("a");
        string str = luaEnv.Global.Get<string>("str");
        bool boolean = luaEnv.Global.Get<bool>("boolean");
        Debug.Log("a=" + a);
        Debug.Log("str=" + str);
        Debug.Log("boolean=" + boolean);
        Debug.Log(luaEnv.Global.Get<int>("c"));
        
        //将lua中的表映射为类
        //映射为类的时候修改类中的值不会影响lua脚本
        Tab t = luaEnv.Global.Get<Tab>("tab");
        Debug.Log("t.name=" + t.name);
        Debug.Log("t.age=" + t.age);
        t.name = "李宏宇";
        Debug.Log("修改前：" + t.name);
        luaEnv.DoString("print('修改后：'..tab.name)");

        //将lua中的表映射为接口
        //映射为接口的时候修改接口中的值会影响lua脚本
        ITab it = luaEnv.Global.Get<ITab>("tab");
        Debug.Log("it.name=" + it.name);
        Debug.Log("it.age=" + it.age);
        it.name = "李宏宇";
        Debug.Log("修改前：" + it.name);
        luaEnv.DoString("print('修改后：'..tab.name)");
        it.func();
        it.func1(10, 10);
        it.func2(20, 20);
        it.func3(30, 30);

        //将lua中的表映射为字典
        Dictionary<string, object> dic = luaEnv.Global.Get<Dictionary<string, object>>("tab");
        foreach(string key in dic.Keys)
        {
            Debug.Log("key=" + key + " " + "value=" + dic[key]);
        }

        //将lua中的表映射为list 只能访问value类型
        List<object> list = luaEnv.Global.Get<List<object>>("tab");
        foreach(object obj in list)
        {
            Debug.Log(obj);
        }

        //将lua中的table映射为luatable类
        LuaTable lt = luaEnv.Global.Get<LuaTable>("tab");
        Debug.Log(lt.Get<string>("name"));
        Debug.Log(lt.Get<int>("age"));
        Debug.Log(lt.Get<int>(1));
        Debug.Log(lt.Get<int>(2));
        Debug.Log(lt.Get<int>(3));

        //访问lua中的全局函数 映射到委托
        LuaFunction func4 = luaEnv.Global.Get<LuaFunction>("func4");
        func4.Call();
        Action act1 = luaEnv.Global.Get<Action>("func4");//无参
        act1();
        delefunc4 Func4 = luaEnv.Global.Get<delefunc4>("func4");
        Func4();
        Action<int,int> act2 = luaEnv.Global.Get<Action<int,int>>("func5");
        act2(10, 10);
        delefunc5 Func5 = luaEnv.Global.Get<delefunc5>("func5");
        Func5(10, 10);
        //当lua函数中有多个返回值的时候
        delefunc6 Func6 = luaEnv.Global.Get<delefunc6>("func6");
        int a1;
        string a2;
        Debug.Log(Func6(10, 10, out a1,out a2));
        testFunc6 tc = Func6(10, 10, out a1, out a2);
        Debug.Log("tc.num=" + tc.num + " " + "tc.nu=" + tc.nu);
        Debug.Log("a1=" + a1 + " " + "a2=" + a2);
        //返回一个函数
        delefunc8 func8 = luaEnv.Global.Get<delefunc8>("func8");
        func8()();
        delefunc10 func10 = luaEnv.Global.Get<delefunc10>("func10");
        LuaFunction Func10 = luaEnv.Global.Get<LuaFunction>("func10");
        object[] objs = Func10.Call();
        foreach(object obj in objs)
        {
            Debug.Log("obj=" + obj);
        }
    }
    
    private byte[] MyLoader(ref string filePath)
    {
        string path = Application.streamingAssetsPath+"/"+ filePath + ".lua.txt";
        print(path);
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(path));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
