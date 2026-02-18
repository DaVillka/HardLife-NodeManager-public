using ST.Library.UI.NodeEditor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HardLife_Options.NodeControls
{
    internal class STNodeInputDialog : STNodeControl
    {
        private readonly InputDialogForm dialogForm = null;
        public STNodeInputDialog()
        {
            Width = 0; Height = 0;
            dialogForm = new();
        }

        public Action<string> Result { get; set; } = null;
        internal void Show(string text = null)
        {
            dialogForm.InputText = text;
            if (dialogForm.ShowDialog() == DialogResult.OK)
            {
                Result?.Invoke(dialogForm.InputText);
            }
        }
        protected override void OnPaint(DrawingTools dt)
        {
        }
    }
    public class InputDialogForm : Form
    {
        private TextBox inputTextBox;
        private Button okButton;
        private Button cancelButton;
        public string InputText
        {
            get => inputTextBox.Text;
            set => inputTextBox.Text = value;
        }

        public InputDialogForm()
        {
            InitializeComponents();
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }
        private void InitializeComponents()
        {
            Size = new System.Drawing.Size(300, 134);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;


            inputTextBox = new TextBox
            {
                Location = new System.Drawing.Point(1, 1),
                Size = new System.Drawing.Size(298, 100),
                Multiline = true,
            };

            okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Size = new System.Drawing.Size(80, 25)
            };
            int buttonWidth = okButton.Width;
            int okButtonX = (Size.Width - (buttonWidth * 2)) / 3;
            int cancelButtonX = okButtonX + buttonWidth + okButtonX;
            okButton.Location = new System.Drawing.Point(okButtonX, 105);
            cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(cancelButtonX, 105),
                Size = new System.Drawing.Size(80, 25)
            };

            Controls.Add(inputTextBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            AcceptButton = okButton;
            CancelButton = cancelButton;
        }
    }
}
