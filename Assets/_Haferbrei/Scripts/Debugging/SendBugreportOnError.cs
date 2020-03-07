//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using MEC;
using UnityEngine;

namespace Haferbrei {
public class SendBugreportOnError : MonoBehaviour
{
    private bool receivedErrors = false;
    private List<string> errorLogs = new List<string>();
    private CoroutineHandle sendCountdown;
    private bool stopLogging;
    
    private void Awake()
    {
        #if UNITY_EDITOR
        return;
        #endif
        DontDestroyOnLoad(this);
        Application.logMessageReceivedThreaded += ReceivedLog;
    }

    private void OnApplicationFocus(bool _hasFocus)
    {
        if (_hasFocus) return;
        SendMailIfErrorsOccured();
    }

    private IEnumerator<float> _SendMailAfterDelay()
    {
        yield return Timing.WaitForSeconds(3);
        SendMailIfErrorsOccured();
    }

    private void SendMailIfErrorsOccured()
    {
        Timing.KillCoroutines(sendCountdown);
        if (!receivedErrors) return;
        Timing.RunCoroutine(_SendErrorLogs());
        receivedErrors = false;
        errorLogs.Clear();
    }

    private IEnumerator<float> _SendErrorLogs()
    {
        string userName = Environment.UserName;
        string currentTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        string mailSubject = $"Automated Bugreport from user {userName} at time {currentTime}";
        string mailBody = (errorLogs.Count > 1) ? $"Folgende {errorLogs.Count} Fehler sind aufgetreten: \n\n" : "Folgender Fehler ist aufgetreten: \n\n";
        
        //errors
        foreach (var error in errorLogs)
        {
            mailBody += error;
        }
        
        //log file attachment
        List<Attachment> attachments = new List<Attachment>();
        string companyName = Application.companyName;
        string productName = Application.productName;
        string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string[] paths = { userFolder, $@"AppData\LocalLow\{companyName}\{productName}\Player.log" };

        Debug.Log("Path: " + Path.Combine(paths));
        
        string logFilePathS = Path.Combine(paths);
        string logFilePathD = Path.Combine(userFolder, $@"AppData\LocalLow\{companyName}\{productName}\Bugreport_{DateTime.Now:MM-dd-yyyy_HH-mm-ss}.log");
        File.Copy(logFilePathS, logFilePathD, true);

        attachments.Add(new Attachment(logFilePathD));
        
        //screenshot
        string screenshotFileName = $"BugreportScreenshot_{DateTime.Now:MM-dd-yyyy_HH-mm-ss}.png";
        string screenshotPath = Path.Combine(userFolder, $@"AppData\LocalLow\{companyName}\{productName}", screenshotFileName);
        ScreenCapture.CaptureScreenshot(screenshotPath);

        Debug.Log("captured screenshot @" + Time.frameCount + ", Time: " + DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture));

        float elapsedTime = 0f;
        while (!File.Exists(screenshotPath) && elapsedTime < 3f) //wenn der Screenshot nach 3s noch nicht gespeichert wurde, wird die Mail ohne Screenshot verschickt
        {
            elapsedTime += Time.deltaTime;
            yield return Timing.WaitForOneFrame;
        }
        if(File.Exists(screenshotPath)) attachments.Add(new Attachment(screenshotPath));
        
        Debug.Log("Send Email @" + Time.frameCount + ", Time: " + DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture));
        
        //send mail
        SendEmail.EmailWasSent += OnEmailWasSent;
        SendEmail.SendMail(mailSubject, mailBody, attachments);
    }

    private void OnEmailWasSent() => RestartApplication.Restart();

    private void ReceivedLog( string logString, string stackTrace, LogType logType )
    {
        if (stopLogging) return;
        if (logType != LogType.Exception && logType != LogType.Error) return;

        receivedErrors = true;
        string error = $"Error:\n{logString}\n\nStackTrace:\n{stackTrace}\n\n";
        errorLogs.Add(error);
        
        if (errorLogs.Count == 20)
        {
            errorLogs.Add("MORE THAN 20 ERRORS OCCURED. QUIT APPLICATION.");
            stopLogging = true;
            SendMailIfErrorsOccured();
        }
        else
        {
            Timing.KillCoroutines(sendCountdown);
            sendCountdown = Timing.RunCoroutine(_SendMailAfterDelay());
        }
    }
}
}