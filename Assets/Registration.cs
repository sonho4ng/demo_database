using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Registration : FormHandlerBase
{
    public InputField nameField;
    public InputField passwordField;
    public Button submitButton;

    void Start()
    {
        submitButton.onClick.AddListener(OnSubmitClicked);
    }

    // Build form dữ liệu để gửi POST
    protected override WWWForm BuildPostForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        return form;
    }

    // URL của API register.php
    protected override string GetRequestURL()
    {
        return "http://localhost/server_unity/register.php";
    }

    // Xử lý phản hồi từ server
    protected override void OnResponse(string responseText)
    {
        Debug.Log("Server response: " + responseText);

        if (responseText == "User registered successfully!")
        {
            Debug.Log("✅ User created successfully.");
            SceneManager.LoadScene(0); // Load scene index 0
        }
        else
        {
            Debug.LogWarning("❌ User creation failed. Server message: " + responseText);
        }
    }

    // Hàm gọi khi nhấn nút submit
    public void OnSubmitClicked()
    {
        if (VerifyInputs())
        {
            StartCoroutine(PostForm());
        }
    }

    // Kiểm tra input
    private bool VerifyInputs()
    {
        if (string.IsNullOrEmpty(nameField.text) || string.IsNullOrEmpty(passwordField.text))
        {
            Debug.LogWarning("⚠️ Please enter both username and password.");
            return false;
        }
        return true;
    }
}
