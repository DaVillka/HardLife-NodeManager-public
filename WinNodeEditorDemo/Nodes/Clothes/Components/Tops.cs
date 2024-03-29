﻿using ST.Library.UI.NodeEditor;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WinNodeEditorDemo.Interfaces;

namespace WinNodeEditorDemo.Nodes.Clothes.Components
{
    [STNode("/Nodes/Clothes/Components", "Tops")]
    internal class Tops : STNode
    {
        public int Id { get; private set; } = -1;
        public List<int> Textures { get; private set; } = new List<int>() { 0 };
        public int Hood { get; private set; } = -1;
        public int CTop { get; private set; } = -1;
        public Dictionary<STNodeOption, Undershirt> Undershirts { get; private set; } = new();

        private STNodeOption _id = null;
        private STNodeOption _textures = null;
        private STNodeOption _hood = null;
        private STNodeOption _top = null;//Убрана в рубашку
        private STNodeOption _undershirts = null;
        private STNodeOption _out = null;

        protected override void OnCreate()
        {
            base.OnCreate();
            Title = GetType().Name;
            AutoSize = false;
            Width = 120;
            Height = 120;

            _id = InputOptions.Add("Id", typeof(int), true);
            _textures = InputOptions.Add("Текстуры", typeof(List<int>), true);
            _hood = InputOptions.Add("Капюшон", typeof(int), true);
            _top = InputOptions.Add("Топ", typeof(int), true);
            _undershirts = InputOptions.Add("Майки", typeof(Undershirt), false);
            _out = OutputOptions.Add("Выход", GetType(), false);
            _out.TransferData(this);

            _id.DataTransfer += (s, e) => { Id = (int)e.TargetOption.Data; Invalidate(); };
            _textures.DataTransfer += (s, e) => { Textures = (List<int>)e.TargetOption.Data; Invalidate(); };
            _hood.DataTransfer += (s, e) => { Hood = (int)e.TargetOption.Data; Invalidate(); };
            _top.DataTransfer += (s, e) => { CTop = (int)e.TargetOption.Data; Invalidate(); };
            
            _undershirts.DataTransfer += (s, e) => { if (Undershirts.ContainsKey(e.TargetOption)) return; Undershirts.Add(e.TargetOption, (Undershirt)e.TargetOption.Data); Invalidate(); };

            _textures.DisConnected += (s, e) => { Textures = new List<int>() { 0 }; };
            _hood.DisConnected += (s, e) => { Hood = -1; };
            _top.DisConnected += (s, e) => { CTop = -1; };
            
            _undershirts.DisConnected += (s, e) => { Undershirts.Remove(e.TargetOption); };

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
                { "Hood", Hood },
                { "Top", CTop },
                { "Undershirts", Undershirts.Values.Select(t=>t.GetBuildObject()) },
            };
        }
    }
}
