using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using System;

//csharp����lua


//��lua�е�tableӳ��Ϊ��
class Tab
{
    public string name;
    public int age;
}

[CSharpCallLua]
public interface ITab//��lua�е�tableӳ��Ϊ�ӿ�  ������ǩ
{
    string name { get; set; }
    int age { get; set; }
    void func();
    void func1(int a, int b);
    void func2(int a, int b);
    void func3(int a, int b);
}

//��lua�еı�ӳ��Ϊ�ֵ�  ֻ�ܷ��ʡ�key=value����ʽ

public delegate void delefunc4();//�޲�ί��
public delegate void delefunc5(int a,int b);
[CSharpCallLua]
public delegate testFunc6 delefunc6(int a, int b, out int a1, out string a2);
//����ֵΪһ��������ί��
[CSharpCallLua]
public delegate Action delefunc8();
//����һ���вεĺ�����ί��
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
        //c#����lua�еĴ���
        luaEnv.DoString("print('hello world')");
        //lua����c#�еĴ���
        luaEnv.DoString("CS.UnityEngine.Debug.Log('hello world')");
        ////ͨ��resources����luaԴ�ļ�
        //TextAsset ta = Resources.Load<TextAsset>("Lua/hotfix.lua");
        //luaEnv.DoString(ta.text);
        ////ͨ��require����luaԴ�ļ�
        //luaEnv.DoString("require('Lua/hotfix')");

        ////�Զ�����ط���,ͨ���Զ��������ʵ�ּ���ָ��Ŀ¼��lua�ļ�
        //luaEnv.AddLoader(MyLoader);
        //luaEnv.DoString("require 'hotfix'");

        //����lua�е�ȫ�ֱ���
        luaEnv.DoString("require 'hotfix'");
        //��ȡlua���е�ȫ�ֱ���  ӳ��
        int a = luaEnv.Global.Get<int>("a");
        string str = luaEnv.Global.Get<string>("str");
        bool boolean = luaEnv.Global.Get<bool>("boolean");
        Debug.Log("a=" + a);
        Debug.Log("str=" + str);
        Debug.Log("boolean=" + boolean);
        Debug.Log(luaEnv.Global.Get<int>("c"));
        
        //��lua�еı�ӳ��Ϊ��
        //ӳ��Ϊ���ʱ���޸����е�ֵ����Ӱ��lua�ű�
        Tab t = luaEnv.Global.Get<Tab>("tab");
        Debug.Log("t.name=" + t.name);
        Debug.Log("t.age=" + t.age);
        t.name = "�����";
        Debug.Log("�޸�ǰ��" + t.name);
        luaEnv.DoString("print('�޸ĺ�'..tab.name)");

        //��lua�еı�ӳ��Ϊ�ӿ�
        //ӳ��Ϊ�ӿڵ�ʱ���޸Ľӿ��е�ֵ��Ӱ��lua�ű�
        ITab it = luaEnv.Global.Get<ITab>("tab");
        Debug.Log("it.name=" + it.name);
        Debug.Log("it.age=" + it.age);
        it.name = "�����";
        Debug.Log("�޸�ǰ��" + it.name);
        luaEnv.DoString("print('�޸ĺ�'..tab.name)");
        it.func();
        it.func1(10, 10);
        it.func2(20, 20);
        it.func3(30, 30);

        //��lua�еı�ӳ��Ϊ�ֵ�
        Dictionary<string, object> dic = luaEnv.Global.Get<Dictionary<string, object>>("tab");
        foreach(string key in dic.Keys)
        {
            Debug.Log("key=" + key + " " + "value=" + dic[key]);
        }

        //��lua�еı�ӳ��Ϊlist ֻ�ܷ���value����
        List<object> list = luaEnv.Global.Get<List<object>>("tab");
        foreach(object obj in list)
        {
            Debug.Log(obj);
        }

        //��lua�е�tableӳ��Ϊluatable��
        LuaTable lt = luaEnv.Global.Get<LuaTable>("tab");
        Debug.Log(lt.Get<string>("name"));
        Debug.Log(lt.Get<int>("age"));
        Debug.Log(lt.Get<int>(1));
        Debug.Log(lt.Get<int>(2));
        Debug.Log(lt.Get<int>(3));

        //����lua�е�ȫ�ֺ��� ӳ�䵽ί��
        LuaFunction func4 = luaEnv.Global.Get<LuaFunction>("func4");
        func4.Call();
        Action act1 = luaEnv.Global.Get<Action>("func4");//�޲�
        act1();
        delefunc4 Func4 = luaEnv.Global.Get<delefunc4>("func4");
        Func4();
        Action<int,int> act2 = luaEnv.Global.Get<Action<int,int>>("func5");
        act2(10, 10);
        delefunc5 Func5 = luaEnv.Global.Get<delefunc5>("func5");
        Func5(10, 10);
        //��lua�������ж������ֵ��ʱ��
        delefunc6 Func6 = luaEnv.Global.Get<delefunc6>("func6");
        int a1;
        string a2;
        Debug.Log(Func6(10, 10, out a1,out a2));
        testFunc6 tc = Func6(10, 10, out a1, out a2);
        Debug.Log("tc.num=" + tc.num + " " + "tc.nu=" + tc.nu);
        Debug.Log("a1=" + a1 + " " + "a2=" + a2);
        //����һ������
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
