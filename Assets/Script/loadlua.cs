using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using UnityEngine.Networking;
using UnityEngine.UI;

public class loadlua : MonoBehaviour
{
    private LuaEnv luaEnv;
    private Button hotfixBtn;
    // Start is called before the first frame update
    void Awake()
    {
        luaEnv = new LuaEnv();
        hotfixBtn = transform.Find("Button2").GetComponent<Button>();
        hotfixBtn.onClick.AddListener(OnClickBtn1);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClickBtn1()
    {
        StartCoroutine(LoadAssetBundle("luacallcsharp"));
    }

    IEnumerator LoadAssetBundle(string fileName)
    {
        Debug.Log("开始下载");
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(@"Http://localhost/AssetsBundle/luascript.unity.assetbundle");
        yield return request.SendWebRequest();
        Debug.Log("下载完成");
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        TextAsset ta = ab.LoadAsset(fileName + ".lua.txt") as TextAsset;
        luaEnv.DoString(ta.text);
    }
}
