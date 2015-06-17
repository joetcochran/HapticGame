using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using Toolbox.NETMF.Hardware;
using System.Threading;

namespace HapticGame
{

    public class Program
    {
        //thread control variables
        static bool KillBaddieThread = false;
        static bool PauseMainThread = false;
        static bool PauseBaddieThread = false;

        //accelerometer variables
        static int XAccel;
        static int YAccel;
        static int XMidval = 0;
        static int YMidval = 0;
        static AnalogInput XInput;
        static AnalogInput YInput;
        static AnalogInput ZInput;
        static Netduino.PWM PWMSpeaker;

        static PWM LeftVibe;
        static PWM RightVibe;

        static double BaddieDistance = 500;
        static double GoalDistanceAsPct = 100;
        static bool Dead = false;
        static bool Goal = false;

        static double BaddieSpeed = 0;
        static int MAX_X = 1000;
        static int MAX_Y = 1000;
        static Entity p = new Entity();
        static Entity g = new Entity();
        static Entity b = new Entity();
        static int CurrentLevel = 1;

        public class Location
        {
            public double Left;
            public double Top;
            public double Width;
            public double Height;
        }

        public class Point
        {
            public Point(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }

            public double X;
            public double Y;
        }

        public class Entity
        {
            public Location Location = new Location();
        }
        
        private static void SpeakerThread()
        {
            if (CurrentLevel == 1)
                return;

            
            while (true)
            {
                if (PauseBaddieThread)
                {
                    Thread.Sleep(100);
                    continue;
                }            

                if (KillBaddieThread) 
                {
                    KillBaddieThread = false;
                    return;
                }

                SpeakerLib.PCSpeaker.Beep();
                
                if (BaddieDistance <= 5)
                {
                    Dead = true;
                    
                    SpeakerLib.DeadTune();
                    return;
                }
                
                if (BaddieDistance < 400)
                    Thread.Sleep((int)BaddieDistance * 8);
                else if (BaddieDistance < 300)
                    Thread.Sleep((int)BaddieDistance * 7);
                else if (BaddieDistance < 200)
                    Thread.Sleep((int)BaddieDistance * 6);
                else
                    Thread.Sleep((int)BaddieDistance * 9);

            }
        }

