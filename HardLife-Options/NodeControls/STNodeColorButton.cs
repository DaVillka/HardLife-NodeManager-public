using ST.Library.UI.NodeEditor;
using System;
using System.Windows.Forms;

namespace HardLife_Options.NodeControls
{
    /// <summary>
    /// 此类仅演示 作为MixRGB节点的颜色选择按钮
    /// </summary>
    public class STNodeColorButton : STNodeControl
    {
        public event EventHandler ValueChanged;
        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);
            ColorDialog cd = new();
            if (cd.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //this._Color = cd.Color;
            BackColor = cd.Color;
            OnValueChanged(new EventArgs());
        }
    }
}
