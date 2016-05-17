using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.VisualBasic.PowerPacks;
using Microsoft.VisualBasic;
using System.Management;
using System.Management.Instrumentation;



using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using DirectShowLib;



using System.IO;









namespace CameraCapture
{
    public partial class CameraCapture : Form
    {
        // Declaring variables 
        private Capture capture1, capture2;
        private bool captureInProgress;


     
        

       

        // define number of parameter to be read/write to the camera config
        public const int numberParam = 15;

        // password to be checked
        public const string loginPass = "Mac4205";

        // array of camera parameters defined by user
        double[] cameraParam = new double[numberParam];

        // Array with two x,y points for the cebter
        Point[] pictureboxPointCenter = new Point[2];

        Point [] linesPointsPictureBox= new Point[8];

        Point[] pictureBoxStartPoint = new Point[2];


        // global suttf for the images
        Image<Bgr, Byte> ImageFrame1 ;
        Image<Bgr, Byte> ImageFrame2 ;


        double frameFPS1=10;

        double frameFPS2=10;

        Thread t1, t2;

        int count = 255;

        bool frameFlag = false;

        // capture flag global
        //int flag = 0;

        // line segments to be used in the draw
     /*   LineSegment2D lineH1 ;
        LineSegment2D lineV1 ;
        
        LineSegment2D lineH2 ;
        LineSegment2D lineV2 ;

        // bgr to paint lines
        Bgr pen;*/
        

        
        


        // Initialize the component
        public CameraCapture()
        {
            InitializeComponent();
            // creating the event size changed to resize picture box
            this.SizeChanged += new EventHandler(CameraCapture_SizeChanged);
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
                       
            ResizeRedraw = true;
            this.CamImageBoxLeft.BackColor = Color.Transparent;
            this.CamImageBoxRight.BackColor = Color.Transparent;



            t1 = new Thread(ProcessFrame1);
            t2 = new Thread(ProcessFrame2);

            
        }



        




        // METHOD: to resize the picture boxes according form size
        private void SetPictureboxSize()
        {
            // Hardcoded boundaries 
            double x_bound = 0.12;
            double y_bound = 0.25;
            double y_split = 0.5;
            double offset = 0.1;
            

            // gettting the form size
            int formHeight = this.Height;
            int formWidth = this.Width;

            CamImageBoxLeft.SetBounds((int)(x_bound * formWidth), (int)(y_split * formHeight), (int)(formWidth / 2 - x_bound * formWidth * 2), (int)(formHeight - y_split * formHeight - y_bound * formHeight));
            CamImageBoxRight.SetBounds((int)(formWidth / 2 + x_bound * formWidth), (int)(y_split * formHeight), (int)(formWidth / 2 - x_bound * formWidth * 2), (int)(formHeight - y_split * formHeight - y_bound * formHeight));
            VerticalLine1.SetBounds((int)(CamImageBoxLeft.Location.X + CamImageBoxLeft.Width / 2), (int)(CamImageBoxLeft.Location.Y - CamImageBoxLeft.Height * offset), 1, (int)(CamImageBoxLeft.Height + CamImageBoxLeft.Height * offset * 2));
            VerticalLine2.SetBounds((int)(CamImageBoxRight.Location.X + CamImageBoxRight.Width / 2), (int)(CamImageBoxRight.Location.Y - CamImageBoxRight.Height * offset), 1, (int)(CamImageBoxRight.Height + CamImageBoxRight.Height * offset * 2));
            HorizontalLine1.SetBounds((int)(CamImageBoxLeft.Location.X - offset * CamImageBoxLeft.Height), (int)(CamImageBoxLeft.Location.Y + CamImageBoxLeft.Height / 2), (int)(CamImageBoxRight.Location.X + CamImageBoxRight.Width + 2 * offset * CamImageBoxRight.Height - CamImageBoxLeft.Location.X), 1);
          
           

                       
            

            // getting the center points of picturebox
            pictureboxPointCenter[0].X =   CamImageBoxLeft.Width / 2;
            pictureboxPointCenter[0].Y =  CamImageBoxLeft.Height / 2;

            pictureboxPointCenter[1].X = CamImageBoxRight.Width / 2 ;
            pictureboxPointCenter[1].Y = CamImageBoxRight.Height / 2 ;


            
            
        }

