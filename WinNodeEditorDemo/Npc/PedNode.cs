using NodeLibrary;
using ST.Library.UI.NodeEditor;
using System;
using WinNodeEditorDemo.Core;
namespace WinNodeEditorDemo.Npc
{

    [STNode("NPC")]
    internal class PedNode : STNode
    {
        private STNodeOption _pedInput = null;
        private NpcNode _npc = null;

        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "NPC";
            AutoSize = false;
            Width = 160;
            Height = 80;
            _pedInput = InputOptions.Add("Ped", typeof(PedHash), true);
            _npc = new NpcNode(Guid.ToString(), 0);
            _pedInput.Connected += _pedInput_Connected;
        }

        private void _pedInput_Connected(object sender, STNodeOptionEventArgs e)
        {
            if (e.TargetOption.Data is PedHash hash)
            {
                _npc.Hash = (uint)hash;
            }
        }
    }
}
