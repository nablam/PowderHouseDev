using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoggerToFile : MonoBehaviour
{



    string fullpath;


    //public string output = "";
    //public string stack = "";
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
        //if (type == LogType.Error || type == LogType.Exception)
        //{

        //    var logEntry = string.Format("\n +++++++++++++++++++++++++++++++++   \n {0} {1} \n {2}\n  {3} \n {4} ", DateTime.Now, type, logString, stackTrace, "+++++++++++++++++++++++++++++++++");

        //    File.AppendAllText(fullpath, logEntry);
        //}
        //else
        //       if ( type == LogType.Warning)
        //{

        //    var logEntry = string.Format("\n ------------------------------------ \n {0} {1} \n {2}\n  {3} \n ", DateTime.Now, type, logString, "------------------------------------");

        //    File.AppendAllText(fullpath, logEntry);
        //}

        //if (type == LogType.Log  )
        //{
        //    File.AppendAllText(fullpath, logString);
        //}
    }

    public static LoggerToFile Instance = null;

    private void Awake()
    {

        fullpath = ArzDirPath + "/" + "ARZbitsandbytes.txt";
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else
            Destroy(gameObject);
    }


    public void CoWrite(List<byte> arglist)
    {
        StartCoroutine(DoasCoroutine(arglist));
    }


    //list or rray of bytes will be printed in hex  00 aa 00 ba ...
    public void Write4BytesHEx(IEnumerable<byte> arglist)
    {
        for (int x = 0; x < arglist.Count(); x++)
        {
            if (x % 4 == 0) { File.AppendAllText(fullpath, Environment.NewLine); }
            File.AppendAllText(fullpath, " " + arglist.ElementAt(x).ToString("X2") + " ");
        }



    }

    IEnumerator DoasCoroutine(List<byte> arglist)
    {

        yield return new WaitForSeconds(10f);
        Write4BytesHEx(arglist);
    }

    //private StreamWriter _writer;

    string ArzDirPath
    {
        get
        {
            // return Application.persistentDataPath;
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
    }

}
