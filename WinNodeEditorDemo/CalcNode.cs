using ST.Library.UI.NodeEditor;
using System.Drawing;

namespace WinNodeEditorDemo
{
    /// <summary>
    /// 此节点仅演示UI自定义以及控件 并不包含功能
    /// </summary>
    [STNode("/", "DebugST", "2212233137@qq.com", "st233.com", "此节点仅演示UI自定义以及控件,并不包含功能.")]
    public class CalcNode : STNode
    {
        private readonly StringFormat m_f;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center
            };
            Title = "Calculator";
            AutoSize = false;          //注意需要先设置AutoSize=false 才能够进行大小设置
            Size = new Size(218, 308);

            STNodeControl ctrl = new()
            {
                Text = "",                 //此控件为显示屏幕
                Location = new Point(13, 31),
                Size = new Size(190, 50)
            };
            _ = Controls.Add(ctrl);

            ctrl.Paint += (s, e) =>
            {
                m_sf.Alignment = StringAlignment.Far;
                STNodeControl c = s as STNodeControl;
                Graphics g = e.DrawingTools.Graphics;
                g.DrawString("0", ctrl.Font, Brushes.White, c.ClientRectangle, m_sf);
            };

            string[] strs = { //按钮文本
                                "MC", "MR", "MS", "M+",
                                "M-", "←",  "CE", "C", "+", "√",
                                "7",  "8",  "9",  "/", "%",
                                "4",  "5",  "6",  "*", "1/x",
                                "1",  "2",  "3",  "-", "=",
                                "0",  " ",  ".",  "+" };
            Point p = new(13, 86);
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] == " ")
                {
                    continue;
                }

                ctrl = new STNodeControl
                {
                    Text = strs[i],
                    Size = new Size(34, 27),
                    Left = 13 + (i % 5 * 39),
                    Top = 86 + (i / 5 * 32)
                };
                if (ctrl.Text == "=")
                {
                    ctrl.Height = 59;
                }

                if (ctrl.Text == "0")
                {
                    ctrl.Width = 73;
                }

                _ = Controls.Add(ctrl);
                if (i == 8)
                {
                    ctrl.Paint += (s, e) =>
                    {
                        m_sf.Alignment = StringAlignment.Center;
                        STNodeControl c = s as STNodeControl;
                        Graphics g = e.DrawingTools.Graphics;
                        g.DrawString("_", ctrl.Font, Brushes.White, c.ClientRectangle, m_sf);
                    };
                }

                ctrl.MouseClick += (s, e) => System.Windows.Forms.MessageBox.Show(((STNodeControl)s).Text);
            }

            _ = OutputOptions.Add("Result", typeof(int), false);
        }
    }
}
