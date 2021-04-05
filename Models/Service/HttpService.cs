using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Models.Service
{
    /// <summary>
    /// 获取天气情况
    /// </summary>
    public class HttpService
    {
        public static string Get(string url, Dictionary<string, string> dic)
        {
            string result = "";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(url);
            if (dic.Count > 0) 
            {
                stringBuilder.Append("?");
                int i = 0;
                foreach(var item in dic)
                {
                    if (i > 0)
                    
                        stringBuilder.Append("&");
                    stringBuilder.AppendFormat("{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(item.Value));
                    i++;
                    
                }
            }

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(stringBuilder.ToString());
            //组合参数
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebResponse.GetResponseStream();
            try
            {
                //获取内容
                using StreamReader streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
            }

            finally
            {
                stream.Close();
            }
            Console.WriteLine(result);
            return result;
        }

        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            #region 添加响应参数
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            foreach(var item in dic)
            {
                if(i > 0)
                
                    stringBuilder.Append("&");
                stringBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
                
            }
            byte[] data = Encoding.UTF8.GetBytes(stringBuilder.ToString());
            httpWebRequest.ContentLength = data.Length;
            using Stream resqStream = httpWebRequest.GetRequestStream();
            resqStream.Write(data, 0, data.Length);
            resqStream.Close();
            #endregion
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
            result = streamReader.ReadToEnd();
            Console.WriteLine(result);
            return result;
        }
    }
}
