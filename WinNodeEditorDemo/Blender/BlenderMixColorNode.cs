using ST.Library.UI.NodeEditor;
using System.Drawing;
using WinNodeEditorDemo.NodeControls;
namespace WinNodeEditorDemo.Blender
{
    /// <summary>
    /// 此类仅仅是演示 并不包含颜色混合功能
    /// </summary>
    [STNode("/Blender/", "Crystal_lz", "2212233137@qq.com", "st233.com", "this is blender mixrgb node")]
    public class BlenderMixColorNode : STNode
    {
        private ColorMixType _MixType;
        [STNodeProperty("MixType", "This is MixType")]
        public ColorMixType MixType
        {
            get => _MixType;
            set
            {
                _MixType = value;
                m_ctrl_select.Enum = value; //当属性被赋值后 对应控件状态值也应当被修改
            }
        }

        private bool _Clamp;
        [STNodeProperty("Clamp", "This is Clamp")]
        public bool Clamp
        {
            get => _Clamp;
            set { _Clamp = value; m_ctrl_checkbox.Checked = value; }
        }

        private int _Fac = 50;
        [STNodeProperty("Fac", "This is Fac")]
        public int Fac
        {
            get => _Fac;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                if (value > 100)
                {
                    value = 100;
                }

                _Fac = value; m_ctrl_progess.Value = value;
            }
        }

        private Color _Color1 = Color.LightGray;//默认的DescriptorType不支持颜色的显示 需要扩展
        [STNodeProperty("Color1", "This is color1", DescriptorType = typeof(WinNodeEditorDemo.DescriptorForColor))]
        public Color Color1
        {
            get => _Color1;
            set { _Color1 = value; m_ctrl_btn_1.BackColor = value; }
        }

        private Color _Color2 = Color.LightGray;
        [STNodeProperty("Color2", "This is color2", DescriptorType = typeof(WinNodeEditorDemo.DescriptorForColor))]
        public Color Color2
        {
            get => _Color2;
            set { _Color2 = value; m_ctrl_btn_2.BackColor = value; }
        }

        public enum ColorMixType
        {
            Mix,
            Value,
            Color,
            Hue,
            Add,
            Subtract
        }

        private STNodeSelectEnumBox m_ctrl_select;  //自定义控件
        private STNodeProgress m_ctrl_progess;
        private STNodeCheckBox m_ctrl_checkbox;
        private STNodeColorButton m_ctrl_btn_1;
        private STNodeColorButton m_ctrl_btn_2;

        protected override void OnCreate()
        {
            base.OnCreate();
            TitleColor = Color.FromArgb(200, Color.DarkKhaki);
            Title = "MixRGB";
            AutoSize = false;
            Size = new Size(140, 142);

            _ = OutputOptions.Add("Color", typeof(Color), true);

            _ = InputOptions.Add(STNodeOption.Empty);  //空白节点 仅站位 不参与绘制与事件触发
            _ = InputOptions.Add(STNodeOption.Empty);
            _ = InputOptions.Add(STNodeOption.Empty);
            _ = InputOptions.Add("", typeof(float), true);
            _ = InputOptions.Add("Color1", typeof(Color), true);
            _ = InputOptions.Add("Color2", typeof(Color), true);

            m_ctrl_progess = new STNodeProgress
            {
                Text = "Fac",
                DisplayRectangle = new Rectangle(10, 61, 120, 18)
            };      //创建控件并添加到节点中
            m_ctrl_progess.ValueChanged += (s, e) => _Fac = m_ctrl_progess.Value;
            _ = Controls.Add(m_ctrl_progess);

            m_ctrl_checkbox = new STNodeCheckBox
            {
                Text = "Clamp",
                DisplayRectangle = new Rectangle(10, 40, 120, 20)
            };
            m_ctrl_checkbox.ValueChanged += (s, e) => _Clamp = m_ctrl_checkbox.Checked;
            _ = Controls.Add(m_ctrl_checkbox);

            m_ctrl_btn_1 = new STNodeColorButton
            {
                Text = "",
                BackColor = _Color1,
                DisplayRectangle = new Rectangle(80, 82, 50, 16)
            };
            m_ctrl_btn_1.ValueChanged += (s, e) => _Color1 = m_ctrl_btn_1.BackColor;
            _ = Controls.Add(m_ctrl_btn_1);

            m_ctrl_btn_2 = new STNodeColorButton
            {
                Text = "",
                BackColor = _Color2,
                DisplayRectangle = new Rectangle(80, 102, 50, 16)
            };
            m_ctrl_btn_2.ValueChanged += (s, e) => _Color2 = m_ctrl_btn_2.BackColor;
            _ = Controls.Add(m_ctrl_btn_2);

            m_ctrl_select = new STNodeSelectEnumBox
            {
                DisplayRectangle = new Rectangle(10, 21, 120, 18),
                Enum = _MixType
            };
            m_ctrl_select.ValueChanged += (s, e) => _MixType = (ColorMixType)m_ctrl_select.Enum;
            _ = Controls.Add(m_ctrl_select);
        }

        protected override void OnOwnerChanged()
        {  //当控件被添加时候 向编辑器提交自己的数据类型希望显示的颜色
            base.OnOwnerChanged();
            if (Owner == null)
            {
                return;
            }

            _ = Owner.SetTypeColor(typeof(float), Color.Gray);
            _ = Owner.SetTypeColor(typeof(Color), Color.Yellow);
        }
    }
}
