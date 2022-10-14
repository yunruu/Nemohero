using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Firebase;
using Firebase.Auth;

public class FeedbackManager : MonoBehaviour
{

    public TMP_InputField feedbackField;
    [SerializeField] GameObject sendButton;
    //[SerializeField] TMPro.Button sendButton;
    //[SerializeField] UnityEngine.UI.Button sendButton;
    [SerializeField] bool sendDirect;
    [SerializeField]
    //public TextMeshProUGUI emailSentMessage;
    public FirebaseAuth auth;
    private FirebaseUser user;
    public TextMeshProUGUI feedbackSent;
    public TextMeshProUGUI feedbackNotSent;

    const string kSenderEmailAddress = "nemohero2022@gmail.com"; //change to nemohero email
    const string kSenderPassword = "ipooewsgzmvivksx"; //create app password for nemohero email
    const string kReceiverEmailAddress = "nemohero2022@gmail.com";

    // Start is called before the first frame update
    void Start()
    {
        // UnityEngine.Assertions.Assert.IsNotNull(feedbackField);
        //UnityEngine.Assertions.Assert.IsNotNull(sendButton);
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendAnEmail()
    {
        // Create mail
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(kSenderEmailAddress);
        mail.To.Add(kReceiverEmailAddress);
        mail.Subject = "Nemohero User Feedback: " + user.DisplayName;
        mail.Body = feedbackField.text;

        // Setup server 
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential(
            kSenderEmailAddress, kSenderPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        // Send mail to server, print results
        try
        {
            if (feedbackField.text != "")
            {
                smtpServer.Send(mail);
            }
            //smtpServer.Send(mail);
        }
        catch (System.Exception e)
        {
            Debug.Log("Email error: " + e.Message);
        }
        finally
        {
            if (feedbackField.text == "")
            {
                feedbackNotSent.gameObject.SetActive(true);
                feedbackSent.gameObject.SetActive(false);
                Debug.Log("No email sent");
            }
            else
            {
                feedbackSent.gameObject.SetActive(true);
                feedbackNotSent.gameObject.SetActive(false);
                Debug.Log("Email sent!");
            }
        }
    }

    /**private IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        emailSentMessage.gameObject.SetActive(false);
    }*/

    /**public void SendEmail()
    {
        //string email = user.Email;
        //string password = user.P;

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("dilyspang@gmail.com");
        mail.To.Add(new MailAddress("nemohero2022@gmail.com"));

        mail.Subject = "NemoHero User Feedback";
        mail.Body = feedbackField.text;

        SmtpServer.Credentials = new System.Net.NetworkCredential();
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);
    }*/
}
