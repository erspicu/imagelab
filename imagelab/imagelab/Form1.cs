using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace imagelab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void trackBar9_ValueChanged(object sender, EventArgs e)
        {
            label24.Text = trackBar9.Value.ToString();
        }

        private void trackBar8_ValueChanged(object sender, EventArgs e)
        {
            label23.Text = trackBar8.Value.ToString();
        }

        private void trackBar7_ValueChanged(object sender, EventArgs e)
        {
            label22.Text = trackBar7.Value.ToString();
        }



        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label12.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label11.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            label10.Text = trackBar3.Value.ToString();
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            label18.Text = trackBar6.Value.ToString();
        }

        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            label17.Text = trackBar5.Value.ToString();
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            label16.Text = trackBar4.Value.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "PNG Image|*.png";

            if (fd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            pictureBox1.Load(fd.FileName);
            pictureBox2.Load(fd.FileName);

        }

        private void trackBar8_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyContrast(trackBar8.Value);
            this.Enabled = true;

        }

        private void trackBar7_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyGamma(((double)(trackBar7.Value)) / 100.0);
            this.Enabled = true;

        }

        private void trackBar9_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyBrightness(trackBar9.Value);
            this.Enabled = true;
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyRGB_R(trackBar1.Value);
            this.Enabled = true;

        }


        private void trackBar2_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyRGB_G(trackBar2.Value);
            this.Enabled = true;
        }

        private void trackBar3_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyRGB_B(trackBar3.Value);
            this.Enabled = true;

        }

        private void trackBar6_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyHSL_Hue( trackBar6.Value );
            this.Enabled = true;
        }

        private void trackBar5_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyHSL_Saturation(trackBar5.Value);
            this.Enabled = true;
        }

        private void trackBar4_MouseUp(object sender, MouseEventArgs e)
        {
            this.Enabled = false;
            ApplyHSL_luminance (trackBar4.Value);
            this.Enabled = true;
        }


        //-----

        //轉灰階
        public void ApplyGreyscale()
        {
            byte A, R, G, B;
            Color pixelColor;


            bool ch = true;

            if (checkBox1.Checked == false)
                ch = false;


            for (int y = 0; y <  pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x <  pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;

                    if (ch == true)
                    {
                        R = (byte)((0.299 * pixelColor.R) + (0.587 * pixelColor.G) + (0.114 * pixelColor.B));
                        G = B = R;
                    }
                    else
                    {
                        R = pixelColor.R;
                        G = pixelColor.G;
                        B = pixelColor.B;
                    }

                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }

        }



        //HSL 調整
        //http://www.codeproject.com/Articles/19077/Hue-Saturation-Lightness-Filter
        //http://stackoverflow.com/questions/359612/how-to-change-rgb-color-to-hsv

        //HSL亮度




        //HSL 亮度調整
        public void ApplyHSL_luminance(double Lua)
        {
            const double c1o60 = 1 / 60.0;
            const double c1o255 = 1 / 255.0;

            double R, G, B;
            double H, S, L, H1;

            double min, max, dif, sum;
            double f1, f2;
            double v1, v2, v3;


            double sat = 0;// 127.0 * Sat / 100.0;
            double lum = 127.0 * Lua / 100.0;


            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    Color pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    B = pixelColor.B;
                    R = pixelColor.R;
                    G = pixelColor.G;

                    //RGB to HSL start            
                    min = R;
                    if (G < min) min = G;
                    if (B < min) min = B;

                    max = R;
                    f1 = 0.0;
                    f2 = G - B;

                    if (G > max)
                    {
                        max = G;
                        f1 = 120.0;
                        f2 = B - R;
                    }

                    if (B > max)
                    {
                        max = B;
                        f1 = 240.0;
                        f2 = R - G;
                    }

                    dif = max - min;
                    sum = max + min;
                    L = 0.5 * sum;


                    if (dif == 0)
                    {
                        H = 0.0;
                        S = 0.0;
                    }
                    else
                    {
                        if (L < 127.5)
                            S = 255.0 * dif / sum;
                        else
                            S = 255.0 * dif / (510.0 - sum);

                        H = (f1 + 60.0 * f2 / dif);
                        if (H < 0.0) H += 360.0;
                        if (H >= 360.0) H -= 360.0;
                    }

                    //RGB to HSL end

                    //轉換start
                    //H = H ;
                    if (H >= 360.0) H = H - 360.0;
                    S = S + sat;
                    if (S < 0.0) S = 0.0;
                    if (S > 255.0) S = 255.0;
                    L = L + lum;
                    if (L < 0.0) L = 0.0;
                    if (L > 255.0) L = 255.0;
                    //轉換end

                    //HSL to RGB start

                    if (S == 0)
                    {
                        R = L;
                        G = L;
                        B = L;
                    }
                    else
                    {

                        if (L < 127.5)
                            v2 = c1o255 * L * (255 + S);
                        else
                            v2 = L + S - c1o255 * S * L;

                        v1 = 2 * L - v2;
                        v3 = v2 - v1;
                        H1 = H + 120.0;

                        if (H1 >= 360.0) H1 -= 360.0;

                        if (H1 < 60.0)
                            R = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            R = v2;
                        else if (H1 < 240.0)
                            R = v1 + v3 * (4 - H1 * c1o60);
                        else
                            R = v1;

                        H1 = H;

                        if (H1 < 60.0)
                            G = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            G = v2;
                        else if (H1 < 240.0)
                            G = v1 + v3 * (4 - H1 * c1o60);
                        else
                            G = v1;

                        H1 = H - 120.0;

                        if (H1 < 0.0) H1 += 360.0;

                        if (H1 < 60.0)
                            B = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            B = v2;
                        else if (H1 < 240.0)
                            B = v1 + v3 * (4 - H1 * c1o60);
                        else
                            B = v1;

                    }
                    //HSL to RGB end
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(255, (int)R, (int)G, (int)B));


                }


            }
        }



        //HSL 飽和調整
        public void ApplyHSL_Saturation(double Sat)
        {
            const double c1o60 = 1 / 60.0;
            const double c1o255 = 1 / 255.0;

            double R, G, B;
            double H, S, L, H1;

            double min, max, dif, sum;
            double f1, f2;
            double v1, v2, v3;


            double sat =  127.0 * Sat / 100.0;
            double lum = 0;// 127 * _lightness / 100;


            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    Color pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    B = pixelColor.B;
                    R = pixelColor.R;
                    G = pixelColor.G;

                    //RGB to HSL start            
                    min = R;
                    if (G < min) min = G;
                    if (B < min) min = B;

                    max = R;
                    f1 = 0.0;
                    f2 = G - B;

                    if (G > max)
                    {
                        max = G;
                        f1 = 120.0;
                        f2 = B - R;
                    }

                    if (B > max)
                    {
                        max = B;
                        f1 = 240.0;
                        f2 = R - G;
                    }

                    dif = max - min;
                    sum = max + min;
                    L = 0.5 * sum;


                    if (dif == 0)
                    {
                        H = 0.0;
                        S = 0.0;
                    }
                    else
                    {
                        if (L < 127.5)
                            S = 255.0 * dif / sum;
                        else
                            S = 255.0 * dif / (510.0 - sum);

                        H = (f1 + 60.0 * f2 / dif);
                        if (H < 0.0) H += 360.0;
                        if (H >= 360.0) H -= 360.0;
                    }

                    //RGB to HSL end

                    //轉換start
                    //H = H ;
                    if (H >= 360.0) H = H - 360.0;
                    S = S + sat;
                    if (S < 0.0) S = 0.0;
                    if (S > 255.0) S = 255.0;
                    L = L + lum;
                    if (L < 0.0) L = 0.0;
                    if (L > 255.0) L = 255.0;
                    //轉換end

                    //HSL to RGB start

                    if (S == 0)
                    {
                        R = L;
                        G = L;
                        B = L;
                    }
                    else
                    {

                        if (L < 127.5)
                            v2 = c1o255 * L * (255 + S);
                        else
                            v2 = L + S - c1o255 * S * L;

                        v1 = 2 * L - v2;
                        v3 = v2 - v1;
                        H1 = H + 120.0;

                        if (H1 >= 360.0) H1 -= 360.0;

                        if (H1 < 60.0)
                            R = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            R = v2;
                        else if (H1 < 240.0)
                            R = v1 + v3 * (4 - H1 * c1o60);
                        else
                            R = v1;

                        H1 = H;

                        if (H1 < 60.0)
                            G = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            G = v2;
                        else if (H1 < 240.0)
                            G = v1 + v3 * (4 - H1 * c1o60);
                        else
                            G = v1;

                        H1 = H - 120.0;

                        if (H1 < 0.0) H1 += 360.0;

                        if (H1 < 60.0)
                            B = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            B = v2;
                        else if (H1 < 240.0)
                            B = v1 + v3 * (4 - H1 * c1o60);
                        else
                            B = v1;

                    }
                    //HSL to RGB end
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(255, (int)R, (int)G, (int)B));


                }


            }
        }



        //HSL Hue調整
        public void ApplyHSL_Hue(double hue)
        {
            const double c1o60 = 1 / 60.0;
            const double c1o255 = 1 / 255.0;

            double R, G, B;
            double H, S, L, H1;

            double min, max, dif, sum;
            double f1, f2;
            double v1, v2, v3;


            double sat = 0; // 127 * _saturation / 100;
            double lum = 0;// 127 * _lightness / 100;


            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    Color pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    B = pixelColor.B;
                    R = pixelColor.R;
                    G = pixelColor.G;

                    //RGB to HSL start            
                    min = R;
                    if (G < min) min = G;
                    if (B < min) min = B;

                    max = R;
                    f1 = 0.0;
                    f2 = G - B;

                    if (G > max)
                    {
                        max = G;
                        f1 = 120.0;
                        f2 = B - R;
                    }

                    if (B > max)
                    {
                        max = B;
                        f1 = 240.0;
                        f2 = R - G;
                    }

                    dif = max - min;
                    sum = max + min;
                    L = 0.5 * sum;


                    if (dif == 0)
                    {
                        H = 0.0;
                        S = 0.0;
                    }
                    else
                    {
                        if (L < 127.5)
                            S = 255.0 * dif / sum;
                        else
                            S = 255.0 * dif / (510.0 - sum);

                        H = (f1 + 60.0 * f2 / dif);
                        if (H < 0.0) H += 360.0;
                        if (H >= 360.0) H -= 360.0;
                    }

                    //RGB to HSL end

                    //轉換start
                    H = H + hue;
                    if (H >= 360.0) H = H - 360.0;
                    S = S + sat;
                    if (S < 0.0) S = 0.0;
                    if (S > 255.0) S = 255.0;
                    L = L + lum;
                    if (L < 0.0) L = 0.0;
                    if (L > 255.0) L = 255.0;
                    //轉換end

                    //HSL to RGB start

                    if (S == 0)
                    {
                        R = L;
                        G = L;
                        B = L;
                    }
                    else
                    {

                        if (L < 127.5)
                            v2 = c1o255 * L * (255 + S);
                        else
                            v2 = L + S - c1o255 * S * L;

                        v1 = 2 * L - v2;
                        v3 = v2 - v1;
                        H1 = H + 120.0;

                        if (H1 >= 360.0) H1 -= 360.0;

                        if (H1 < 60.0)
                            R = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            R = v2;
                        else if (H1 < 240.0)
                            R = v1 + v3 * (4 - H1 * c1o60);
                        else
                            R = v1;

                        H1 = H;

                        if (H1 < 60.0)
                            G = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            G = v2;
                        else if (H1 < 240.0)
                            G = v1 + v3 * (4 - H1 * c1o60);
                        else
                            G = v1;

                        H1 = H - 120.0;

                        if (H1 < 0.0) H1 += 360.0;

                        if (H1 < 60.0)
                            B = v1 + v3 * H1 * c1o60;
                        else if (H1 < 180.0)
                            B = v2;
                        else if (H1 < 240.0)
                            B = v1 + v3 * (4 - H1 * c1o60);
                        else
                            B = v1;

                    }
                    //HSL to RGB end
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(255, (int)R, (int)G, (int)B));


                }


            }
        }








        //RGB色調調整 - B
        public void ApplyRGB_B(int b)
        {
            int A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;

                    B = pixelColor.B + b;
                    if (B > 255)
                        B = 255;
                    else if (B < 0)
                        B = 0;

                    R = pixelColor.R;
                    G = pixelColor.G;
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
        }

        //RGB色調調整 - G
        public void ApplyRGB_G(int g)
        {
            int A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;

                    G = pixelColor.G + g;
                    if (G > 255)
                        G = 255;
                    else if (G < 0)
                        G = 0;

                    R = pixelColor.R;
                    B = pixelColor.B;
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
        }

        //RGB色調調整 - R
        public void ApplyRGB_R(int r)
        {
            int A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;
                    R = pixelColor.R + r;
                    if (R > 255)
                        R = 255;
                    else if (R < 0)
                        R = 0;
                    G = pixelColor.G;
                    B = pixelColor.B;
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
        }


        //亮度調整
        public void ApplyBrightness(int brightness)
        {
            int A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;
                    R = pixelColor.R + brightness;
                    if (R > 255)
                    {
                        R = 255;
                    }
                    else if (R < 0)
                    {
                        R = 0;
                    }

                    G = pixelColor.G + brightness;
                    if (G > 255)
                    {
                        G = 255;
                    }
                    else if (G < 0)
                    {
                        G = 0;
                    }

                    B = pixelColor.B + brightness;
                    if (B > 255)
                    {
                        B = 255;
                    }
                    else if (B < 0)
                    {
                        B = 0;
                    }

                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }

        }



        //Gamma調整
        //http://www.smokycogs.com/blog/image-processing-in-c-sharp-adjusting-the-gamma/

        public void ApplyGamma(double gamma)
        {
            byte A, R, G, B;
            Color pixelColor;

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0
                    * Math.Pow(i / 255.0, gamma)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0
                    * Math.Pow(i / 255.0, gamma)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0
                    * Math.Pow(i / 255.0, gamma)) + 0.5));
            }

            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;
                    R = redGamma[pixelColor.R];
                    G = greenGamma[pixelColor.G];
                    B = blueGamma[pixelColor.B];
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
        }

        //對比調整
        // http://www.smokycogs.com/blog/image-processing-in-c-sharp-contrast/
        public void ApplyContrast(double contrast)
        {
            double A, R, G, B;
            Color pixelColor;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            for (int y = 0; y < pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;
                    R = pixelColor.R / 255.0;
                    R -= 0.5;
                    R *= contrast;
                    R += 0.5;
                    R *= 255;

                    if (R > 255)
                        R = 255;
                    else if (R < 0)
                        R = 0;

                    G = pixelColor.G / 255.0;
                    G -= 0.5;
                    G *= contrast;
                    G += 0.5;
                    G *= 255;

                    if (G > 255)
                        G = 255;
                    else if (G < 0)
                        G = 0;

                    B = pixelColor.B / 255.0;
                    B -= 0.5;
                    B *= contrast;
                    B += 0.5;
                    B *= 255;
                    if (B > 255)
                        B = 255;
                    else if (B < 0)
                        B = 0;
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }

        }

        public void ApplyInvert()
        {
            byte A, R, G, B;
            Color pixelColor;

            bool ch = true;

            if (checkBox2.Checked == false)
                ch = false;

            for (int y = 0; y <  pictureBox1.Image.Height; y++)
            {
                for (int x = 0; x < pictureBox1.Image.Width; x++)
                {
                    pixelColor = ((Bitmap)(pictureBox1.Image)).GetPixel(x, y);
                    A = pixelColor.A;
                    if (ch == true)
                    {
                        R = (byte)(255 - pixelColor.R);
                        G = (byte)(255 - pixelColor.G);
                        B = (byte)(255 - pixelColor.B);
                    }
                    else
                    {
                        R = (byte)(  pixelColor.R);
                        G = (byte)(  pixelColor.G);
                        B = (byte)(  pixelColor.B);
                    }
                    ((Bitmap)(pictureBox2.Image)).SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            this.Enabled = false;
            ApplyGreyscale();
            this.Enabled = true;

        }



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            

            this.Enabled = false;
            ApplyInvert();
            this.Enabled = true;

        }













    }
}
