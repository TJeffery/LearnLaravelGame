using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JavascriptHook : MonoBehaviour
{
    [SerializeField] public TMP_Text testText;
    [SerializeField] public TMP_Text webTestJson;

    public void sendMessage(string testTest)
    {
        testText.text = testTest;
    }
    public void TestJson(string json)
    {
        JsonObj jsonObj = JsonUtility.FromJson<JsonObj>(json);
        testText.text = jsonObj.message;
    }
    public class JsonObj
    {
        public string message;
    }

}
