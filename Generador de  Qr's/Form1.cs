using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using System.IO;
using System.Windows.Shapes;
using static QRCoder.PayloadGenerator;

namespace Generador_de__Qr_s
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtContenido.Text != "")
                {
                    if (rdbNormal.Checked)
                    {
                        using (SaveFileDialog s = new SaveFileDialog())
                        {
                            s.FileName = txtContenido.Text;
                            s.DefaultExt = ".Png";
                            s.Filter = "Image(.Png)|*.Png";
                            if (s.ShowDialog() == DialogResult.OK)
                            {
                                // Save Image
                                string filename = s.FileName;
                                FileStream fstream = new FileStream(filename, FileMode.Create);
                                pbCodigoQr.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Png);
                                fstream.Close();
                            }
                        }
                        MessageBox.Show("Codigo Generado con exito!");
                        txtContenido.Clear();
                    }
                    else if(rdbEnlace.Checked)
                    {
                        using (SaveFileDialog s = new SaveFileDialog())
                        {
                            s.FileName = "Enlace";
                            s.DefaultExt = ".Png";
                            s.Filter = "Image(.Png)|*.Png";
                            if (s.ShowDialog() == DialogResult.OK)
                            {
                                // Save Image
                                string filename = s.FileName;
                                FileStream fstream = new FileStream(filename, FileMode.Create);
                                pbCodigoQr.Image.Save(fstream, System.Drawing.Imaging.ImageFormat.Png);
                                fstream.Close();
                            }
                            MessageBox.Show("Codigo Generado con exito!");
                            txtContenido.Clear();
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error> " + x.Message);
            }
        }

        private void rdbNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNormal.Checked)
            {
                txtContenido.Clear();
                rdbEnlace.Checked = false;
            }
        }

        private void rdbEnlace_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEnlace.Checked)
            {
                txtContenido.Clear();
                rdbNormal.Checked = false;
            }
        }

        /*private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\CodigoQr\\";
                System.Diagnostics.Process.Start(@path);
            }
            catch (Exception x)
            {
                MessageBox.Show("Error> " + x.Message);
            }
        }*/

        private void txtContenido_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtContenido.Text != "")
                {
                    if (rdbNormal.Checked)
                    {
                        //Contenido normal
                        using (QRCodeGenerator qr = new QRCodeGenerator())
                        {
                            QRCodeData qrData = qr.CreateQrCode(txtContenido.Text, QRCodeGenerator.ECCLevel.Q);
                            QRCode qrCode = new QRCode(qrData);

                            //Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("C:\\myimage.png"));
                            Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\zQ.png"));

                            pbCodigoQr.Image = qrCodeImage;
                        }
                    }
                    else if (rdbEnlace.Checked)
                    {
                        //Enlace
                        using (QRCodeGenerator qr = new QRCodeGenerator())
                        {
                            Url generator = new Url(txtContenido.Text);
                            string payload = generator.ToString();
                            QRCodeData qrData = qr.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
                            QRCode qrCode = new QRCode(qrData);
                            Bitmap qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\zQ.png"));
                            pbCodigoQr.Image = qrCodeImage;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecciona un modo primero!");
                        txtContenido.Clear();
                    }
                }
                else
                {
                    pbCodigoQr.Image = null;
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error> " + x.Message);
            }
        }
        private void txtContenido_MouseClick(object sender, MouseEventArgs e)
        {
            txtContenido.SelectAll();
        }

        private void txtContenido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
                btnGenerar.Focus();
        }
    }
}
