using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace 中文转义
{
    public class NetUtil
    {
        

        public static string Get(string url)
        {

            // 创建一个HTTP请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //request.Method="get";
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            StreamReader myreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            myreader.Close();

            return responseText;
           
        }


    }
}
