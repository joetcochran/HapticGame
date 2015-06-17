using System;
using Microsoft.SPOT;
using Toolbox.NETMF.Hardware;
using System.Threading;

namespace HapticGame
{
    static class SpeakerLib
    {
            public static Speaker PCSpeaker;
            public enum WinToneType
            {
                CUSHING,
                CELEBRATION,
                BANGONTHEDRUMS,
                EYEOFTHETIGER,
                FINALCOUNTDOWN,
                CHAMPIONS
            }
            public static void Beep()
            {
                PCSpeaker.Sound(1200, 0.25f);                
            }

            public static void DeadTune()
            {
            // Initializes the speaker
            //Netduino.PWM pwm = new Netduino.PWM(Pins.GPIO_PIN_D9);
            //Speaker PCSpeaker = new Speaker(pwm);

             //Produces a 440 hertz tone for 5 ticks
            PCSpeaker.Sound(440, 9);
            Thread.Sleep(20);
            PCSpeaker.Sound(440, 6);
            Thread.Sleep(20);
            PCSpeaker.Sound(440, 3);
            Thread.Sleep(20);
            PCSpeaker.Sound(440, 9);
            Thread.Sleep(20);
            PCSpeaker.Sound(523, 6);
            Thread.Sleep(20);
            PCSpeaker.Sound(494, 3);
            Thread.Sleep(20);
            PCSpeaker.Sound(494, 6);
            Thread.Sleep(20);
            PCSpeaker.Sound(440, 3);
            Thread.Sleep(20);
            PCSpeaker.Sound(440, 6);
            Thread.Sleep(20);
            PCSpeaker.Sound(420, 3);
            Thread.Sleep(20);
            PCSpeaker.Sound(440, 9);
            }

        public static void StartLevel()
        {
            Thread.Sleep(2000);

            PCSpeaker.Sound(523, 6);
            Thread.Sleep(500);
            PCSpeaker.Sound(523, 6);
            Thread.Sleep(500);
            PCSpeaker.Sound(523, 6);
            Thread.Sleep(500);
            PCSpeaker.Sound(1000, 12);

        }

        public static void WinTone(WinToneType type)
        {
            if (type == WinToneType.CUSHING)
            {
                //generic win
                PCSpeaker.Sound(523, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(523, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 5);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 1.5f);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 1.5f);
                Thread.Sleep(20);
                PCSpeaker.Sound(699, 1.5f);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 1.5f);
                //Thread.Sleep(20);
                //PCSpeaker.Sound(587, 1);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(988, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(988, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(1047, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 3);
                Thread.Sleep(20);
                PCSpeaker.Sound(1047, 6);
                Thread.Sleep(20);
            }
            if (type == WinToneType.CELEBRATION)
            {
                PCSpeaker.Sound(794, 15);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(794, 6);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 6);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 12);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 5);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 5);
                Thread.Sleep(20);

            }
            if (type == WinToneType.BANGONTHEDRUMS)
            {
                PCSpeaker.Sound(740, 16);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 4);
                Thread.Sleep(20);
                Thread.Sleep(600);
                PCSpeaker.Sound(587, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(740, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 4);
                Thread.Sleep(20);

            }
            if (type == WinToneType.EYEOFTHETIGER)
            {
                PCSpeaker.Sound(784, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 4);
                Thread.Sleep(250);
                PCSpeaker.Sound(932, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 6);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(699, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(699, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(932, 6);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(699, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(784, 12);
                Thread.Sleep(20);

            }

            if (type == WinToneType.FINALCOUNTDOWN)
            {
                PCSpeaker.Sound(659, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(440, 8);
                Thread.Sleep(600);
                PCSpeaker.Sound(699, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(699, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 8);
                Thread.Sleep(600);
                PCSpeaker.Sound(699, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(659, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(699, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(440, 8);
                Thread.Sleep(600);
                PCSpeaker.Sound(587, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(523, 2);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(523, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(494, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(587, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(523, 8);
                Thread.Sleep(20);

            }

            if (type == WinToneType.CHAMPIONS)
            {
                PCSpeaker.Sound(1397, 16);
                Thread.Sleep(20);
                PCSpeaker.Sound(1319, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(1397, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(1319, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(1047, 12);
                Thread.Sleep(200);
                PCSpeaker.Sound(880, 4);
                Thread.Sleep(20);
                PCSpeaker.Sound(1175, 8);
                Thread.Sleep(20);
                PCSpeaker.Sound(880, 20);
                Thread.Sleep(20);
            }
        }
    }
}

