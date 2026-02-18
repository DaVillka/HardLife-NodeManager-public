using ST.Library.UI.NodeEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using HardLife_Options.Core;
using HardLife_Options.Interfaces;
using HardLife_Options.NodeControls;

namespace HardLife_Options.Nodes.DataTypes
{
    [STNode("/Instance/Types", "Массив")]
    internal class ArrayNode : STNode
    {
        private enum LabelId
        {
            Type,
            DrawType,
            DrawLeft,
            DrawCenter,
            DrawRight,
        }
        //[STNodeProperty("Тип массива", "Указывается тип который будет использоватся в ноде")]
        public ArrayType ArrayType { get => _arrayType; set { _arrayType = value; OnChangeArrayType(); } }

        private STNodeOption _outputArray = null;
        private STNodeOption _output = null;
        private ArrayType _arrayType = ArrayType.Int;
        private STNodeInputDialog _inputDialog = null;
        private Dictionary<LabelId, STNodeLabel> _labels = null;
        private STNodeButton _button = null;
        private STNodeSelectEnumBox _selectEnumBox = null;

        private Dictionary<Type, object> _types = new()
            {
                { typeof(IArrayInt), new List<int> {} },
                { typeof(IArrayFloat), new List<float> {} },
                { typeof(IArrayString), new List<string> {} }
            };
        protected override void OnCreate()
        {
            base.OnCreate();
            Title = "Массив";
            AutoSize = false;
            Width = 100;
            Height = 130;
            _ = OutputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            _ = OutputOptions.Add(STNodeOption.Empty);
            _output = OutputOptions.Add("", typeof(ArrayNode), false);
            _output.Data = this;
            _outputArray = OutputOptions.Add("Массив", typeof(object), false);
            _labels = new()
            {
                {LabelId.DrawLeft, new()
                    {
                        Location = new System.Drawing.Point(10, 60),
                        Size = new System.Drawing.Size(Width-20, 20),
                        BackColor = Color.Transparent,
                        Alignment = StringAlignment.Near,
                        Text = "0"
                    }
                },
                {LabelId.DrawRight, new()
                    {
                        Location = new System.Drawing.Point(10, 60),
                        Size = new System.Drawing.Size(Width-20, 20),
                        BackColor = Color.Transparent,
                        Alignment = StringAlignment.Far,
                        Text = "0"
                    }
                },
                {LabelId.DrawCenter, new()
                    {
                        Location = new System.Drawing.Point(0, 60),
                        Size = new System.Drawing.Size(Width, 20),
                        BackColor = Color.Transparent,
                        Alignment = StringAlignment.Center,
                        Text = "-"
                    }
                },
            };
            _button = new STNodeButton("Нажмите чтобы ввести")
            {
                Location = new System.Drawing.Point(1, 21),
                Size = new System.Drawing.Size(Width - 2, 39),
                Alignment = StringAlignment.Center,
            };
            _selectEnumBox = new()
            {
                Enum = ArrayType,
                Location = new Point(0, 0),
                Size = new Size(Width, 20)
            };
            _selectEnumBox.ValueChanged += (sender, e) => ArrayType = (ArrayType)_selectEnumBox.Enum;

            _ = Controls.Add(_selectEnumBox);
            _ = Controls.Add(_button);

            foreach (KeyValuePair<LabelId, STNodeLabel> item in _labels)
            {
                _ = Controls.Add(item.Value);
            }

            ArrayType = ArrayType.Int;

            _inputDialog = new STNodeInputDialog();
            _ = Controls.Add(_inputDialog);

            _button.MouseClick += (sender, e) => _inputDialog?.Show(_arrayType == ArrayType.Int ? ConvertListToString((List<int>)_types[typeof(IArrayInt)]) : _arrayType == ArrayType.Float ? ConvertListToString((List<float>)_types[typeof(IArrayFloat)]) : ConvertListToString((List<string>)_types[typeof(IArrayString)]));
            _inputDialog.Result += DialogResult;
        }
        private string ConvertListToString<T>(List<T> list)
        {
            return string.Join(",", list);
        }
        protected override void OnLoadNode(Dictionary<string, byte[]> dic)
        {
            _types = Deserialize(dic["arrayData"]);
            _arrayType = (ArrayType)BitConverter.ToInt32(dic["arrayType"], 0);
            base.OnLoadNode(dic);
            OnChangeArrayType();
        }
        protected override void OnSaveNode(Dictionary<string, byte[]> dic)
        {
            dic.Add("arrayData", Serialize(_types));
            dic.Add("arrayType", BitConverter.GetBytes((int)_arrayType));
            base.OnSaveNode(dic);
        }

