using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype_S
{
    public class ConsoleUI : MonoBehaviour
    {
        public TextMeshProUGUI consoleText;
        public ScrollRect scrollRect;

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }
        void HandleLog(string logString, string stackTrace, LogType type)
        {
            consoleText.text += "\n" + logString;

            StartCoroutine(ScrollToBottom());
        }
        
        private IEnumerator ScrollToBottom()
        {
            // Wait for the end of the frame to ensure the layout has been rebuilt
            yield return new WaitForEndOfFrame();

            // Set the vertical scroll position to the bottom (0f)
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}
