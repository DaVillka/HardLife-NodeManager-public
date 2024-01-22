using ST.Library.UI.NodeEditor;
using System.Collections.Generic;
using System.Drawing;
using WinNodeEditorDemo.NodeControls;
using WinNodeEditorDemo.Nodes.Clothes.Components;

namespace WinNodeEditorDemo.Clothes
{
    [STNode("/Nodes/Clothes/", "Компонент одежды")]
    internal class Cloth : STNode
    {
        public string Name {  get; private set; }
        public string Description {  get; private set; }
        private STNodeOption strName = null;
        private STNodeOption strDescription = null;
        private STNodeOption _icon = null;


        private STNodeOption   _barefootIN = null;
        private STNodeOption      _socksIN = null;
        private STNodeOption      _shoesIN = null;
        private STNodeOption      _nakedIN = null;
        private STNodeOption  _underwearIN = null;
        private STNodeOption       _feetIN = null;
        private STNodeOption     _tnakedIN = null;
        private STNodeOption  _undersirtIN = null;
        private STNodeOption        _topIN = null;
        private STNodeOption     _glovesIN = null;

        private STNodeOption  _barefootOUT = null;
        private STNodeOption     _socksOUT = null;
        private STNodeOption     _shoesOUT = null;
        private STNodeOption     _nakedOUT = null;
        private STNodeOption _underwearOUT = null;
        private STNodeOption      _feetOUT = null;
        private STNodeOption    _tnakedOUT = null;
        private STNodeOption _undersirtOUT = null;
        private STNodeOption       _topOUT = null;
        private STNodeOption    _glovesOUT = null;


        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "Одежда";
            AutoSize = false;
            Width = 160;
            Height = 320 + 80;
            LetGetOptions = true;
            strName = InputOptions.Add("Имя", typeof(string), true);
            strDescription = InputOptions.Add("Описание", typeof(string), true);
            _icon = InputOptions.Add("Иконка", typeof(Image), true);
            
            strName.DataTransfer += (s, e) => { Name = e.TargetOption.Data as string; };
            strName.Connected += (s, e) => { Name = e.TargetOption.Data as string; };
            strName.DisConnected += (s, e) => { Name = null; };

            strDescription.DataTransfer += (s, e) => { Description = e.TargetOption.Data as string; };
            strDescription.Connected += (s, e) => { Description = e.TargetOption.Data as string; };
            strDescription.DisConnected += (s, e) => { Description = null; };

            _ = InputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);

            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(0, 60),
                Size = new System.Drawing.Size(Width, 20),
                BackColor = Color.Transparent,
                Alignment = StringAlignment.Center,
                Text = "---- Ниже пояса ----"
            });
            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(180 / 2, 80),
                Size = new System.Drawing.Size(60, 20),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Alignment = StringAlignment.Center,
                Text = "-Обувь-",
                Rotate = 90f
            });
            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(180 / 2, 80 + 60),
                Size = new System.Drawing.Size(60, 20),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Alignment = StringAlignment.Center,
                Text = "-Ноги-",
                Rotate = 90f
            });
            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(0, 140),
                Size = new System.Drawing.Size(160, 1),
                BackColor = Color.White,
                Alignment = StringAlignment.Center,
                Text = "",
            });

            _barefootIN = InputOptions.Add("Босый", typeof(Shoes), true);
            _socksIN = InputOptions.Add("Носки", typeof(Shoes), true);
            _shoesIN = InputOptions.Add("Обувь", typeof(Shoes), true);

            _nakedIN = InputOptions.Add("Голые", typeof(Legs), true);
            _underwearIN = InputOptions.Add("Белье", typeof(Legs), true);
            _feetIN = InputOptions.Add("Штаны", typeof(Legs), true);

            _barefootOUT = OutputOptions.Add("Босый", typeof(Shoes), false);
            _socksOUT = OutputOptions.Add("Носки", typeof(Shoes), false);
            _shoesOUT = OutputOptions.Add("Обувь", typeof(Shoes), false);

            _nakedOUT = OutputOptions.Add("Голые", typeof(Legs), false);
            _underwearOUT = OutputOptions.Add("Белье", typeof(Legs), false);
            _feetOUT = OutputOptions.Add("Штаны", typeof(Legs), false);

            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(0, 60 + 140),
                Size = new System.Drawing.Size(Width, 20),
                BackColor = Color.Transparent,
                Alignment = StringAlignment.Center,
                Text = "---- Выше пояса ----",
            });
            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(180 / 2, 80 + 60 + 80),
                Size = new System.Drawing.Size(80, 20),
                BackColor = Color.Black,
                ForeColor = Color.White,
                Alignment = StringAlignment.Center,
                Text = "-Тело-",
                Rotate = 90f
            });
            _ = Controls.Add(new STNodeLabel()
            {
                Location = new System.Drawing.Point(0, 140 + 160),
                Size = new System.Drawing.Size(160, 1),
                BackColor = Color.White,
                Alignment = StringAlignment.Center,
                Text = "",
            });
            _ = InputOptions.Add(STNodeOption.Empty);
            _tnakedIN = InputOptions.Add("Торс", typeof(Torso), true);
            _undersirtIN = InputOptions.Add("Майка", typeof(Tops), true);
            _topIN = InputOptions.Add("Топ", typeof(Tops), true);
            _glovesIN = InputOptions.Add("Перчи", typeof(Torso), true);
            _tnakedIN.Connected += (s, e) => { };
            _ = OutputOptions.Add(STNodeOption.Empty);

            _tnakedOUT = OutputOptions.Add("Торс", typeof(Torso), false);
            _undersirtOUT = OutputOptions.Add("Майка", typeof(Tops), false);
            _topOUT = OutputOptions.Add("Топ", typeof(Tops), false);
            _glovesOUT = OutputOptions.Add("Перчи", typeof(Torso), false);
        }
        public override object GetBuildObject()
        {
            var clothData = new Dictionary<string, dynamic>
            {
                { "GUID", Guid },
                { "Name", Name },
                { "Description", Description },
                //{ "Icon", _icon.Data as Image },
                { "Barefoot", GetData(_barefootIN) },
                { "Socks", GetData(_socksIN) },
                { "Shoes", GetData(_shoesIN) },
                { "Naked", GetData(_nakedIN) },
                { "Underwear", GetData(_underwearIN) },
                { "Feet", GetData(_feetIN) },
                { "Torso", GetData(_tnakedIN) },
                { "Undershirt",  GetData(_undersirtIN) },
                { "Top", GetData(_topIN) },
                { "Gloves",  GetData(_glovesIN) },
            };

            return clothData;
        }
        private object GetData(STNodeOption node)
        {
            if(node.ConnectionCount != 0)
                return node.GetConnectedOption(true)[0].Owner.GetBuildObject();
            return null;
        }
    }
}