        private static byte[] Serialize(Dictionary<Type, object> data)
        {
            using MemoryStream stream = new();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            return stream.ToArray();
        }

        private static Dictionary<Type, object> Deserialize(byte[] serializedData)
        {
            using MemoryStream stream = new(serializedData);
            IFormatter formatter = new BinaryFormatter();
            return (Dictionary<Type, object>)formatter.Deserialize(stream);
        }
        private void DialogResult(string input)
        {
            string[] elements = input.Split(',');
            if (_arrayType == ArrayType.String) { ((List<string>)_types[typeof(IArrayString)]).Clear(); ((List<string>)_types[typeof(IArrayString)]).AddRange(elements); }
            else
            {
                if (_arrayType == ArrayType.Float)
                {
                    ((List<float>)_types[typeof(IArrayFloat)]).Clear();
                }

                if (_arrayType == ArrayType.Int)
                {
                    ((List<int>)_types[typeof(IArrayInt)]).Clear();
                }

                for (int i = 0; i < elements.Length; i++)
                {
                    if (_arrayType == ArrayType.Float)
                    {
                        elements[i] = Regex.Replace(elements[i], "[^0-9.]", "");
                        if (!string.IsNullOrWhiteSpace(elements[i]) && !elements[i].Contains("."))
                        {
                            elements[i] = elements[i] + ",0";
                            ((List<float>)_types[typeof(IArrayFloat)]).Add(float.Parse(elements[i]));
                        }
                    }
                    if (_arrayType == ArrayType.Int)
                    {
                        if (!string.IsNullOrWhiteSpace(elements[i]))
                        {
                            elements[i] = Regex.Replace(elements[i], "[^0-9]", "");
                            ((List<int>)_types[typeof(IArrayInt)]).Add(int.Parse(elements[i]));
                        }

                    }
                }
            }
            elements = elements.Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
            _labels[LabelId.DrawLeft].Text = elements[0];
            _labels[LabelId.DrawRight].Text = elements[elements.Length - 1];

            //string result = string.Join(", ", elements);
            //_button.Text = result;
            OnChangeArrayType();
        }

        private void OnChangeArrayType()
        {
            //_labels[LabelId.DrawType].Text = _arrayType.ToString();
            //if(_output.DataType)
            Type active = null;
            Type arrayType = null;
            switch (_arrayType)
            {
                case ArrayType.Int: { arrayType = typeof(List<int>); active = typeof(IArrayInt); break; }
                case ArrayType.String: { arrayType = typeof(List<string>); active = typeof(IArrayString); break; }
                case ArrayType.Float: { arrayType = typeof(List<float>); active = typeof(IArrayFloat); break; }
            }
            //TitleColor = TabEditorInstance.TypeColors[active];
            //if (_output.DataType != active) { _output.DisConnectionAll();  }
            List<object> list = GetListByArrayType(_arrayType);
            string left = list.Count != 0 ? list[0].ToString() : "0";
            string right = list.Count != 0 ? list[list.Count - 1].ToString(): "0";
            _labels[LabelId.DrawLeft].Text = left;
            _labels[LabelId.DrawRight].Text = right;
            _output.TransferData(this);

            if (_outputArray.DataType != arrayType)
            {
                _outputArray.DisConnectionAll();
            }

            _outputArray.DataType = arrayType;

            switch (_arrayType)
            {
                case ArrayType.Int: { _outputArray.TransferData(list.OfType<int>().ToList()); break; }
                case ArrayType.String: { _outputArray.TransferData(list.OfType<string>().ToList()); break; }
                case ArrayType.Float: { _outputArray.TransferData(list.OfType<float>().ToList()); break; }
            }

            Invalidate();
        }

        public List<object> GetListByArrayType(ArrayType arrayType)
        {
            Type targetType = GetTypeByArrayType(arrayType);

            // Проверка, есть ли указанный тип в словаре
            if (_types.TryGetValue(targetType, out object list))
            {
                // Приводим список к типу List<object>
                List<object> objectList = ((IEnumerable)list).Cast<object>().ToList();
                return objectList;
            }
            else
            {
                // Если указанного типа нет, возвращаем null или другое значение по умолчанию
                return null;
            }
        }

        private Type GetTypeByArrayType(ArrayType arrayType)
        {
            // Можете использовать switch или другой подход для определения типа по значению _arrayType
            return arrayType switch
            {
                ArrayType.Int => typeof(IArrayInt),
                ArrayType.Float => typeof(IArrayFloat),
                ArrayType.String => typeof(IArrayString),
                _ => throw new ArgumentException("Unsupported ArrayType"),
            };
        }
    }
}
