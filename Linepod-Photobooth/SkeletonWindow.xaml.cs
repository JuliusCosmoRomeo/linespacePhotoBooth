﻿/*using System.IO;
using System.Windows;
using System.Windows.Media;
using Microsoft.Kinect;

using System.Windows.Documents;
using System.Collections.Generic;
using PointF = System.Drawing.PointF;
using System;
using System.Text;
using System.Diagnostics;
using System.Net.Sockets;
using Svg;
using Svg.Pathing;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;

namespace Microsoft.Samples.Kinect.BackgroundRemovalBasics

{
    
  
    public partial class SkeletonWindow : Window
    {

        private const int scale = 100;
        private const int svgWidth = 200;
        private const int svgHeight = 200;
        private static BluetoothClient thisDevice;
        private Boolean alreadyPaired=false;
        private Skeleton[] skeletons;
        /// <summary>
        /// Width of output drawing
        /// </summary>
        private const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        private const float RenderHeight = 480.0f;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of body center ellipse
        /// </summary>
        private const double BodyCenterThickness = 10;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Brush used to draw skeleton center point
        /// </summary>
        private readonly Brush centerPointBrush = Brushes.Blue;

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently tracked
        /// </summary>
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Drawing group for skeleton rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public SkeletonWindow()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping skeleton data
        /// </summary>
        /// <param name="skeleton">skeleton to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            Image.Source = this.imageSource;

            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.NoKinectReady;
            }


            //Bluetooth stuff here 
            thisDevice = new BluetoothClient();
            BluetoothComponent bluetoothComponent = new BluetoothComponent(thisDevice);
            bluetoothComponent.DiscoverDevicesProgress += bluetoothComponent_DiscoverDevicesProgress;
            bluetoothComponent.DiscoverDevicesComplete += bluetoothComponent_DiscoverDevicesComplete;
            bluetoothComponent.DiscoverDevicesAsync(8, true, true, true, false, thisDevice);

        }


        private void bluetoothComponent_DiscoverDevicesComplete(object sender, DiscoverDevicesEventArgs e)
        {
            Console.WriteLine("Discovery finished");
        }

        private void bluetoothComponent_DiscoverDevicesProgress(object sender, DiscoverDevicesEventArgs e)
        {
            foreach (BluetoothDeviceInfo device in e.Devices)
            {
                Debug.WriteLine(device.DeviceName + " is a " + device.ClassOfDevice.MajorDevice.ToString());
                if (device.DeviceName.Contains("raspberry") && !alreadyPaired)
                {
                    bool paired = BluetoothSecurity.PairRequest(device.DeviceAddress, "123456");
                    if (paired)
                    {
                        alreadyPaired = true;
                        MessageBox.Show("Paired!");
                        thisDevice.BeginConnect(device.DeviceAddress,BluetoothService.SerialPort, new AsyncCallback(Connect), device);

                    }
                    else
                    {
                        MessageBox.Show("There was a problem pairing.");
                    }
                }
            }
        }

        private void SendSvg(String svgString)
        {
            if (thisDevice.Connected)
            {
                Debug.WriteLine("Connected");
                NetworkStream stream = thisDevice.GetStream();
                if (stream.CanWrite)
                {

                    stream.Write(Encoding.UTF8.GetBytes(svgString), 0, Encoding.UTF8.GetBytes(svgString).Length);

                }
            }
            else
            {
                Debug.WriteLine("Not Connected");
            }
           
        }


        private static void Connect(IAsyncResult result)
        {

            if (result.IsCompleted)
            {
                
                // client is connected now :)
                Console.WriteLine(thisDevice.Connected);
                
                

                /*NetworkStream stream = thisDevice.GetStream();
                
                if (stream.CanRead)
                {
                    byte[] myReadBuffer = new byte[1024];
                    StringBuilder myCompleteMessage = new StringBuilder();
                    int numberOfBytesRead = 0;

                    // Incoming message may be larger than the buffer size. 
                    do
                    {
                        numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);

                        myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);

                    // Print out the received message to the console.
                    Console.WriteLine("You received the following message : " + myCompleteMessage);
                }
                else
                {
                    Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
                }

                Console.ReadLine();*/


