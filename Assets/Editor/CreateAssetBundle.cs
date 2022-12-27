using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundle
{
    [MenuItem("Tools/Build AssetBundle")]
    public static void BuildAssetBundles()
    {
        string dir = "AssetsBundle";//默认在工程目录下
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows64);
    }

}
