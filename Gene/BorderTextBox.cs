using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gene
{
    public class BorderedTextBox : UserControl
    {
        TextBox textBox;

        public BorderedTextBox()
        {
            textBox = new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(-1, -1),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };
            Control container = new ContainerControl()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(-1)
            };
            container.Controls.Add(textBox);
            this.Controls.Add(container);

            DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            //FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(183)))), ((int)(((byte)(83)))));
            BackColor = DefaultBorderColor;
            Padding = new Padding(1);
            Size = textBox.Size;
        }

        public Color DefaultBorderColor { get; set; }
        public Color FocusedBorderColor { get; set; }

        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        protected override void OnEnter(EventArgs e)
        {
            //BackColor = FocusedBorderColor;
            //base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            //BackColor = DefaultBorderColor;
            //base.OnLeave(e);
        }

        protected override void SetBoundsCore(int x, int y,
            int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, textBox.PreferredHeight, specified);
        }
    }


    class BigCheckBox : CheckBox
    {
        public BigCheckBox()
        {
            this.Text = "Approved";
            this.TextAlign = ContentAlignment.MiddleRight;
        }

        public override bool AutoSize
        {
            set { base.AutoSize = false; }
            get { return base.AutoSize; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.Height = 50;
            this.Width = 50;
            int squareSide = 25;

            Rectangle rect = new Rectangle(new Point(0, 1), new Size(squareSide, squareSide));

            ControlPaint.DrawCheckBox(e.Graphics, rect, this.Checked ? ButtonState.Checked : ButtonState.Normal);
        }
    }
}
