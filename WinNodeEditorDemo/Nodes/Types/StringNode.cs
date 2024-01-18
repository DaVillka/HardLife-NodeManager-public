using ST.Library.UI.NodeEditor;
using System.Collections.Generic;
using System.Drawing;
using WinNodeEditorDemo.Instances;
using WinNodeEditorDemo.NodeControls;

namespace WinNodeEditorDemo.DataTypes
{
    [STNode("/Nodes/Types/", "Строка")]
    internal class StringNode : STNode
    {
        private STNodeOption _stringOut = null;
        private STNodeButton _button = null;

        [STNodeProperty("Строка", "")]
        public string _string { get; set; }

        private STNodeInputDialog _inputText = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "String";
            AutoSize = false;
            Width = 100;
            Height = 80;
            _ = OutputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            TitleColor = TabEditorInstance.TypeColors[typeof(string)];
            _stringOut = new STNodeOption("Выход", typeof(string), false);
            _ = OutputOptions.Add(_stringOut);

            _button = new("Нажмите чтобы ввести")
            {
                Location = new Point(1, 1),
                Size = new Size(Width - 2, 40)
            };
            _button.MouseClick += (sender, e) => _inputText?.Show(_string);
            _ = Controls.Add(_button);

            _inputText = new STNodeInputDialog();
            _inputText.Result += (t) => { _button.Text = t; _string = t; };

            _stringOut.Connected += (s, e) => { _stringOut.TransferData(_string); };
        }
        protected override void OnLoadNode(Dictionary<string, byte[]> dic)
        {
            base.OnLoadNode(dic);
            if (dic.ContainsKey(nameof(_string)))
            {
                _button.Text = _string;
            }
        }
    }

}
