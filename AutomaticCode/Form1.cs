using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomaticCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string path3 = AppDomain.CurrentDomain.BaseDirectory + @"FileModel\";
            DataModel data = new DataModel();
            data.DataName = textBox1.Text;
            data.ClassName = textBox2.Text;
            data.IniDataBase();

            //String pageContent = data.TransformText();
            //System.IO.File.WriteAllText(path3 + data.ClassName + ".cs", pageContent);
            //MessageBox.Show("OK");

            string mainPath= AppDomain.CurrentDomain.BaseDirectory + data.ClassName+"Manage";
            //创建模块文件夹
            if (Directory.Exists(mainPath))
            {
                Directory.Delete(mainPath,true);
            }
            Directory.CreateDirectory(mainPath);
            Directory.CreateDirectory(mainPath+ @"\Common");
            Directory.CreateDirectory(mainPath + @"\DAL");
            Directory.CreateDirectory(mainPath + @"\Model");
            Directory.CreateDirectory(mainPath + @"\Properties");
            Directory.CreateDirectory(mainPath + @"\Resources");
            //拷贝通用文件
            // 拷贝文件
            DirectoryInfo sDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory+ @"Properties");
            FileInfo[] fileArray = sDir.GetFiles();
            foreach (FileInfo file in fileArray)
            {
                file.CopyTo(mainPath + @"\Properties" + "\\" + file.Name, true);
            }

            DirectoryInfo eDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + @"Resources");
            FileInfo[] files = eDir.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(mainPath + @"\Resources" + "\\" + file.Name, true);
            }
            //创建内容文件夹
            File.Copy(AppDomain.CurrentDomain.BaseDirectory + "App.config", mainPath+@"\App.config", true);
            //生成文件
            //实体
            String dataModel = data.TransformText();
            File.WriteAllText(mainPath + @"\Model\" + data.ClassName + ".cs", dataModel);
            //窗体
            MainWindows mwin = new MainWindows(data);
            string win = mwin.TransformText();
            //MSOPWindow.xaml
            File.WriteAllText(mainPath  +"\\"+ data.ClassName+ "ManageWindow.xaml", win);
            //交互代码
            MainCode mcode = new MainCode(data);
            string code = mcode.TransformText();
            File.WriteAllText(mainPath + "\\" + data.ClassName + "ManageWindow.xaml.cs", code);
            //模块
            ManageModule mm = new ManageModule(data);
            mm.ModuleId = Guid.NewGuid().ToString();
            string mstr = mm.TransformText();
            File.WriteAllText(mainPath + "\\" + data.ClassName + "ManageModule.cs", mstr);
            //解决方案
            ProjectFile projectF = new ProjectFile(data);
            string project = projectF.TransformText();
            File.WriteAllText(mainPath + "\\" + data.ClassName + "Manage.csproj", project);
            //特性
            AssemblyInfo ass = new AssemblyInfo(data);
            ass.ModuleId = mm.ModuleId;
            string assinfo = ass.TransformText();
            File.WriteAllText(mainPath + @"\Properties\AssemblyInfo.cs", assinfo);

            NotificationObject ob = new NotificationObject(data);
            string obstr = ob.TransformText();
            File.WriteAllText(mainPath + @"\Model\NotificationObject.cs", obstr);
            MessageBox.Show("OK");


        }
    }
}