        private static void VibeThread()
        {
            if (Dead)
                return;

            uint pulse = 0;

            while (true)
            {
                Thread.Sleep(100);

                if (GoalDistanceAsPct > 95)
                {
                    LeftVibe.SetPulse(20000, 0);
                    RightVibe.SetPulse(20000, 0);
                }
                else if (GoalDistanceAsPct <= 5)
                {
                    LeftVibe.SetPulse(20000, 0);
                    RightVibe.SetPulse(20000, 0);
                    Goal = true;
                    PauseMainThread = true;
                    PauseBaddieThread = true;
                    if (CurrentLevel == 1)
                        SpeakerLib.WinTone(SpeakerLib.WinToneType.CUSHING);
                    else if (CurrentLevel == 2)
                        SpeakerLib.WinTone(SpeakerLib.WinToneType.CELEBRATION);
                    else if (CurrentLevel == 3)
                        SpeakerLib.WinTone(SpeakerLib.WinToneType.BANGONTHEDRUMS);
                    else if (CurrentLevel == 4)
                        SpeakerLib.WinTone(SpeakerLib.WinToneType.EYEOFTHETIGER);
                    else if (CurrentLevel == 5)
                        SpeakerLib.WinTone(SpeakerLib.WinToneType.FINALCOUNTDOWN);
                    else if (CurrentLevel == 6)
                    {
                        SpeakerLib.WinTone(SpeakerLib.WinToneType.CHAMPIONS);
                        return; 
                    }
                    PauseBaddieThread = false;
                    PauseMainThread = false;
                    CurrentLevel++;
                    return;
                }
                else
                {
                    //range: 5000 to 15000
                    pulse = 5000 + ((100 - (uint)GoalDistanceAsPct) * 100);
                    LeftVibe.SetPulse(20000, pulse);
                    RightVibe.SetPulse(20000, pulse);
                }

                
            }
        }

    
        public static void InitializeLevel()
        {
            PauseMainThread = true;
            Dead = false;
            
            if (CurrentLevel >= 3)
                KillBaddieThread = true;

            GoalDistanceAsPct = 100;

            p.Location.Left = MAX_X / 2.0;
            p.Location.Top = MAX_Y / 2.0;


            if (CurrentLevel == 1)
            {
                BaddieSpeed = 0;
                MAX_X = 1000;
                MAX_Y = 1000;
            }
            if (CurrentLevel == 2)
            {
                BaddieSpeed = 0;
                MAX_X = 1100;
                MAX_Y = 1100;
            }
            if (CurrentLevel == 3)
            {
                BaddieSpeed = 0.25;
                MAX_X = 1200;
                MAX_Y = 1200;
            }
            if (CurrentLevel == 4)
            {
                BaddieSpeed = 0.5;
                MAX_X = 1300;
                MAX_Y = 1300;
            }
            if (CurrentLevel == 5)
            {
                BaddieSpeed = 1;
                MAX_X = 1400;
                MAX_Y = 1400;
            }
            if (CurrentLevel == 6)
            {
                BaddieSpeed = 2.0;
                MAX_X = 1500;
                MAX_Y = 1500;
            }
            p.Location.Width = 20;
            p.Location.Height = 20;

            g.Location.Width = 3;
            g.Location.Height = 3;

            b.Location.Width = 5;
            b.Location.Height = 5;

            System.Random r = new System.Random();
            g.Location.Left = MAX_X * r.NextDouble() - g.Location.Width;
            g.Location.Top = MAX_Y * r.NextDouble() - g.Location.Height;
            if (g.Location.Left < 0) g.Location.Left = 0;
            if (g.Location.Top < 0) g.Location.Top = 0;

            b.Location.Left = MAX_X * r.NextDouble() - b.Location.Width;
            b.Location.Top = MAX_Y * r.NextDouble() - b.Location.Height;
            if (b.Location.Left < 0) b.Location.Left = 0;
            if (b.Location.Top < 0) b.Location.Top = 0;

            
            SpeakerLib.StartLevel();
            XMidval = (XMidval == 0 ? XInput.Read() : (XMidval + XInput.Read()) / 2);
            YMidval = (YMidval == 0 ? YInput.Read() : (YMidval + YInput.Read()) / 2);

            Goal = false;
            PauseMainThread = false;

            ThreadStart delegateVibeWorkerMain = new ThreadStart(VibeThread);
            Thread VibeThreadWorker = new Thread(delegateVibeWorkerMain);
            VibeThreadWorker.Start();

            if (CurrentLevel > 1)
            {
                ThreadStart delegateSpeakerWorkerMain = new ThreadStart(SpeakerThread);
                Thread SpeakerThreadWorker = new Thread(delegateSpeakerWorkerMain);
                SpeakerThreadWorker.Start();
            }
        }

