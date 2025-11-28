using RAGEMPServerTool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RAGEMPServerTool
{
    public partial class MainForm : Form
    {
        #region Singleton
        private static MainForm instance = null;
        public static MainForm Instance { get => instance; }
        #endregion
        public MainForm()
        {
            InitializeComponent();
            instance = this;

            var scripts = Reflection.CreateInstances<Script>(Reflection.GetTypesImplementingClass(typeof(Script)));
            foreach (var item in scripts) item.Awake(this);
            foreach (var item in scripts) item.Start(this);
            foreach (var item in scripts) item.Finish(this);
            
        }

    }
}
internal static class Debug
{
    public static void Log(string message) 
    {
        MainForm.Instance.tDebug.AppendText(message);
        MainForm.Instance.tDebug.AppendText(Environment.NewLine);
    }
}