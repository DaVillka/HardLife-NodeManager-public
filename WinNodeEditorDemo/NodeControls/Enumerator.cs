using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;

namespace WinNodeEditorDemo.NodeControls
{
    internal enum EnumeratorButton
    {
        Left,
        Right,
    }
    internal class Enumerator : STNodeControl
    {
        public Action<EnumeratorButton> ButtonClick;
        private readonly STNodeButton _left = null;
        private readonly STNodeButton _right = null;
        private readonly STNodeLabel _label = null;
        public new Size Size
        {
            get => base.Size;
            set
            {
                base.Size = value;
                _left.Size = new System.Drawing.Size(base.Size.Height, base.Size.Height);
                _right.Size = new System.Drawing.Size(base.Size.Height, base.Size.Height);
                _left.Location = new System.Drawing.Point(base.Location.X + 0, base.Location.Y + 0);
                _right.Location = new System.Drawing.Point(base.Location.X + base.Width - 20, base.Location.Y + 0);
            }
        }
        public new Point Location
        {
            get => base.Location;
            set
            {
                base.Location = value;
                _left.Location = new System.Drawing.Point(base.Location.X + 0, base.Location.Y + 0);
                _right.Location = new System.Drawing.Point(base.Location.X + base.Width - 20, base.Location.Y + 0);
            }
        }

        public Enumerator(STNodeControlCollection node)
        {
            _left = new STNodeButton("<")
            {
                Location = new System.Drawing.Point(base.Location.X + 0, base.Location.Y + 0),
                Size = new System.Drawing.Size(base.Size.Height, base.Size.Height)
            };
            _right = new STNodeButton(">")
            {
                Location = new System.Drawing.Point(Width - 20, 0),
                Size = new System.Drawing.Size(base.Size.Height, base.Size.Height)
            };
            _ = node.Add(this);
            _ = node.Add(_left);
            _ = node.Add(_right);
            BackColor = Color.FromArgb(80, BackColor);
            Text = "<>";
            _left.MouseClick += _button_MouseClick;
            _right.MouseClick += _button_MouseClick;
            base.Enabled = false;
        }

        private void _button_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (sender is STNodeButton b)
            {
                if (b == _left)
                {
                    ButtonClick?.Invoke(EnumeratorButton.Left);
                }

                if (b == _right)
                {
                    ButtonClick?.Invoke(EnumeratorButton.Right);
                }
            }
        }
    }
}
