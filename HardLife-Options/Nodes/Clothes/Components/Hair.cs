using ST.Library.UI.NodeEditor;
using System.Collections.Generic;
using System.Drawing;

namespace HardLife_Options.Nodes.Clothes.Components
{
    [STNode("/Instance/Clothes/Components", "Hair")]
    internal class Hair : STNode
    {
        public int Id { get; private set; } = -1;
        public List<int> Textures { get; private set; } = new List<int>() { 0 };

        private STNodeOption _id = null;
        private STNodeOption _textures = null;
        private STNodeOption _out = null;

        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 60;

            _id = InputOptions.Add("Id", typeof(int), true);
            _textures = InputOptions.Add("Текстуры", typeof(List<int>), true);
            _out = OutputOptions.Add("Выход", GetType(), false);
            _out.TransferData(this);

            _id.DataTransfer += (s, e) => { Id = (int)e.TargetOption.Data; Invalidate(); };
            _textures.DataTransfer += (s, e) => { Textures = (List<int>)e.TargetOption.Data; Invalidate(); };
            _textures.DisConnected += (s, e) => { Textures = new List<int>() { 0 }; };

        }
        protected override void OnDrawOptionText(DrawingTools dt, STNodeOption op)
        {
            if (op == _id) { m_sf.Alignment = StringAlignment.Near; dt.Graphics.DrawString("Id: " + Id.ToString(), Font, Brushes.White, op.TextRectangle, m_sf); return; }
            base.OnDrawOptionText(dt, op);
        }
        public override object GetBuildObject()
        {
            return new Dictionary<string, object>()
            {
                { "Id", Id },
                { "Textures", Textures },
            };
        }
    }
}
