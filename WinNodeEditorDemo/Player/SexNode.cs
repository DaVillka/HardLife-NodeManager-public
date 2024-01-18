using ST.Library.UI.NodeEditor;
using System.Collections.Generic;
using WinNodeEditorDemo.Core;

namespace WinNodeEditorDemo.Player
{
    [STNode("/Player/")]
    internal class SexNode : STNode
    {
        private Sex Sex { get; set; }
        //private STNodeSelectEnumBox e_sex = null;
        private STNodeOption n_sexOut = null;
        private STNodeOption n_sexMaleIn = null;
        private STNodeOption n_sexFemaleIn = null;

        private STNodeOption _maleOption = null;
        private STNodeOption _femaleOption = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "Пол";
            AutoSize = false;
            Width = 115;
            Height = 60;
            //e_sex = new STNodeSelectEnumBox();
            //e_sex.DisplayRectangle = new Rectangle(10, 21, 100, 18);
            //e_sex.Enum = Sex.Male;
            //e_sex.Location = new Point(10, 1);
            //this.Controls.Add(e_sex);

            //this.InputOptions.Add(STNodeOption.Empty);
            //this.OutputOptions.Add(STNodeOption.Empty);

            n_sexMaleIn = new("Мужской", typeof(object), true);
            n_sexFemaleIn = new("Женский", typeof(object), true);
            n_sexOut = new("", typeof(object), true);
            _ = OutputOptions.Add(n_sexOut);
            _ = InputOptions.Add(n_sexMaleIn);
            _ = InputOptions.Add(n_sexFemaleIn);

            n_sexOut.Connected += Output_Connected;
            n_sexOut.DisConnected += Output_DisConnected;
            n_sexMaleIn.Connected += Input_Connected;
            n_sexMaleIn.DisConnected += Input_DisConnected;
            n_sexFemaleIn.Connected += Input_Connected;
            n_sexFemaleIn.DisConnected += Input_DisConnected;
            //n_sexFemaleIn.DataTransfer += input_DataTransfer;
            //UpdateOptions();
            n_sexMaleIn.DataTransfer += (s, e) => { _maleOption = e.TargetOption; };
            n_sexFemaleIn.DataTransfer += (s, e) => { _femaleOption = e.TargetOption; };
        }

        private void Output_DisConnected(object sender, STNodeOptionEventArgs e)
        {
            STNodeOption op = sender as STNodeOption;
            if (op.ConnectionCount != 0) return;
            
            int nIndex = OutputOptions.IndexOf(op);
            if (InputOptions[nIndex].ConnectionCount != 0) return;
            
            InputOptions[nIndex].DataType = typeof(object);
            OutputOptions[nIndex].DataType = typeof(object);
        }

        private void Output_Connected(object sender, STNodeOptionEventArgs e)
        {
            STNodeOption op = sender as STNodeOption;
            int nIndex = OutputOptions.IndexOf(op);
            if (InputOptions[nIndex].DataType == typeof(object))
            {
                op.DataType = e.TargetOption.DataType;
                InputOptions[nIndex].DataType = op.DataType;
            }
        }

        private void Input_DisConnected(object sender, STNodeOptionEventArgs e)
        {
            if (n_sexMaleIn.ConnectionCount == 0 && n_sexFemaleIn.ConnectionCount == 0)
            {
                n_sexMaleIn.DisConnectionAll();
                n_sexFemaleIn.DisConnectionAll();
                n_sexOut.DisConnectionAll();
                n_sexMaleIn.DataType = typeof(object);
                n_sexFemaleIn.DataType = typeof(object);
                n_sexOut.DataType = typeof(object);
            }
        }
        private void Input_Connected(object sender, STNodeOptionEventArgs e)
        {
            if(sender is STNodeOption)
            {
                if(n_sexOut.DataType != e.TargetOption.DataType) n_sexOut.DataType = e.TargetOption.DataType;
                n_sexMaleIn.DataType = e.TargetOption.DataType;
                n_sexFemaleIn.DataType = e.TargetOption.DataType;
                
            }
        }
        public override object GetBuildObject()
        {
            return new Dictionary<string, object>() 
            {
                {"Male" , _maleOption.Owner.GetBuildObject() },
                {"Female" , _femaleOption.Owner.GetBuildObject() },
            };
        }
    }
}