        private Image<Bgr, Byte> CutImage(Image<Bgr, byte> imgOriginal, double zoomValue)
        {
            // if zoom value is defined above
            if (zoomValue != 1)
            {

                // normilizing percentage
                double normalzedPercentage = 1.0 / zoomValue;

                imgOriginal.ROI = new Rectangle((int)((imgOriginal.Width / 2) - (imgOriginal.Width / 2) * normalzedPercentage), (int)((imgOriginal.Height / 2)-(imgOriginal.Height / 2) * normalzedPercentage), (int)(imgOriginal.Width * normalzedPercentage), (int)(imgOriginal.Height * normalzedPercentage));
                return imgOriginal.Copy();
            }
            else
                return imgOriginal;
        }

        //METHOD: Set Picturebox properties
        private void SetPictureBoxProperties(int value, int picture)
        {

            CamImageBoxLeft.AutoScrollOffset.Offset(pictureboxPointCenter[0]);

            if (value != 0)
            {
                if (picture == 1)
                {
                    CamImageBoxLeft.VerticalScrollBar.Visible = true;
                    CamImageBoxLeft.HorizontalScrollBar.Visible = true;
                }
                else
                {
                    CamImageBoxRight.VerticalScrollBar.Visible = true;
                    CamImageBoxRight.HorizontalScrollBar.Visible = true;

                }

            }
            else
            {
                if (picture == 1)
                {
                    CamImageBoxLeft.VerticalScrollBar.Visible = false;
                    CamImageBoxLeft.HorizontalScrollBar.Visible = false;
                }
                else
                {
                    CamImageBoxRight.VerticalScrollBar.Visible = false;
                    CamImageBoxRight.HorizontalScrollBar.Visible = false;

                }
 
            }


            

            //CamImageBoxLeft.SizeMode = PictureBoxSizeMode.CenterImage;
            //CamImageBoxRight.SizeMode = PictureBoxSizeMode.CenterImage;
            
        }



        //METHOD: Read hardware info
        public static string GetHardwareId(string device)
        {

            string hardwareInfo = "";
            string type = "";
            string type_properties = "";

            switch (device)
            {
                case "cpu": type = "win32_processor";
                    type_properties = "processorID";
                    break;

                case "mac": type = "Win32_NetworkAdapterConfiguration";
                    type_properties = "MacAddress";
                    break;

                default:
                    break;

            }

            try
            {
                ManagementClass mc = new ManagementClass(type);
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    hardwareInfo = mo.Properties[type_properties].Value.ToString();
                    break;
                }

                mc.Dispose();
                moc.Dispose();
                // return the saved string
                return hardwareInfo;
            }
            catch
            {

                MessageBox.Show("CPU Id not Available");
                Application.Exit();
                
                return "";
            }
        }

