using ST.Library.UI.NodeEditor;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HardLife_Options.Nodes.Clothes.Components
{
    [STNode("/Instance/Clothes/Components", "Torso")]
    internal class Torso : STNode
    {
        public int Id { get; private set; } = -1;
        public int[] Gloves => _gloveIds.ToArray();

        private STNodeOption _id = null;
        //private STNodeOption _gloves = null;
        private STNodeOption _out = null;

        private List<int> _gloveIds = new();
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 40;

            _id = InputOptions.Add("Id", typeof(int), true);
            //_gloves = InputOptions.Add("Перчи", typeof(List<int>), true);
            _out = OutputOptions.Add("Выход", GetType(), false);
            _out.TransferData(this);

            _id.DataTransfer += (s, e) => { Id = (int)e.TargetOption.Data; Invalidate(); };

            //_gloves.DataTransfer += (s, e) => { _gloveIds = (List<int>)e.TargetOption.Data; };
            //_gloves.DisConnected += (s, e) => _gloveIds.Clear();
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
                //{ "Gloves", Gloves },
            };
        }
    }
}
