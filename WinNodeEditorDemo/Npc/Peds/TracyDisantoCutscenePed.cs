using ST.Library.UI.NodeEditor;
using WinNodeEditorDemo.Core;

namespace WinNodeEditorDemo.Npc.Peds
{
    [STNode("/NPC/Peds/")]
    internal class TracyDisantoCutscenePed : STNode
    {
        private STNodeOption _pedOutput = null;
        private readonly PedHash pedHash = PedHash.TracyDisantoCutscene;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "TracyDisantoCutscene";
            _pedOutput = OutputOptions.Add("Ped", typeof(PedHash), false);
            _pedOutput.Data = pedHash;

        }
    }
}