        // METHOD: Show message box with warnings
        private void ShowErrorMessage( string type)
        {
            String messageBoxText = "";
            String caption = "";
            MessageBoxIcon icon = MessageBoxIcon.Exclamation;
            
            switch ( type)
            {

                case "ID":
                    messageBoxText =  "Please contact 'Mac'Label Graph'";
                    caption = "ID Error";
                    icon = MessageBoxIcon.Exclamation;
                    break;
                    
                case "cam":
                    messageBoxText = "Insuficient number of cameras Detected";
                    caption = "Camera Error";
                    icon = MessageBoxIcon.Error;
                    break;
                    
                case "pass":
                    messageBoxText = "Invalid Password!!";
                    caption = "Camera Error";
                    icon = MessageBoxIcon.Warning;
                    break;

                default:
                    messageBoxText = "Unhandled Error. Please Contact 'Mac'Label Graph'";
                    caption = "Error";
                    icon = MessageBoxIcon.Hand;
                        break;
            }

            MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OK, icon);
            return;
           
        }


        // METHOD:  to read the cameraparam or if file doeasent exist create with default parameters
        private void ReadConfigFile(string device)
        {
            // Predifined hardcoded parameters if file doesent exist
            // Note, if mode parameters need, need to define on the object
            cameraParam[0] = 0;
            cameraParam[1] = 0;
            cameraParam[2] = 0; // Flip camera 1 horizontal
            cameraParam[3] = 0; // Flip camera 2 horizontal
            cameraParam[4] = 0; // flip Camera 1 Vertical
            cameraParam[5] = 0; // flip Camera 2 Vertical
            cameraParam[6] = 1.0; // Camera 1 Bright
            cameraParam[7] = 1.0; // camera 2 bright
            cameraParam[8] = 1.0; // camera 1 Contrast
            cameraParam[9] = 1.0; // camera 2 contrast
            cameraParam[10] = 1.0;  // camera 1 zoom ( double change array)
            cameraParam[11] = 1.0; // camera 2 zoom () 
            //
            cameraParam[12] = 800; // cameras weifh Both
            cameraParam[13] = 600; // cameras height Both
            cameraParam[14] = 30; // FPS both cameras

            


            // aux local variables
            string line = "";
            int count = 0;
            string path;

            // lest get the path
           path = Path.GetDirectoryName(Application.ExecutablePath);
            

            // lets get the current path
            //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //path = Directory.GetCurrentDirectory();

            try
            {
                // Lets try o read file with the concatenated directory
                path = @path + "\\config.txt";
                path = @"config.txt";
              
                
                    using (StreamReader sr = new StreamReader(path))
                    {
                        // lest test if the file if there!
                        if (sr != null)
                        {
                            // If ok lets extract and add to the variables
                            while (count < numberParam)
                            {
                                // read the line 
                                if ((line = sr.ReadLine()) == null) break;
                                // split the string by the token camera and value
                                string[] tokens = line.Split('=');
                                // convert the second tokens to double that are the parameters
                                cameraParam[count] = double.Parse(tokens[1]);
                                count++;
                            }

                            // clse the file to read again and check
                            string idharware = GetHardwareId(device);
                            // read next line
                            line = sr.ReadLine();
                            if (line.Contains("id"))
                            {
                                string[] tokensid = line.Split('=');
                                if ((tokensid[1].Equals(idharware)))
                                {
                                    sr.Close();
                                    sr.Dispose();

                                    return;
                                }
                                else
                                {
                                    sr.Close();
                                    sr.Dispose();
                                    ReleaseData();


                                    // sHOW MESSAGE BOX
                                    ShowErrorMessage("ID");

                                    ReleaseData();
                                    // fecho aplicacao
                                    Application.Exit();
                                }
                            }
                        }


                        // here remove all txt and create a new one
                        sr.Close();
                        sr.Dispose();

                    }
               
            }
            catch (FileNotFoundException ex) // if any error lest create the file with default parameters
            {
                
                string temp = "";

                // read the id hardware
                string idharware = GetHardwareId(device);

                // lounch the input box
                //string valueInputBox = Interaction.InputBox("Insert Password", string.Empty);
                string valueInputBox = Prompt.ShowDialog("Insert Password", "Admin");
                
                if (valueInputBox.Equals(loginPass))
                {
                    // Ok add new line with the id value to be added to file
                    temp = "id=" + idharware + Environment.NewLine;

                }
                else
                {
                    
                    // password wrong close form
                    ShowErrorMessage("pass");
                    ReleaseData();
                    Application.Exit();
                    return;
                }

                // lest get the path
                path = Path.GetDirectoryName(Application.ExecutablePath);
                
                // lets get the current path
                //Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                //path = Directory.GetCurrentDirectory();

                // create the file
                path = @path + "\\config.txt";
                path = @"config.txt";
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = new StreamWriter(path,true))
                    {
                        // for each camara parameter extract and write
                        count = 0;
                        string parse = "";
                        foreach (int param in cameraParam)
                        {
                            // build the string parameter
                            if (count < 2)
                                parse = "camaraAngle" + count.ToString() + "=" + param.ToString();

                            if (count > 1 && count < 4)
                                parse = "camaraFlipHorizontal" + (count - 2).ToString() + "=" + param.ToString();

                            if (count > 3 && count < 6)
                                parse = "camaraFlipVerical" + (count - 4).ToString() + "=" + param.ToString();

                            if (count > 5 && count < 8)
                                parse = "camaraBright" + (count - 6).ToString() + "=" + param.ToString();

                            if (count > 7 && count < 10)
                                parse = "camaraContrast" + (count - 8).ToString() + "=" + param.ToString();

                            if (count > 9 && count < 12)
                                parse = "camaraZoom" + (count - 10).ToString() + "=" + param.ToString();

                            if (count == 12)
                                parse = "camarasWidth=" + param.ToString();


                            if (count == 13)
                                parse = "camarasHeight=" + param.ToString();


                            if (count == 14)
                                parse = "camarasFPS=" + param.ToString();


                            // Write the param on the file camera=angle
                            sw.WriteLine(parse);
                            count++;
                        }
                        sw.WriteLine(temp); // default hardware ID
                        sw.Close();
                        sw.Dispose();
                    }
                }
                
                // here I can show show an auto destructive messa message box with default param loaded
            }
        }

        //METHOD: obtain the 2d lines to put on the image
        public void SetLines2D(Image<Bgr, Byte> img1, Image<Bgr, Byte> img2)
        {
            // Image 1
            int heightPic1 = img1.Size.Height;
            int widthPic1 = img1.Size.Width;
            Point startH1 = new Point(0, heightPic1 / 2);
            Point endH1 = new Point(widthPic1, heightPic1 / 2);
            Point startV1 = new Point(widthPic1 / 2, 0);
            Point endV1 = new Point(widthPic1 / 2, heightPic1);

            // image 2
            int heightPic2 = img2.Size.Height;
            int widthPic2 = img2.Size.Width;
            Point startH2 = new Point(0, heightPic2 / 2);
            Point endH2 = new Point(widthPic2, heightPic2 / 2);
            Point startV2 = new Point(widthPic2 / 2, 0);
            Point endV2 = new Point(widthPic2 / 2, heightPic2);


           
        }


        // METHOD: resize image 
        private Image ZoomImage(Image input, Rectangle zoomArea, Rectangle sourceArea)
        {
            Bitmap newImage = new Bitmap(sourceArea.Width, sourceArea.Height);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                
                g.DrawImage(input, sourceArea,zoomArea,GraphicsUnit.Pixel);
 
            }
            return (Image)newImage;
        }



        // METHOD: grab the frame from camera and rotate and show on imagebox
        public void ProcessFrame1()
        {
            do{
                // Get the frames for both imagebox
                ImageFrame1 = capture1.QueryFrame();
                // get the fps in runtime
                if (frameFlag == true)
                    frameFPS1 = capture1.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);

               


                // here we can flip the image acording to arguments passed
                if (cameraParam[2] != 0)
                    ImageFrame1 = ImageFrame1.Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);

                if (cameraParam[4] != 0)
                    ImageFrame1 = ImageFrame1.Flip(Emgu.CV.CvEnum.FLIP.VERTICAL);


                // Here we can rotate the frame according to the angle ( angle is an argument passed on text)
                ImageFrame1 = ImageFrame1.Rotate(cameraParam[0], new Bgr());



                // Apply brighness to the image
                ImageFrame1 = ImageFrame1.Mul(cameraParam[6]);



                // apply contrast to the image
                ImageFrame1._GammaCorrect(cameraParam[8]);


                //ImageFrame1._EqualizeHist();

                SetPictureBoxProperties(0, 1);


                CamImageBoxLeft.Image = CutImage(ImageFrame1, cameraParam[10]);

                Thread.Sleep((int)(1000 / frameFPS1));
            }while(true);

        }
           

        // METHOD: grab the frame from camera and rotate and show on imagebox
        private void ProcessFrame2()
        {
            // test if has new frame
            do{
            // Get the frames for both imagebox            
            ImageFrame2 = capture2.QueryFrame();
            count--;
            if (count == 0)
                frameFlag = true;


             if(frameFlag == true)    
                frameFPS2 = capture2.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS);

                
            



           
            
            if (cameraParam[3] != 0)
                ImageFrame2 = ImageFrame2.Flip(Emgu.CV.CvEnum.FLIP.HORIZONTAL);
            
            if (cameraParam[5] != 0)
                ImageFrame2 = ImageFrame2.Flip(Emgu.CV.CvEnum.FLIP.VERTICAL);



            // Here we can rotate the frame according to the angle ( angle is an argument passed on text)
            ImageFrame2 = ImageFrame2.Rotate(cameraParam[1], new Bgr());

            
            // Apply brighness to the image
            ImageFrame2 =ImageFrame2.Mul(cameraParam[7]);

            // apply contrast to the image
            ImageFrame2._GammaCorrect(cameraParam[9]);

            //ImageFrame2._EqualizeHist();
 
            
            SetPictureBoxProperties(0, 2);

            CamImageBoxRight.Image = CutImage(ImageFrame2, cameraParam[11]);

            Thread.Sleep((int)(1000 / frameFPS2));

            }while(true);

        }







        // METHOD: SET the camera parameter capture
        private void SetCaptureParameters()
        {
                   
            // Set cameras1  bright
            capture1.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, cameraParam[6]);

            // set camera 2 bright
            capture2.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, cameraParam[7]);

            // set camera contrast
            capture1.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, cameraParam[8]);
            //
            capture2.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, cameraParam[9]);

            // Set size camera
            capture1.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, Convert.ToInt32(cameraParam[12]));
            capture1.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, Convert.ToInt32(cameraParam[13]));

            // same for camera 2
            capture2.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, Convert.ToInt32(cameraParam[12]));
            capture2.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, Convert.ToInt32(cameraParam[13]));


            // set the cameras FPS equal for both
            capture1.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS, cameraParam[14]);
            capture2.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS, cameraParam[14]);



        }



        /////////////////////////////////////
        // event handlers Zone
        /// <summary>
        /// /////////////////////////////////
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Fist get the list of cameras available
            DsDevice[] _SystemCameras = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            //MessageBox.Show(_SystemCameras.Length.ToString());
            
            // test number of devices
            if (_SystemCameras.Length < 2)
            {
                ShowErrorMessage("cam");
                ReleaseData();
                Application.Exit();
                return;

            }


            // Thy to get the frame for both cameras, if fail excpetion is raised and handled
            if (capture1 == null && capture2 == null)
            {
                try
                {
                    capture1 = new Capture(0);
                    capture2 = new Capture(1);

                    // Set the properties of the cameras acording parameters on file
                    SetCaptureParameters();
                }

                catch 
                {
                    MessageBox.Show("cam");

                    // delay para fechar
                    ReleaseData();


                    // fecho aplicacao
                    Application.Exit();

                }
            }
            if (capture1 != null && capture2 != null)
            {
               
                if (captureInProgress)
                {
                    btnStart.Text = "StartCam";

                    t1.Join();
                    t2.Join();
                   // t1.Abort();
                    //t2.Abort();


                    //Application.Idle -= ProcessFrame1;
                    //Application.Idle -= ProcessFrame2;
                }
                else
                {
                    btnStart.Text = "StopCam";
                    t1.Priority = ThreadPriority.Highest;
                    t2.Priority = ThreadPriority.Highest;
                    
                    t1.Start();
                    t2.Start();
                    
                    
                    //Application.Idle += ProcessFrame1;
                    //Application.Idle += ProcessFrame2;
                }
                captureInProgress = !captureInProgress;
            }
        }

        // METHOD: for release resourses
        private void ReleaseData()
        {
            if (capture1 != null)
                capture1.Dispose();

            if (capture2 != null)
                capture2.Dispose();

        }



        // on form rezize event
        private void CameraCapture_SizeChanged(object sender, EventArgs e)
        {
            SetPictureboxSize();
        }


        // on form load 
        private void CameraCapture_Load(object sender, EventArgs e)
        {
            

            // MessageBox.Show("Diskid:"+ GetHardwareId("mac"));
            // on form load read the config file and proceed

            // read the config file and create if doasent exist
            ReadConfigFile("cpu");
            
            // maximize the form 
            this.WindowState = FormWindowState.Maximized;

            // Set the pictures box size accordinf to form
            SetPictureboxSize();
            

            // start the thing :)
            btnStart.PerformClick();
            btnStart.Hide();



        }

        private void CameraCapture_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            t1.Join();
            t2.Join();
           // t1.Abort();
            //t2.Abort();
            ReleaseData();
            Application.Exit();
        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lineShapeV2_Click(object sender, EventArgs e)
        {

        }

        private void lineShapeH_Click(object sender, EventArgs e)
        {

        }




    }


}
