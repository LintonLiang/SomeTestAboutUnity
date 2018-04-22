using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class Utility {

    public static void WriteFile(string path ,string name, string info)
    {
        //StreamWriter sw;
        //FileInfo fi = new FileInfo(path + "/" + name);
        //sw = fi.CreateText();
        //sw.WriteLine(info);
        //sw.Close();
        //sw.Dispose();
        using (FileStream fsWriter = new FileStream(path + "/" + name, FileMode.OpenOrCreate, FileAccess.Write))
        {
            byte[] buffer = Encoding.Default.GetBytes(info);
            fsWriter.Write(buffer, 0, buffer.Length);
        }

    }
    public static string ReadFile(string path, string name)
    {
        string readInfo = "";
        using (FileStream fsReader = new FileStream(path + "/" + name, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer = new byte[1024 * 1024 * 5];

            while (true)
            {
                
                int r = fsReader.Read(buffer, 0, buffer.Length);
                if (r == 0)
                {
                    break;
                }
                readInfo += Encoding.Default.GetString(buffer, 0, r);
            }
               
        }
        return readInfo;
    }
    public static void MakeToast(string info)
    {
        AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        currentActivity.Call("runOnThread", new AndroidJavaRunnable(() =>
        {
            Toast.CallStatic<AndroidJavaObject>("makeText", currentActivity, info, Toast.GetStatic<int>("LENGTH_LONG")).Call("show");
        }));
    }
}
