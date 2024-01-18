using ST.Library.UI.NodeEditor;

namespace WinNodeEditorDemo
{
    [STNode("/")]
    public class EmptyOptionTestNode : STNode
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "EmptyTest";
            _ = InputOptions.Add(STNodeOption.Empty);
            _ = InputOptions.Add("string", typeof(string), false);
        }
    }
}
