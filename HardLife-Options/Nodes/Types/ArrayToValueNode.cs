using ST.Library.UI.NodeEditor;
using System;
using System.Collections.Generic;
using HardLife_Options.Core;
using HardLife_Options.NodeControls;

namespace HardLife_Options.Nodes.DataTypes
{
    [STNode("/Instance/Types")]
    internal class ArrayToValueNode : STNode
    {
        private STNodeOption _arrayNode = null;
        private STNodeOption _valueNode = null;
        private ArrayNode _targetArray = null;
        private Enumerator _enumerator = null;

        private List<object> _activeList = null;
        private int _index = 0;

        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "Массив в элемент";
            AutoSize = false;
            Width = 140;
            Height = 80;
            _ = InputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            _arrayNode = InputOptions.Add("Массив", typeof(ArrayNode), false);
            _valueNode = OutputOptions.Add("Значение", typeof(object), false);

            _arrayNode.Connected += _arrayNode_Connected;
            _arrayNode.DataTransfer += _arrayNode_DataTransfer;
            _arrayNode.DisConnected += _arrayNode_DisConnected;
            _valueNode.Connected += _valueNode_Connected;

            _enumerator = new Enumerator(Controls)
            {
                Location = new System.Drawing.Point(0, 0),
                Size = new System.Drawing.Size(140, 20)
            };
            _enumerator.ButtonClick += ButtonClick;
        }
        protected override void OnSaveNode(Dictionary<string, byte[]> dic)
        {
            dic.Add("index", BitConverter.GetBytes(_index));
            base.OnSaveNode(dic);
        }
        protected override void OnLoadNode(Dictionary<string, byte[]> dic)
        {
            _index = BitConverter.ToInt32(dic["index"], 0);
            base.OnLoadNode(dic);
        }

        private void ButtonClick(EnumeratorButton button)
        {
            if (_targetArray == null || _activeList == null || _activeList.Count == 0)
            {
                return;
            }

            if (button == EnumeratorButton.Right)
            {
                _index++;
            }

            if (button == EnumeratorButton.Left)
            {
                _index--;
            }

            if (_index < 0)
            {
                _index = 0;
            }

            if (_index > _activeList.Count - 1)
            {
                _index = _activeList.Count - 1;
            }
            //_enumerator.Text = _activeList[_index].ToString();
            Update();
        }

        private void _arrayNode_DataTransfer(object sender, STNodeOptionEventArgs e)
        {
            _targetArray = e.TargetOption.Data as ArrayNode;
            if (_targetArray == null)
            {
                return;
            }

            Update();
        }

        private void _arrayNode_Connected(object sender, STNodeOptionEventArgs e)
        {
            _targetArray = e.TargetOption.Data as ArrayNode;
            if (_targetArray == null)
            {
                return;
            }

            Update();
        }
        private void Update()
        {
            Type _valueType = null;
            switch (_targetArray.ArrayType)
            {
                case ArrayType.Int: _valueType = typeof(int); break;
                case ArrayType.Float: _valueType = typeof(float); break;
                case ArrayType.String: _valueType = typeof(string); break;
            }
            if (_valueNode.DataType != _valueType && _valueType != typeof(object))
            {
                _valueNode.DisConnectionAll();
            }

            _valueNode.DataType = _valueType;
            _activeList = _targetArray.GetListByArrayType(_targetArray.ArrayType);
            _ = (int)_activeList[0];
            if (_activeList.Count > 0)
            {
                _ = _activeList[_index].GetType(); _enumerator.Text = _activeList[_index].ToString(); _valueNode.TransferData((int)_activeList[_index]);
            }
            else { _enumerator.Text = "<>"; _valueNode.Data = null; _valueNode.TransferData(null); }

        }
        private void _arrayNode_DisConnected(object sender, STNodeOptionEventArgs e)
        {
            _valueNode.DataType = typeof(object);
        }

        private void _valueNode_Connected(object sender, STNodeOptionEventArgs e)
        {
        }
    }
}
