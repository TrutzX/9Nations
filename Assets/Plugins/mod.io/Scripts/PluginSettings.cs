using UnityEngine;

namespace ModIO
{
    /// <summary>Stores the settings used by various classes that are unique to the game/app.</summary>
    public class PluginSettings : ScriptableObject
    {
        // ---------[ NESTED CLASSES ]---------
        /// <summary>Data struct that is wrapped by the ScriptableObject.</summary>
        [System.Serializable]
        public struct Data
        {
            // ---------[ FIELDS ]---------
            [Tooltip("API URL to use when making requests")]
            public string   apiURL;
            [Tooltip("Game Id assigned to your game profile")]
            public int      gameId;
            [Tooltip("API Key assigned to your game profile")]
            public string   gameAPIKey;
            [Tooltip("Directory to use for mod installations")]
            public string   installationDirectory;
            [Tooltip("Directory to use for cached server data")]
            public string   cacheDirectory;
            [Tooltip("Log all web requests made to using Debug.Log")]
            public bool     logAllRequests;
        }

        // ---------[ CONSTANTS & STATICS ]---------
        /// <summary>Location of the settings file.</summary>
        public static readonly string FILE_PATH = "modio_settings";

        /// <summary>Has the asset been loaded.</summary>
        private static bool _loaded = false;

        /// <summary>Singleton instance.</summary>
        private static Data _dataInstance;

        /// <summary>The values that the plugin should use.</summary>
        public static Data data
        {
            get
            {
                #if UNITY_EDITOR
                if(!Application.isPlaying)
                {
                    PluginSettings.LoadDataInstance();
                }
                #endif

                if(!PluginSettings._loaded)
                {
                    PluginSettings.LoadDataInstance();
                }

                return PluginSettings._dataInstance;
            }
        }

        // ---------[ FIELDS ]---------
        /// <summary>Settings data.</summary>
        [SerializeField]
        #pragma warning disable 0649
        private Data m_data;
        #pragma warning restore 0649

        // ---------[ FUNCTIONALITY ]---------
        /// <summary>Loads the Data from the asset instance.</summary>
        private static void LoadDataInstance()
        {
            PluginSettings wrapper = Resources.Load<PluginSettings>(PluginSettings.FILE_PATH);

            if(wrapper == null)
            {
                PluginSettings._dataInstance = new Data();
            }
            else
            {
                Data settings = wrapper.m_data;

                // - Path variable replacement -
                // cachedir
                if(settings.cacheDirectory != null)
                {
                    string[] cacheDirParts = settings.cacheDirectory.Split(System.IO.Path.AltDirectorySeparatorChar,
                                                                           System.IO.Path.DirectorySeparatorChar);
                    for(int i = 0; i < cacheDirParts.Length; ++i)
                    {
                        if(cacheDirParts[i].ToUpper().Equals("$PERSISTENT_DATA_PATH$"))
                        {
                            cacheDirParts[i] = Application.persistentDataPath;
                        }

                        cacheDirParts[i] = cacheDirParts[i].Replace("$GAME_ID$", settings.gameId.ToString());
                    }
                    settings.cacheDirectory = IOUtilities.CombinePath(cacheDirParts);
                }

                // installdir
                if(settings.installationDirectory != null)
                {
                    string[] installDirParts = settings.installationDirectory.Split(System.IO.Path.AltDirectorySeparatorChar,
                                                                                    System.IO.Path.DirectorySeparatorChar);
                    for(int i = 0; i < installDirParts.Length; ++i)
                    {
                        if(installDirParts[i].ToUpper().Equals("$PERSISTENT_DATA_PATH$"))
                        {
                            installDirParts[i] = Application.persistentDataPath;
                        }

                        installDirParts[i] = installDirParts[i].Replace("$GAME_ID$", settings.gameId.ToString());
                    }

                    settings.installationDirectory = IOUtilities.CombinePath(installDirParts);
                }

                // apply to data instance
                PluginSettings._dataInstance = settings;
            }

            PluginSettings._loaded = true;
        }

        #if UNITY_EDITOR
        /// <summary>Locates the PluginSettings asset used by the plugin.</summary>
        [UnityEditor.MenuItem("Tools/mod.io/Edit Settings", false)]
        public static void FocusAsset()
        {
            PluginSettings settings = Resources.Load<PluginSettings>(PluginSettings.FILE_PATH);

            if(settings == null)
            {
                settings = PluginSettings.InitializeAsset();
            }

            UnityEditor.EditorGUIUtility.PingObject(settings);
            UnityEditor.Selection.activeObject = settings;
        }

        /// <summary>Creates the asset instance that the plugin will use.</summary>
        private static PluginSettings InitializeAsset()
        {
            PluginSettings.Data data = new PluginSettings.Data()
            {
                apiURL = APIClient.API_URL_PRODUCTIONSERVER + APIClient.API_VERSION,
                gameId = 0,
                gameAPIKey = string.Empty,
                cacheDirectory = "$PERSISTENT_DATA_PATH$/modio-$GAME_ID$",
                installationDirectory = "$PERSISTENT_DATA_PATH$/modio-$GAME_ID$/_installedMods",
                logAllRequests = false,
            };

            return SetGlobalValues(data);
        }

        /// <summary>Sets the values of the Plugin Settings.</summary>
        public static PluginSettings SetGlobalValues(PluginSettings.Data data)
        {
            string assetPath = "Assets/Resources/" + PluginSettings.FILE_PATH + ".asset";
            PluginSettings settings = ScriptableObject.CreateInstance<PluginSettings>();

            if(!UnityEditor.AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");
            }

            settings.m_data = data;

            UnityEditor.AssetDatabase.CreateAsset(settings, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            return settings;
        }

        /// <summary>Copies a Plugin Settings' values into the main asset.</summary>
        public static PluginSettings SetAssetAsGlobal(string assetPath)
        {
            PluginSettings valuesToCopy = UnityEditor.AssetDatabase.LoadAssetAtPath<PluginSettings>(assetPath);
            if(valuesToCopy == null)
            {
                Debug.LogError("[mod.io] PluginSettings at " + assetPath + " could not be found and"
                               + " thus the globally used PluginSettings asset was unchanged.");
                return null;
            }

            return PluginSettings.SetGlobalValues(valuesToCopy.m_data);
        }
        #endif
    }
}