        public static void Main()
        {


            XInput = new AnalogInput(Pins.GPIO_PIN_A0);
            YInput = new AnalogInput(Pins.GPIO_PIN_A1);
            ZInput = new AnalogInput(Pins.GPIO_PIN_A2);
            PWMSpeaker = new Netduino.PWM(Pins.GPIO_PIN_D9);
            LeftVibe = new PWM(Pins.GPIO_PIN_D5);
            RightVibe = new PWM(Pins.GPIO_PIN_D6);

            SpeakerLib.PCSpeaker = new Speaker(PWMSpeaker);

            //set up initial level parameters
            InitializeLevel();
            

            // initialize one of the digital pins (that support PWM!) for PWM         
            int maxDistance = CalculateDistance(new Point(0, 0), new Point(MAX_X, MAX_Y));

            while (true)
            {
                if (PauseMainThread)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }

                Debug.Print("player: " + p.Location.Left.ToString() + ", " + p.Location.Top.ToString());
                Debug.Print("goal: " + g.Location.Left.ToString() + ", " + g.Location.Top.ToString());

                //set up the next level
                if (Goal == true)
                {
                    InitializeLevel();
                }
                //sleep the thread so that its slow enough for humans
                Thread.Sleep(100);

                //read acceletometers
                int XSpeed = ReadXAccelerometer();
                int YSpeed = ReadYAccelerometer();

                //make sure player won't go off the field of play
                if (p.Location.Left + XSpeed < 0)
                    XSpeed = 0;
                if (p.Location.Top + YSpeed < 0)
                    YSpeed = 0;
                if (p.Location.Top + YSpeed > MAX_Y)
                    YSpeed = 0;
                if (p.Location.Left + XSpeed > MAX_X)
                    XSpeed = 0;

                //move the player on the field
                p.Location.Left = p.Location.Left + XSpeed;
                p.Location.Top = p.Location.Top + YSpeed;

                //calculate the new distance from the goal
                double GoalDistance = CalculateDistance(new Point(p.Location.Top + p.Location.Height / 2, p.Location.Left + p.Location.Width / 2),
                                                              new Point(g.Location.Top + g.Location.Height / 2, g.Location.Left + g.Location.Width / 2));
                
                if (GoalDistance < 500)                
                    GoalDistanceAsPct = 100 - (((500.00 - GoalDistance) / 500.00) * 100);
                else
                    GoalDistance = 0;

                //move the baddie towards the player
                double BaddieXDirection = 0;
                double BaddieYDirection = 0;
                if (b.Location.Top < p.Location.Top)
                    BaddieYDirection = 1;
                else if (b.Location.Top > p.Location.Top)
                    BaddieYDirection = -1;

                if (b.Location.Left < p.Location.Left)
                    BaddieXDirection = 1;
                else if (b.Location.Left > p.Location.Left)
                    BaddieXDirection = -1;

                b.Location.Left = b.Location.Left + (BaddieXDirection * BaddieSpeed);
                b.Location.Top = b.Location.Top + (BaddieYDirection * BaddieSpeed);

                //calculate distance of baddie from player
                BaddieDistance = CalculateDistance(new Point(p.Location.Top + p.Location.Height / 2, p.Location.Left + p.Location.Width / 2),
                                                  new Point(b.Location.Top + b.Location.Height / 2, b.Location.Left + b.Location.Width / 2));

            }

        }

        
        private static int ReadXAccelerometer()
        {
            
            XAccel = XInput.Read();
            //Debug.Print("X: " + XAccel.ToString());
            int DistilledValue = -4;

            if (XAccel > XMidval + 70)
                DistilledValue = 4;
            else if (XAccel > XMidval + 50)
                DistilledValue = 3;
            else if (XAccel > XMidval + 30)
                DistilledValue = 2;
            else if (XAccel > XMidval + 10)
                DistilledValue = 1;
            else if (XAccel > XMidval - 10)
                DistilledValue = 0;
            else if (XAccel > XMidval - 30)
                DistilledValue = -1;
            else if (XAccel > XMidval - 50)
                DistilledValue = -2;
            else if (XAccel > XMidval - 70)
                DistilledValue = -3;
            else if (XAccel > XMidval - 90)
                DistilledValue = -4;


            return DistilledValue;
        }


        private static int ReadYAccelerometer()
        {
            YAccel = YInput.Read();
            
            //Debug.Print("Y: " + YAccel.ToString());
            int DistilledValue = -4;

            if (YAccel > YMidval + 70)
                DistilledValue = -4;
            else if (YAccel > YMidval + 50)
                DistilledValue = -3;
            else if (YAccel > YMidval + 30)
                DistilledValue = -2;
            else if (YAccel > YMidval + 10)
                DistilledValue = -1;
            else if (YAccel > YMidval - 10)
                DistilledValue = 0;
            else if (YAccel > YMidval - 30)
                DistilledValue = 1;
            else if (YAccel > YMidval - 50)
                DistilledValue = 2;
            else if (YAccel > YMidval - 70)
                DistilledValue = 3;
            else if (YAccel > YMidval - 90)
                DistilledValue = 4;

            return DistilledValue;
        }

        private static int CalculateDistance(Point pt1, Point pt2)
        {
            long dist1 = (int)pt1.X - (int)pt2.X;
            if (dist1 < 0)
                dist1 = dist1 * -1;
            long dist2 = (int)pt1.Y - (int)pt2.Y;
            if (dist2 < 0)
                dist2 = dist2 * -1;
            dist1 = dist1 * dist1;
            dist2 = dist2 * dist2;



            double Distance =
                System.Math.Pow((double)(dist1 + dist2), 0.5);
            return (int)Distance;

        }


    }
}
