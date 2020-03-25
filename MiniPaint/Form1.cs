using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniPaint
{
    public partial class Form1 : Form
    {
        string path;
        Graphics graphics;
        Point tempPoint;
        Pen myPen;
        SolidBrush myBrush;
        public Form1()
        {
            InitializeComponent();
            openFileDialog.Filter = saveFileDialog.Filter = "Grafika BMP|*.bmp|Grafika PNG|*.png|Grafika JPG|*.jpg";
            myPen = new Pen(buttonColor.BackColor, (float)numericUpDownWidth.Value);
            myPen.EndCap = myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            myBrush = new SolidBrush(buttonFillColor.BackColor);

            nowyToolStripMenuItem_Click(null, null);
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxMyImage.Image = new Bitmap(800, 600);
            graphics = Graphics.FromImage(pictureBoxMyImage.Image);
            graphics.Clear(Color.White);
            path = "";
        }

        private void otwórzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBoxMyImage.Image = Image.FromFile(openFileDialog.FileName);
                graphics = Graphics.FromImage(pictureBoxMyImage.Image);
                path = openFileDialog.FileName;
                Text = path;
            }
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);
                ImageFormat imageFormat = ImageFormat.Bmp;
                switch (extension)
                {
                    case ".bmp":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                        imageFormat = ImageFormat.Png;
                        break;
                    case ".jpg":
                        imageFormat = ImageFormat.Jpeg;
                        break;
                }
                pictureBoxMyImage.Image.Save(saveFileDialog.FileName, imageFormat);
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (path != "")
            {
                string extension = Path.GetExtension(saveFileDialog.FileName);
            }
            else
            {
                zapiszJakoToolStripMenuItem_Click(null, null);
            }
        }

        private void pictureBoxMyImage_MouseDown(object sender, MouseEventArgs e)
        {
           if(e.Button == MouseButtons.Left)
            {
                tempPoint = e.Location;
            }
        }

        private void pictureBoxMyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                    pictureBoxMyImage.Refresh();
                    tempPoint = e.Location;
                }
            }
        }

        private void pictureBoxMyImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonCurve.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }
                else if(radioButtonLine.Checked)
                {
                    graphics.DrawLine(myPen, tempPoint, e.Location);
                }
                else if (radioButtonRectangle.Checked)
                {
                    graphics.DrawRectangle(myPen,
                                        Math.Min(tempPoint.X, e.X),
                                        Math.Min(tempPoint.Y, e.Y),
                                        Math.Abs(tempPoint.X - e.X),
                                        Math.Abs(tempPoint.Y - e.Y));
                    if(radioButtonFill.Checked)
                    {
                        graphics.FillRectangle(myBrush,
                                        Math.Min(tempPoint.X, e.X),
                                        Math.Min(tempPoint.Y, e.Y),
                                        Math.Abs(tempPoint.X - e.X),
                                        Math.Abs(tempPoint.Y - e.Y));
                    }
                }
                else if (radioButtonElipse.Checked)
                {
                    graphics.DrawEllipse(myPen, 
                                        Math.Min(tempPoint.X, e.X),
                                        Math.Min(tempPoint.Y, e.Y),
                                        Math.Abs(tempPoint.X - e.X),
                                        Math.Abs(tempPoint.Y - e.Y));
                    if (radioButtonFill.Checked)
                    {
                        graphics.FillEllipse(myBrush,
                                        Math.Min(tempPoint.X, e.X),
                                        Math.Min(tempPoint.Y, e.Y),
                                        Math.Abs(tempPoint.X - e.X),
                                        Math.Abs(tempPoint.Y - e.Y));
                    }
                }
                pictureBoxMyImage.Refresh();
            }
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            myPen.Width = (float)numericUpDownWidth.Value;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonColor.BackColor = colorDialog.Color;
                myPen.Color = colorDialog.Color;
            }
        }

        private void buttonFillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                buttonFillColor.BackColor = colorDialog.Color;
                myBrush.Color = colorDialog.Color;
            }
        }
    }
}
