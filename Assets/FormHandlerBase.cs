using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public abstract class FormHandlerBase : MonoBehaviour
{
    // Hàm này dùng để build form trước khi gửi POST
    protected abstract WWWForm BuildPostForm();

    // Hàm xử lý kết quả sau khi POST hoặc GET
    protected abstract void OnResponse(string responseText);

    // URL API cần override trong class con
    protected abstract string GetRequestURL();

    // Gửi POST (dùng cho form nhập liệu)
    protected IEnumerator PostForm()
    {
        WWWForm form = BuildPostForm();
        UnityWebRequest www = UnityWebRequest.Post(GetRequestURL(), form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("POST Error: " + www.error);
        }
        else
        {
            OnResponse(www.downloadHandler.text);
        }
    }

    // Gửi GET (dùng cho form chọn dữ liệu)
    protected IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(GetRequestURL());
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("GET Error: " + www.error);
        }
        else
        {
            OnResponse(www.downloadHandler.text);
        }
    }
}
