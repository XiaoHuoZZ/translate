using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 中文转义
{
    
    public partial class Form1 : Form
    {
       
        private int funcationState;  //0 为转义  1为翻译
        private Language[] lg_list = {new Language("英语","en"), new Language("日语", "jp"), new Language("韩语", "kor"),
            new Language("法语", "fra"),new Language("西班牙语","spa"),new Language("越南语","vie"),new Language("德语","de"),
            new Language("俄语","ru"),new Language("文言文","wyw")};
       
        public Form1()
        {
    
            InitializeComponent();
            funcationState = 0;
            menuItem1.Checked = true;
            menuItem2.Checked = false;
            ComboBoxBindData();
            comboBox1.SelectedIndex = 0;

        }

        private void ComboBoxBindData()
        {
            for (int i = 0; i < lg_list.Length; i++)
            {
                comboBox1.Items.Add(lg_list[i].fullName);
            }
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            BaiDuApi api = new BaiDuApi();
            string source = source_tb.Text;
            if (!source.Equals(""))
            {

                TranslateResponse tr = api.translate(source, "zh", lg_list[comboBox1.SelectedIndex].shortName);
                if (funcationState == 0)
                {
                    string tempLanguage = lg_list[comboBox1.SelectedIndex].shortName;//中间语言
                    if (tr != null)
                    {
                        string temp = assembleString(tr);
                        tr = api.translate(temp, tempLanguage, "zh");
                        if (tr != null)
                            result_tb.Text = assembleString(tr);
                        else
                            MessageBox.Show("出问题了");
                    }
                    else
                        MessageBox.Show("出问题了");
                }
                else
                {
                    if (tr != null)
                        result_tb.Text = assembleString(tr);
                    else
                        MessageBox.Show("出问题了");
                }
            }
        }

        private string assembleString(TranslateResponse tr)
        {
            List<TranslateResult> list = tr.trans_result;
            string s = "";
            int count = list.Count;
            foreach (TranslateResult res in list)
            {
                if (count == 1)
                    s = s + res.dst;
                else
                    s = s + res.dst + "\r\n";
                count--;
            }
            return s;
        }

        private void MenuItem1_Click(object sender, EventArgs e)
        {
            funcationState = 0;
            menuItem1.Checked = true;
            menuItem2.Checked = false;
        }

        private void MenuItem2_Click(object sender, EventArgs e)
        {
            funcationState = 1;
            menuItem1.Checked = false;
            menuItem2.Checked = true;
        }

       
    }
}
