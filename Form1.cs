using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace webcam

{
    public partial class Form1 : Form
    {
        OpenCvSharp.VideoCapture capture;
        OpenCvSharp.Mat frame;
        Bitmap image;
        OpenCvSharp.Mat image1;
        private Thread camera;
        bool isCameraRunning = false;
        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();
        }
        private void CaptureCameraCallback()
        {
            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);
            if (capture.IsOpened())
            {
                while (isCameraRunning)
                {
                    capture.Read(frame);
                    image1 = frame;

                    // image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = BitmapConverter.ToBitmap(image1);

                   

                }
            }


        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Start"))
            {
                CaptureCamera();
                button1.Text = "Stop";
                isCameraRunning = true;
                
            }
            else
            {
                capture.Release();
                button1.Text = "Start";
                isCameraRunning = false;
            }
        }
        [DllImport("detectcameratest.dll")]
        [return: MarshalAs(UnmanagedType.SafeArray)]
        private extern static string[] ProcessFrame(int width, int height, IntPtr data);
        private void button2_Click(object sender, EventArgs e)
        {
            int sleepTime = (int)Math.Round(1000 / capture.Fps);

            if (isCameraRunning)
            {

              //  while (true)
               // {
                    try
                {
                        
                   // Image img = pictureBox1.Image;
                                          
                    //OpenCvSharp.Mat converted = OpenCvSharp.Extensions.BitmapConverter.ToMat(snapshot);

                    var facelist = ProcessFrame(frame.Width, frame.Height, frame.Data);

                    foreach (var item in facelist)
                    {
                        listBox1.Items.Add(item);
                    }
        
                    Thread.Sleep(50);
                
                    //  snapshot.Save(string.Format(@"C:\Users\Subtek\Desktop\face_datase\{0}.jpg", Guid.NewGuid()), ImageFormat.Jpeg);
                    Application.DoEvents();
                }
                //
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                   // }

            }

            else
            {
                MessageBox.Show("Cannot take picture if the camera isn't capturing image!");
            }

            Cv2.WaitKey(sleepTime);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}