/*
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        RenderClippedEdges(skel, dc);

                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);

                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skel.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }

        private String BuildSvg(Skeleton skel)
        {
            SvgDocument doc = new SvgDocument()
            {
                Width = svgWidth,
                Height = svgHeight
            };

            SvgPath path = new SvgPath()
            {
                FillOpacity = 0,
                Stroke = new SvgColourServer(System.Drawing.Color.Black)
            };
        
            Joint leftHand = skel.Joints[JointType.HandLeft];
            Joint rightHand = skel.Joints[JointType.HandRight];
            Joint leftWrist = skel.Joints[JointType.WristLeft];
            Joint rightWrist = skel.Joints[JointType.WristRight];
            Joint leftElbow = skel.Joints[JointType.ElbowLeft];
            Joint rightElbow = skel.Joints[JointType.ElbowRight];
            Joint leftShoulder = skel.Joints[JointType.ShoulderLeft];
            Joint rightShoulder = skel.Joints[JointType.ShoulderRight];
            Joint leftFoot = skel.Joints[JointType.FootLeft];
            Joint rightFoot = skel.Joints[JointType.FootRight];
            Joint leftAnkle = skel.Joints[JointType.AnkleLeft];
            Joint rightAnkle = skel.Joints[JointType.AnkleRight];
            Joint leftKnee = skel.Joints[JointType.KneeLeft];
            Joint rightKnee = skel.Joints[JointType.KneeRight];
            Joint leftHip = skel.Joints[JointType.HipLeft];
            Joint rightHip = skel.Joints[JointType.HipRight];
            Joint head = skel.Joints[JointType.Head];
            Joint shoulderCenter= skel.Joints[JointType.ShoulderCenter];
            Joint spine = skel.Joints[JointType.Spine];
            Joint hipCenter = skel.Joints[JointType.HipCenter];

            List<Joint> arms = new List<Joint>();
            arms.Add(leftHand);
            arms.Add(leftWrist);
            arms.Add(leftElbow);
            arms.Add(leftShoulder);
            arms.Add(shoulderCenter);
            arms.Add(rightShoulder);
            arms.Add(rightElbow);
            arms.Add(rightWrist);
            arms.Add(rightHand);

            List<Joint> back = new List<Joint>();
            //back.Add(head);
            back.Add(shoulderCenter);
            back.Add(spine);
            back.Add(hipCenter);

            List<Joint> legs = new List<Joint>();
            legs.Add(leftFoot);
            legs.Add(leftAnkle);
            legs.Add(leftKnee);
            legs.Add(leftHip);
            legs.Add(hipCenter);
            legs.Add(rightHip);
            legs.Add(rightKnee);
            legs.Add(rightAnkle);
            legs.Add(rightFoot);

            AddJointsToPath(path, arms,100);
            AddJointsToPath(path, back,100);
            AddJointsToPath(path, legs,100);

            Console.WriteLine("svg output");
            float deltaX = leftHand.Position.X - leftElbow.Position.X;
            float deltaY = leftHand.Position.Y - leftElbow.Position.Y;
            float deltaZ = leftHand.Position.Z - leftElbow.Position.Z;

            float distance = (float)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
            float headRadius = (float)(distance*scale/2.5);
            foreach (SvgPathSegment element in path.PathData)
            {

                Console.WriteLine(element.ToString());

            }

            Console.WriteLine("svg output end");

            
            //calculate intersecion point of head and neck
            //double shoulderToHead = Math.Sqrt(Math.Pow(head.Position.X - shoulderCenter.Position.X,2) + Math.Pow(head.Position.Y - shoulderCenter.Position.Y,2));
            Vector headVec = new Vector(head.Position.X, head.Position.Y);
            Vector distVector = headVec - new Vector(shoulderCenter.Position.X, shoulderCenter.Position.Y);
            distVector.Normalize();
            Vector intersectingPoint = headVec - distVector*headRadius;

            SvgCircle headCircle = new SvgCircle() {
                Radius = headRadius,

                FillOpacity = 0,
                Stroke = new SvgColourServer(System.Drawing.Color.Black),
                CenterX = new Svg.SvgUnit(TranslatePosition(head.Position.X)),
                CenterY = new Svg.SvgUnit(TranslatePosition(head.Position.Y)),
                StrokeWidth = 1
            };
            path.PathData.Add(new SvgMoveToSegment(new PointF(TranslatePosition((float)intersectingPoint.X), TranslatePosition((float)intersectingPoint.Y))));
            path.PathData.Add(new SvgLineSegment(new PointF(TranslatePosition(shoulderCenter.Position.X), TranslatePosition(shoulderCenter.Position.Y)), new PointF(TranslatePosition((float)intersectingPoint.X), TranslatePosition((float)intersectingPoint.Y))));
            doc.Children.Add(path);


            doc.Children.Add(headCircle);
            var stream = new MemoryStream();
            doc.Write(stream);
            Console.WriteLine(Encoding.UTF8.GetString(stream.GetBuffer()));
            return Encoding.UTF8.GetString(stream.GetBuffer());
            
        }

        private void AddJointsToPath(SvgPath path, List<Joint> joints, int scale)
        {
            path.PathData.Add(new SvgMoveToSegment(new PointF(TranslatePosition(joints[0].Position.X), TranslatePosition(joints[0].Position.Y))));
            for (var i = 0; i < joints.Count - 1; i++)
            {
                var start = joints[i];
                var end = joints[i + 1];

                path.PathData.Add(new SvgLineSegment(new PointF(TranslatePosition(start.Position.X), TranslatePosition(start.Position.Y)), new PointF(TranslatePosition(end.Position.X), TranslatePosition(end.Position.Y))));
            }
        }

        private float TranslatePosition(float pos)
        {
            
            return svgHeight - ((pos + 1) * scale);

        }

        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);
 
            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;                    
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;                    
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        /// <summary>
        /// Draws a bone line between two joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw bones from</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="jointType0">joint to start drawing from</param>
        /// <param name="jointType1">joint to end drawing at</param>
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }

        /// <summary>
        /// Handles the checking or unchecking of the seated mode combo box
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void CheckBoxSeatedModeChanged(object sender, RoutedEventArgs e)
        {
            if (null != this.sensor)
            {
                if (this.checkBoxSeatedMode.IsChecked.GetValueOrDefault())
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                }
                else
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.sensor.SkeletonStream.IsEnabled)
            {
                
                foreach (Skeleton skel in skeletons)
                {
                    if (skel.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        Console.WriteLine("new skeleton" + skel.TrackingId.ToString());
                        String svgString = this.BuildSvg(skel);
                        this.SendSvg(svgString);
                    }
                }
                this.sensor.SkeletonStream.Disable();
                Console.WriteLine("all skeletons done" );
            }
            else
            {
                this.sensor.SkeletonStream.Enable();
            }
            
        }

        private void ButtonOpenBackgroundRemoval(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}*/