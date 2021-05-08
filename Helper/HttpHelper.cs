using ICSharpCode.SharpZipLib.GZip;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WallPoster.Models.Service
{
    /// <summary>
    /// HTTP请求工具类 Deflate压缩方式未做处理
    /// </summary>
    public class HttpHelper
    {
        private static ILog log = LogManager.GetLogger("HttpHelper");
        private static readonly object LockObj = new object();
        private static HttpClient client = null;
        /*private static readonly HttpClient _httpClient;*/

        public HttpHelper()
        {
            GetInstance();
        }
        /// <summary>
        /// 单例 双检锁/双重校验锁
        /// </summary>
        /// <returns></returns>
        public static HttpClient GetInstance()
        {
            if (client == null)
            {
                lock (LockObj)
                {
                    if (client == null)
                    {
                        client = new HttpClient();
                    }
                }
            }
            return client;
        }

        /*static HttpHelper()
        {
            client = new HttpClient() { BaseAddress = new Uri(Consts.NowWeather) };
            //帮HttpClient热身
            client.SendAsync(new HttpRequestMessage
            {
                Method = new HttpMethod("HEAD"),
                RequestUri = new Uri(Consts.NowWeather + "/")
            })
                .Result.EnsureSuccessStatusCode();
        }*/

        public static string Get(string url, Dictionary<string, string> dic)
        {
            string result = @"";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(url);
            if (dic.Count > 0)
            {
                stringBuilder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)

                        stringBuilder.Append("&");
                    stringBuilder.AppendFormat("{0}={1}", item.Key, System.Web.HttpUtility.UrlEncode(item.Value));
                    i++;

                }
            }
            //组合参数
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(stringBuilder.ToString());
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebResponse.ContentEncoding == "gzip"
                ? new GZipStream(httpWebResponse.GetResponseStream(), CompressionMode.Decompress)
                : httpWebResponse.GetResponseStream();
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
            foreach (var item in dic)
            {
                if (i > 0)

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
            return result;
        }

        public static string Get(string url)
        {
            string result = @"";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
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
            return result;
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string url, Dictionary<string, string> dic)
        {
            string result = @"";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(url);
            if (dic.Count > 0)
            {
                stringBuilder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        stringBuilder.Append("&");
                    stringBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }
            try
            {
                HttpResponseMessage httpResponse = await client.GetAsync(stringBuilder.ToString());
                httpResponse.EnsureSuccessStatusCode();
                var resType = httpResponse.Content.Headers.ContentEncoding.ToString();
                if (resType == "gzip")
                {
                    GZipInputStream inputStream = new GZipInputStream(await httpResponse.Content.ReadAsStreamAsync());
                    result = new StreamReader(inputStream).ReadToEnd();
                    return result;
                }
                result = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                log.Debug($"网络异常--" + e.Message);
                return result;
            }
            return result;
        }

        public async Task<string> PostAsync(string url, Dictionary<string, string> dic)
        {
            string result = @"";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(url);
            if (dic.Count > 0)
            {
                stringBuilder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        stringBuilder.Append("&");
                    stringBuilder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }
            try
            {
                HttpContent content = new StringContent(stringBuilder.ToString());
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            catch (Exception e)
            {
                log.Debug($"网络异常--{e.StackTrace}" + e.Message);
            }
            return result;
        }
    }
}
