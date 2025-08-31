using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase.Extensions;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FirebaseAuthController : MonoBehaviour
{
    [Header("Firebase")]
    private FirebaseAuth auth;
    private FirebaseUser user;

    [Header("Utility")]
    private Coroutine hideCoroutine;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField input_id;
    [SerializeField] private TMP_InputField input_pw;
    [SerializeField] private Image img_id;
    [SerializeField] private Image img_pw;

    [Header("Messages")]
    [SerializeField] private TextMeshProUGUI errorMessage;

    [Header("UI Windows")]
    [SerializeField] private GameObject loginObject;
    [SerializeField] private GameObject playObject;
    [SerializeField] private GameObject playLogingObject;
    [SerializeField] private GameObject playBtnObject;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    private void SetErrorMessage(string message, float duration = 2f)
    {
        if (errorMessage != null)
        {
            errorMessage.text = message;
            errorMessage.gameObject.SetActive(true);
            input_id.text = "";
            input_pw.text = "";
            img_id.color = new Color(1f, 0.847f, 0.847f);
            img_pw.color = new Color(1f, 0.847f, 0.847f);
            if (hideCoroutine != null)
            {
                StopCoroutine(hideCoroutine);
            }

            if (!string.IsNullOrEmpty(message))
            {
                hideCoroutine = StartCoroutine(HideErrorAfterSeconds(duration));
            }
        }
    }

    private IEnumerator HideErrorAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (errorMessage != null)
        {
            errorMessage.gameObject.SetActive(false);
            img_id.color = new Color(0.9622642f, 0.9622642f, 0.9622642f);
            img_pw.color = new Color(0.9622642f, 0.9622642f, 0.9622642f);
        }

        hideCoroutine = null;
    }

    public void Create()
    {
        auth.CreateUserWithEmailAndPasswordAsync(input_id.text, input_pw.text)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("회원가입 취소");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.Log("회원가입 실패");

                    string message = "";
                    if (task.Exception != null)
                    {
                        foreach (var e in task.Exception.InnerExceptions)
                        {
                            message = e.Message;
                        }
                    }

                    SetErrorMessage(message);
                    return;
                }

                Firebase.Auth.AuthResult result = task.Result;
                user = result.User;

                Debug.Log("회원가입 완료: " + user.Email);
                SetErrorMessage("Sign Up Complete!"); // 성공 시 메시지 지우기
            });
    }

    private void LookPlayWindow()
    {
        loginObject.SetActive(false);
        playObject.SetActive(true);
        StartCoroutine(LookLogingObject());
    }
    private IEnumerator LookLogingObject()
    {
        yield return new WaitForSeconds(5.0f);
        playLogingObject.SetActive(false);
        playBtnObject.SetActive(true);
    }
    private void LookLoginWindow()
    {
        loginObject.SetActive(true);

        playObject.SetActive(false);
        playLogingObject.SetActive(true);
        playBtnObject.SetActive(false);
    }
    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(input_id.text, input_pw.text)
            .ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("로그인 취소");
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.Log("로그인 실패");

                    string message = "";

                    if (task.Exception != null)
                    {
                        foreach (var e in task.Exception.InnerExceptions)
                        {
                            message = e.Message;
                        }
                    }

                    SetErrorMessage(message);
                    return;
                }

                Firebase.Auth.AuthResult result = task.Result;
                user = result.User;

                Debug.Log("로그인 성공: " + user.Email);
                SetErrorMessage("Login Successful!");
                LookPlayWindow();
            });
    }

    public void Logout()
    {
        auth.SignOut();
        user = null;
        LookLoginWindow();
        Debug.Log("로그아웃");
    }

    public void SceneChangeLoginToPlay()
    {
        SceneManager.LoadScene("Play");
    }
}
