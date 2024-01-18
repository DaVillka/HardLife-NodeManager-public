using ST.Library.UI.NodeEditor;
using WinNodeEditorDemo.Core;

namespace WinNodeEditorDemo.Npc.Peds
{
    [STNode("/NPC/Peds/")]
    internal class MichaelPed : STNode
    {
        private STNodeOption _pedOutput = null;
        private readonly PedHash pedHash = PedHash.Michael;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "Michae";
            _pedOutput = OutputOptions.Add("Ped", typeof(PedHash), false);
            _pedOutput.Data = pedHash;
        }
    }
}
