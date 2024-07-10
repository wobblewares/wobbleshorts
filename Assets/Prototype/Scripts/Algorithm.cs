#nullable enable

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace WobbleShorts
{
    public static class Algorithm
    {
        public static int FramesPerSecond = 6;
        
        public static AppState AppState
        {
            get
            {
                if (_appState == null) {
                    _appState = new ();
                    var accountConfigs = GetConfigs<AccountConfig>();
                    foreach (var config in accountConfigs)
                        AppState.Add(config.Create());
                }
                return _appState;
            }
        }
        
        private static AppState? _appState;
        
        private static T[] GetConfigs<T>() where T : ScriptableObject
        {
            var configs = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            return configs.Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x))).ToArray();
        }
    }
}