using System.Collections;
using System.Collections.Generic;
using PlasticGui;
using UnityEditor;
using UnityEditor.Build.Profile;
using UnityEngine;

public class BuildMiniGame : MonoBehaviour
{
    [MenuItem("BuildMiniGame/BuildWeixinMiniGame")]
    private static void BuildWeixinMiniGame()
    {
        BuildWeixinMiniGamePlayer();
    }

    private static void BuildWeixinMiniGamePlayer()
    {
        List<string> buildProfilePathList = new List<string>()
        {
            "Assets/Settings/Build Profiles/performance.asset"
        };
        
        for (int i = 0; i < buildProfilePathList.Count; i++)
        {
            BuildProfile buildProfile = AssetDatabase.LoadAssetAtPath(buildProfilePathList[i], typeof(BuildProfile)) as BuildProfile;
            if (buildProfile != null)
            {
                Debug.Log($"[BuildPipeline Debuglog] BuildProfile Path: \"{buildProfilePathList[i]}\" is building!");
                BuildMiniGameError buildMiniGameError = BuildPipeline.BuildMiniGame(buildProfile);
                Debug.Log("[BuildPipeline Debuglog] BuildMiniGame Result: " + buildMiniGameError);
            }
            else
            {
                Debug.Log($"[BuildPipeline Debuglog] BuildProfile Path: \"{buildProfilePathList[i]}\" is not found!");
            }
        }
    }
}
