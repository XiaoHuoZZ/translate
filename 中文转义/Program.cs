using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 中文转义
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //反射
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.Run(new Form1());
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, EventArgs e)
        {
            //从资源中加载DLL到内存
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("中文转义.dll.Newtonsoft.Json.dll"))
            {
                byte[] byData = new byte[stream.Length];
                stream.Read(byData, 0, byData.Length);
                return Assembly.Load(byData);
            }
        }

    }
}
