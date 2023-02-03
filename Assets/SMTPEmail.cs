using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using PixelCrushers.DialogueSystem;


public class SMTPEmail : MonoBehaviour {


    public string SenderAddress = "skrt1533@gmail.com";
    public string ReceiverAddress = "409a206@naver.com";
    public string MailSubject = "메타계원 프로젝트";
    public string MailBody = 
    "김형진님! 퀘스트 완료를 축하드립니다.이 인증 메일을 캡쳐하여 학생처에 제출하시면 경품을 받으실 수 있습니다.\n" + 
    "학번: 20xx123456 \n" + 
    "이름: 김형진";

    private string password = "wcmdsuyseowprmgw";
    
    [Tooltip("Typically leave unticked so temporary Dialogue Managers don't unregister your functions.")]
    public bool unregisterOnDisable = false;

    public void SendMail(double dummy)

    {

        MailMessage mail = new MailMessage();


        mail.From = new MailAddress(SenderAddress); // 보내는사람

        mail.To.Add(ReceiverAddress); // 받는 사람

        mail.Subject = MailSubject;

        mail.Body = MailBody;


        // 첨부파일 - 대용량은 안됨.

        //System.Net.Mail.Attachment attachment;

        //attachment = new System.Net.Mail.Attachment("D:\\Test\\2018-06-11-09-03-17-E7104.mp4"); // 경로 및 파일 선택

        //mail.Attachments.Add(attachment);


        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

        smtpServer.Host = "smtp.gmail.com";
        smtpServer.Port = 587;
        smtpServer.Timeout = 10000;
        //smtpServer.UseDefaultCredentials = true;
        smtpServer.EnableSsl = true;//true false
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpServer.Credentials = new System.Net.NetworkCredential("skrt1533@gmail.com", password);
        
        try{
            smtpServer.Send(mail);
            mail.Dispose();
            Debug.Log("success");
        }
        catch(Exception e) {
            Debug.Log(e.ToString());
        } 


    }

    // public void SendMail() {
    //     Application.OpenURL("mailto:" + ReceiverAddress + "?subject=" + MailSubject + "&body=" + MailBody);
    // }

    // private string EscapeURL(string url) {
    //     return WWW.EscapeURL(url).Replace("+", "%20");
    // }

    
    #region Register with Lua
    void OnEnable()
    {
        // Make the functions available to Lua: (Replace these lines with your own.)
        // Lua.RegisterFunction("DebugLog", this, SymbolExtensions.GetMethodInfo(() => DebugLog(string.Empty)));
        // Lua.RegisterFunction("AddOne", this, SymbolExtensions.GetMethodInfo(() => AddOne((double)0)));

        Lua.RegisterFunction("SendMail", this, SymbolExtensions.GetMethodInfo(() => SendMail((double)0)));
    }

    void OnDisable()
    {
        if (unregisterOnDisable)
        {
            // Remove the functions from Lua: (Replace these lines with your own.)
            // Lua.UnregisterFunction("DebugLog");
            // Lua.UnregisterFunction("AddOne");
            Lua.UnregisterFunction("SendMail");

        }
    }

   #endregion

}
