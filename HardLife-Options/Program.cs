using RAGEMPServerTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardLife_Options
{
	internal static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			NodesForm nodesForm = new NodesForm();
			//MainForm mainForm = new MainForm();
			GridsForm gridsForm = new GridsForm();

			//mainForm.FormClosed += (s, e) => { nodesForm.Close(); gridsForm.Close(); };
			nodesForm.Show();
			gridsForm.Show();

			Application.Run(gridsForm);

			var scripts = Reflection.CreateInstances<Script>(Reflection.GetTypesImplementingClass(typeof(Script)));
			foreach (var item in scripts) item.Awake(null);
			foreach (var item in scripts) item.Start(null);
			foreach (var item in scripts) item.Finish(null);
		}
	}
}
