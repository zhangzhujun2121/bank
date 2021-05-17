using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer2.Enabled= true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private const int N = 10005;
        static public int n, m;//进程数、资源数
        static public int[,] Max, Need, Allocation; //最大需求，剩余需求，已经分配
        static public int[] Work, Finish, Avaliable;    //动态显示剩余资源 表示安全 获取剩余资源
        static public ArrayList result = new ArrayList();

        string str1;
        string str2;

        private void button1_Click(object sender, EventArgs e)  //初始化获取值开始计算
        {
            str1 = textBox1.Text;
            str2 = textBox2.Text;
            n = Convert.ToInt32(str1);
            m = Convert.ToInt32(str2);

            Max = new int[n, m];
            Need = new int[n, m];
            Allocation = new int[n, m];
            Avaliable = new int[m];
            result.Clear();
            richTextBox3.Clear();
            xi = 0;
            xt = 0;
            flag = 0;
            textBox5.Text = "";

            button2.Enabled = true;
            button3.Enabled = true;
            for (int i=0;i<n;i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Allocation[i, j] = int.Parse(dataGridView1.Rows[i].Cells[j+1].Value.ToString());
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Max[i, j] = int.Parse(dataGridView2.Rows[i].Cells[j+1].Value.ToString());
                }
            }
            for (int i = 0; i < m; i++)
            {
                Avaliable[i] = int.Parse(dataGridView3.Rows[0].Cells[i].Value.ToString());
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Need[i,j] = Max[i,j] - Allocation[i,j];
                }
            }
            Work = new int[m];
            for (int i = 0; i < m; i++)
            {
                Work[i] = Avaliable[i];
            }
            Finish = new int[n];
            for (int i = 0; i < n; i++)
            {
                Finish[i] = 0;
            }

        }

        private void button2_Click(object sender, EventArgs e) //自动执行按钮
        {
            if(button2.Text=="自动执行")
            {
                timer1.Enabled = true;
                button2.Text = "暂停";
            }
            else
            {
                timer1.Enabled = false;
                button2.Text = "自动执行";
            }
        }
 

        private void timer1_Tick(object sender, EventArgs e) //计时器
        {
            button3_Click(sender,e);
            timer1.Interval=500;
        }
  

        private void button4_Click(object sender, EventArgs e) //测试
        {
            dataGridView1.Rows[0].Cells[1].Value=1;
            dataGridView1.Rows[0].Cells[2].Value = 0;
            dataGridView1.Rows[0].Cells[3].Value = 0;

            dataGridView1.Rows[1].Cells[1].Value = 5;
            dataGridView1.Rows[1].Cells[2].Value = 1;
            dataGridView1.Rows[1].Cells[3].Value = 1;

            dataGridView1.Rows[2].Cells[1].Value = 2;
            dataGridView1.Rows[2].Cells[2].Value = 1;
            dataGridView1.Rows[2].Cells[3].Value = 1;

            dataGridView1.Rows[3].Cells[1].Value = 0;
            dataGridView1.Rows[3].Cells[2].Value = 2;
            dataGridView1.Rows[3].Cells[3].Value = 2;

            dataGridView2.Rows[0].Cells[1].Value = 3;
            dataGridView2.Rows[0].Cells[2].Value = 2;
            dataGridView2.Rows[0].Cells[3].Value = 2;

            dataGridView2.Rows[1].Cells[1].Value = 6;
            dataGridView2.Rows[1].Cells[2].Value = 1;
            dataGridView2.Rows[1].Cells[3].Value = 3;

            dataGridView2.Rows[2].Cells[1].Value = 3;
            dataGridView2.Rows[2].Cells[2].Value = 1;
            dataGridView2.Rows[2].Cells[3].Value = 4;

            dataGridView2.Rows[3].Cells[1].Value = 4;
            dataGridView2.Rows[3].Cells[2].Value = 2;
            dataGridView2.Rows[3].Cells[3].Value = 2;


            dataGridView3.Rows[0].Cells[0].Value = 1;
            dataGridView3.Rows[0].Cells[1].Value = 1;
            dataGridView3.Rows[0].Cells[2].Value = 2;
        }
        private void button6_Click(object sender, EventArgs e)  //得到表
        {
            button2.Enabled = false;
            button3.Enabled = false;
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            dataGridView3.Columns.Clear();
            dataGridView1.Columns.Add("", "");
            for (int i = 0; i <int.Parse(textBox2.Text);i++)
            {
                dataGridView1.Columns.Add(string.Format("资源{0}",(char)('a' + i)), string.Format("资源{0}", (char)('a' + i)));
            }
            for (int i = 0; i < int.Parse(textBox1.Text); i++)
            {

                dataGridView1.Rows.Add(string.Format("进程{0}", (char)('A' + i)));
            }

            dataGridView2.Columns.Add("", "");
            for (int i = 0; i < int.Parse(textBox2.Text); i++)
            {
                dataGridView2.Columns.Add(string.Format("资源{0}", (char)('a' + i)), string.Format("资源{0}", (char)('a' + i)));
            }
            for (int i = 0; i < int.Parse(textBox1.Text); i++)
            {

                dataGridView2.Rows.Add(string.Format("进程{0}", (char)('A' + i)));
            }

            for (int i = 0; i < int.Parse(textBox2.Text); i++)
            {
                dataGridView3.Columns.Add(string.Format("资源{0}", (char)('a' + i)), string.Format("资源{0}", (char)('a' + i)));
            }
        }

        private void timer2_Tick(object sender, EventArgs e) //时间计时器
        {
            toolStripStatusLabel1.Text= string.Format("当前时间:{0}", DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
        }

        int xi = 0;
        int xt = 0;
        int flag = 0;
        private void button3_Click(object sender, EventArgs e) //单步执行
        {


            if (flag == 0)
            {
                if (Finish[xi] == 0) //刚开始不满足安全序列
                {
                    richTextBox3.Text += "进程" + (char)(xi + 'A') + "申请资源\n";
                    int ans = 0;    //满足资源数
                    for (int j = 0; j < m; j++)
                    {
                        if (Need[xi, j] <= Work[j])  //比较资源是否满足
                        {
                            ans++;
                        }
                    }
                    richTextBox3.Text += "需要资源数：（";
                    for (int j = 0; j < m; j++)
                    {
                        richTextBox3.Text += Need[xi, j]+" ";
                    }
                    richTextBox3.Text += ")\n";
                    richTextBox3.Text += "剩余资源数：（";
                    for (int j = 0; j < m; j++)
                    {
                        richTextBox3.Text += Work[j] + " ";
                    }
                    richTextBox3.Text += ")\n";
                    if (ans == m)
                    {  // 满足安全序列条件
                        for (int j = 0; j < m; j++)
                        {
                            Work[j] += Allocation[xi, j]; //释放资源
                        }
                        Finish[xi] = 1;  //满足安全序列
                        richTextBox3.Text += "进程" + (char)(xi + 'A') + "申请资源成功！！！ 释放资源\n\n";
                        result.Add(xi);
                        xt++;
                        xi = 0;
                        return;
                    }
                    richTextBox3.Text += "进程" + (char)(xi + 'A') + "申请资源失败！\n\n";
                }
                xi++;
                if (xi == n)
                {
                    xi = 0;
                    xt++;
                }
            }
            if(xt==n&&flag==0)
            {
                flag = 1;
                for (int i = 0; i < n; i++)
                {
                    if (Finish[i] == 0)
                    {
                        MessageBox.Show("系统处于不安全状态");
                        if (button2.Text == "暂停")
                        {
                            button2_Click(sender, e);
                        }
                        return;
                    }
                }
                string str;
                string tmp;
                str = "";
                foreach (int x in result)
                {
                    tmp = Convert.ToString((char)(x + 'A'));
                    str += tmp;
                }
                textBox5.Text = str;
                if(button2.Text=="暂停")
                {
                    button2_Click(sender, e);
                }
                MessageBox.Show("系统处于安全状态");
            }
            richTextBox3.Select(richTextBox3.Text.Length, 0);
            richTextBox3.ScrollToCaret();
        }
        public bool Check()    //算法
        {
            for (int tmp = 0; tmp < n; tmp++) //找n次
            {
                for (int i = 0; i < n; i++)     //直到找到一个满足安全序列条件的
                {
                    if (Finish[i] == 0) //刚开始不满足安全序列
                    {
                        int ans = 0;    //满足资源数
                        for (int j = 0; j < m; j++)
                        {
                            if (Need[i, j] <= Work[j])  //比较资源是否满足
                            {
                                ans++;
                            }
                        }
                        if (ans == m)
                        {  // 满足安全序列条件
                            for (int j = 0; j < m; j++)
                            {
                                Work[j] += Allocation[i, j]; //释放资源
                            }
                            Finish[i] = 1;  //满足安全序列
                            result.Add(i);
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (Finish[i] == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
