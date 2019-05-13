using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace 中文转义
{
    class BaiDuApi
    {
        private string appid = "xxx";
        private string secretkey = "xxx";
        private JsonSerializer serializer;
        public BaiDuApi()
        {
            serializer = new JsonSerializer();
        }
        public TranslateResponse translate(string q,string from,string to)
        {
            Random rd = new Random();
            string salt = rd.Next().ToString();
            Console.WriteLine(salt);
            string sign = generateSign(q,salt);
            string requestUrl = "http://api.fanyi.baidu.com/api/trans/vip/translate?q=" + HttpUtility.UrlEncode(q, System.Text.Encoding.UTF8) +
                  "&from=" + from +
                "&to=" + to +
                "&appid=" + appid +
                "&salt=" + salt +
                "&sign="+sign;
            string s= NetUtil.Get(requestUrl);
            TranslateResponse tr = JsonConvert.DeserializeObject<TranslateResponse>(s);
            if (tr.trans_result == null)
                return null;
            else
                return tr;
       
        }
        //salt 随机数
        private string generateSign(string q,string salt)
        {
            string source = appid + q + salt + secretkey;
            return MD5Encrypt32(source);
        }

        private  string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            pwd=BitConverter.ToString(s).Replace("-", "").ToLower();
            return pwd;
        }
    }
}
