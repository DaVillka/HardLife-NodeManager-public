using ST.Library.UI.NodeEditor;
using System;

namespace WinNodeEditorDemo.Instances
{
    internal class ScriptInstance
    {
        #region Singleton
        private static ScriptInstance _instance;
        public static ScriptInstance Instance
        {
            get
            {
                _instance ??= new ScriptInstance();
                return _instance;
            }
        }
        #endregion

        public void Initialize()
        {
            TabEditorInstance.OnCreateEditor += (STNodeEditor e) =>
            {
                Console.WriteLine("test");
            };
        }
    }
}
