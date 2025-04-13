using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
public class SelectFormHandler : FormHandlerBase
{
    public Button fetchButton;
    public TMP_Text displayText; // Nếu bạn dùng TextMeshPro thì đổi sang TMP_Text và thêm using TMPro;

    void Start()
    {
        fetchButton.onClick.AddListener(OnFetchClicked);
    }

    protected override WWWForm BuildPostForm()
    {
        return null; // Không cần form khi dùng GET
    }

    protected override string GetRequestURL()
    {
        return "http://localhost/server_unity/get_users.php";
    }

    protected override void OnResponse(string responseText)
    {
        if (responseText.StartsWith("["))
        {
            string[] users = JsonHelper.FromJson<string>(WrapJsonArray(responseText));
            displayText.text = "Danh sách người dùng:\n";
            foreach (string name in users)
            {
                displayText.text += "- " + name + "\n";
            }
        }
        else
        {
            displayText.text = "Lỗi hoặc không có người dùng nào: " + responseText;
        }
    }

    public void OnFetchClicked()
    {
        StartCoroutine(GetData());
    }

    // Helper: JsonUtility không parse được array gốc nên cần bọc lại
    private string WrapJsonArray(string rawJson)
    {
        return "{\"Items\":" + rawJson + "}";
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public T[] Items;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }
    }
}
