using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinNodeEditorDemo.Core;

namespace WinNodeEditorDemo.Clothes
{
    [STNode("/Player/", "Пол")]
    internal class SexNode : STNode
    {
        private STNodeOption _inMale = null;
        private STNodeOption _outFemale = null;
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "Пол";
            AutoSize = false;
            Width = 125;
            Height = 40;

            _inMale = InputOptions.Add("Мужской", typeof(Sex), true);
            _outFemale = OutputOptions.Add("Женский", typeof(Sex), true);
            _inMale.TransferData(Sex.MALE);
            _outFemale.TransferData(Sex.FEMALE);

            //_inMale.Connected += (s, e) => { Description = e.TargetOption.Data as string; };
        }
    }
}
