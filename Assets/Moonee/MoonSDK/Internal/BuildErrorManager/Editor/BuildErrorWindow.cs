using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Moonee.MoonSDK.Internal.Editor
{
    
    /// <summary>
    /// For logging build errors and warnings.  Aggregates all received errors and displays it in an EditorWindow for the user to review. 
    /// </summary>
    public class BuildErrorWindow : EditorWindow
    {
        private const string TAG = "BuildErrorWindow";


        private const string TITLE = /*"Build Errors"*/  "Build Warnings";
        //private const string WarningTITLE = "Warning Errors";


        // Data 
        // Due to serialization/saving issues Dictionary cannot be used here 
        private readonly List<BuildErrorConfig.ErrorID> errorIDs = new List<BuildErrorConfig.ErrorID>(); 
        private readonly List<string> errorMessages = new List<string>();

        // GUI Related
        private Vector2 scrollPos0;
        private Vector2 scrollPos1;

        // Singleton - to preserve data between reloads/builds 
        private static BuildErrorWindow _instance; 
        private static BuildErrorWindow Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (BuildErrorWindow) GetWindow(typeof(BuildErrorWindow), true, TITLE, true);
                }

                return _instance;
            }
        }
        private static void ShowWindow()
        {
            // Get existing open window or if none, make a new one:
            var window = (BuildErrorWindow)EditorWindow.GetWindow(typeof(BuildErrorWindow), true, TITLE, true);
            window.Show();
        }
        
        
        /* ==========================================================================================
         * Logging Logic 
         * ==========================================================================================
         */

        internal static void Clear()
        {
            Instance.errorIDs.Clear();
            Instance.errorMessages.Clear();
            Highlighter.Stop();
        }
        
        internal static void LogBuildError(BuildErrorConfig.ErrorID errorID)
        {
            if (BuildErrorConfig.ErrorMessageDict.ContainsKey(errorID))
            {
                LogBuildError(
                    errorID,
                    BuildErrorConfig.ErrorMessageDict[errorID]);
            }
            else
            {
                MooneeLog.LogW(TAG, "Warning nonexistent ErrorID"); 
            }
        }

        private static void LogBuildError(BuildErrorConfig.ErrorID errorID, string message)
        {
            ShowWindow();
            
            string errorMessage = $"{errorID} : {message}";

            Instance.errorIDs.Add(errorID);
            Instance.errorMessages.Add(message); 
            
            Debug.LogError(errorMessage);
        }

        
        /* ==========================================================================================
         * Display Logic 
         * ==========================================================================================
         */

        private static void DisplayError(string errorMessage)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.HelpBox(
                errorMessage,
                MessageType.Warning
            );
            EditorGUILayout.EndHorizontal(); 
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Clear"))
            {
                Clear();
            }
            if (errorIDs.Count == 0)
            {
                GUILayout.Label("No Moon SDK Build Warnings Detected"); 
            }
            if (errorIDs.Count > 0)
            {
                GUILayout.Label($"Moon SDK Build Warnings ({errorIDs.Count})", EditorStyles.boldLabel);

                scrollPos0 = EditorGUILayout.BeginScrollView(scrollPos0, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

                for (var i = 0; i < errorIDs.Count; i++)
                {
                    DisplayError( errorMessages[i]); 
                }

                EditorGUILayout.EndScrollView();
            }
        }
    }
}