using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace HardLife_Options.Nodes.DataTypes
{
    [STNode("/Instance/Types/", "Скрипт")]

    internal class ScriptNode : STNode
    {
        private string _name;
        [STNodeProperty("Name", "введите имя скрипта")]
        public string ScriptName
        {
            get => _name;
            set
            {
                _name = value;
                Invalidate();
            }
        }
        public ScriptNode()
        {
            Title = "Script";
        }
        protected override void OnDrawOptionText(DrawingTools dt, STNodeOption op)
        {
            base.OnDrawOptionText(dt, op);
            dt.Graphics.DrawString(_name, Font, Brushes.White, op.TextRectangle, m_sf);
        }
    }
}
