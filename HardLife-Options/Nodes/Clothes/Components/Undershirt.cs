using ST.Library.UI.NodeEditor;
using System.Collections.Generic;


namespace HardLife_Options.Nodes.Clothes.Components
{
    [STNode("/Instance/Clothes/Components", "Undershirt")]
    internal class Undershirt : STNode
    {
        public int Id { get; private set; } = -1;
        public int Torso { get; private set; }

        private STNodeOption _id = null;
        private STNodeOption _torso = null;
        private STNodeOption _out = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 60;

            _id = InputOptions.Add("Id", typeof(int), true);
            _torso = InputOptions.Add("Торс", typeof(int), true);
            _out = OutputOptions.Add("Выход", GetType(), false);
            _out.TransferData(this);

            _id.Connected += (s, e) => { Id = (int)e.TargetOption.Data; _out.TransferData(this); };
            _id.DataTransfer += (s, e) => { Id = (int)e.TargetOption.Data; _out.TransferData(this); };
            _torso.Connected += (s, e) => { Torso = (int)e.TargetOption.Data; _out.TransferData(this); };
            _torso.DataTransfer += (s, e) => { Torso = (int)e.TargetOption.Data; _out.TransferData(this); };
            _id.DisConnected += (s, e) => { Id = -1; _out.TransferData(this); };
            _torso.DisConnected += (s, e) => { Torso = -1; _out.TransferData(this); };

        }
        public override object GetBuildObject()
        {
            return new Dictionary<string, object>()
            {
                { "Id", Id },
                { "Torso", Torso },
            };
        }
    }
}
