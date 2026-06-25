using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_FormUI.Render
{
    public class AnimateBase
    {
        public static double Back(double x, LEaseMode easeMode)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = (((2.70158 * x) * x) * x) - ((1.70158 * x) * x);
                    break;

                case LEaseMode.EaseOut:
                    num = (1.0 + (2.70158 * Math.Pow(x - 1.0, 3.0))) + (1.70158 * Math.Pow(x - 1.0, 2.0));
                    break;

                case LEaseMode.EaseInOut:
                    num = (x < 0.5) ? ((Math.Pow(2.0 * x, 2.0) * ((7.189819 * x) - 2.5949095)) / 2.0) : (((Math.Pow((2.0 * x) - 2.0, 2.0) * ((7.189819 * x) - 4.5949095)) + 2.0) / 2.0);
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }

        public static double Bounce(double x, LEaseMode easeMode)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = 1.0 - BounceOut(1.0 - x);
                    break;

                case LEaseMode.EaseOut:
                    num = BounceOut(x);
                    break;

                case LEaseMode.EaseInOut:
                    num = (x < 0.5) ? ((1.0 - BounceOut(1.0 - (2.0 * x))) / 2.0) : ((1.0 + BounceOut((2.0 * x) - 1.0)) / 2.0);
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }

        private static double BounceOut(double x)
        {
            double num;
            if (x < 0.36363636363636365)
            {
                num = (7.5625 * x) * x;
            }
            else if (x < 0.72727272727272729)
            {
                double num1 = x - 0.54545454545454541;
                num = ((7.5625 * (x = num1)) * x) + 0.75;
            }
            else if (x < 0.90909090909090906)
            {
                double num2 = x - 0.81818181818181823;
                num = ((7.5625 * (x = num2)) * x) + 0.9375;
            }
            else
            {
                double num3 = x - 0.95454545454545459;
                num = ((7.5625 * (x = num3)) * x) + 0.984375;
            }
            return num;
        }

        public static double Circle(double x, LEaseMode easeMode)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = 1.0 - Math.Sqrt(1.0 - Math.Pow(x, 2.0));
                    break;

                case LEaseMode.EaseOut:
                    num = Math.Sqrt(1.0 - Math.Pow(x - 1.0, 2.0));
                    break;

                case LEaseMode.EaseInOut:
                    num = (x < 0.5) ? ((1.0 - Math.Sqrt(1.0 - Math.Pow(2.0 * x, 2.0))) / 2.0) : ((Math.Sqrt(1.0 - Math.Pow((-2.0 * x) + 2.0, 2.0)) + 1.0) / 2.0);
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }

        public static double Cubic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 3);
        }

        public static double Elastic(double x, LEaseMode easeMode)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = (x == 0.0) ? 0.0 : ((x == 1.0) ? 1.0 : (-Math.Pow(2.0, (10.0 * x) - 10.0) * Math.Sin(((((x * 10.0) - 10.75) * 2.0) * 3.1415926535897931) / 3.0)));
                    break;

                case LEaseMode.EaseOut:
                    num = (x == 0.0) ? 0.0 : ((x == 1.0) ? 1.0 : ((Math.Pow(2.0, -10.0 * x) * Math.Sin(((((x * 10.0) - 0.75) * 2.0) * 3.1415926535897931) / 3.0)) + 1.0));
                    break;

                case LEaseMode.EaseInOut:
                    num = (x == 0.0) ? 0.0 : ((x == 1.0) ? 1.0 : ((x < 0.5) ? (-(Math.Pow(2.0, (20.0 * x) - 10.0) * Math.Sin(((((x * 20.0) - 11.125) * 2.0) * 3.1415926535897931) / 4.5)) / 2.0) : (((Math.Pow(2.0, (-20.0 * x) + 10.0) * Math.Sin(((((x * 20.0) - 11.125) * 2.0) * 3.1415926535897931) / 4.5)) / 2.0) + 1.0)));
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }

        public static double Exponential(double x, LEaseMode easeMode)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = (x == 0.0) ? 0.0 : Math.Pow(2.0, (10.0 * x) - 10.0);
                    break;

                case LEaseMode.EaseOut:
                    num = (x == 1.0) ? 1.0 : (1.0 - Math.Pow(2.0, -10.0 * x));
                    break;

                case LEaseMode.EaseInOut:
                    num = (x == 0.0) ? 0.0 : ((x == 1.0) ? 1.0 : ((x < 0.5) ? (Math.Pow(2.0, (20.0 * x) - 10.0) / 2.0) : ((2.0 - Math.Pow(2.0, (-20.0 * x) + 10.0)) / 2.0)));
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }

        public static double Power(double x, LEaseMode easeMode, int power)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = Math.Pow(x, (double)power);
                    break;

                case LEaseMode.EaseOut:
                    num = 1.0 - Math.Pow(1.0 - x, (double)power);
                    break;

                case LEaseMode.EaseInOut:
                    num = (x < 0.5) ? (Math.Pow(2.0, (double)(power - 1)) * Math.Pow(x, (double)power)) : (1.0 - (Math.Pow((-2.0 * x) + 2.0, (double)power) / 2.0));
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }

        public static double Quadratic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 2);
        }

        public static double Quartic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 4);
        }

        public static double Quintic(double x, LEaseMode easeMode)
        {
            return Power(x, easeMode, 5);
        }

        public static double Sine(double x, LEaseMode easeMode)
        {
            double num;
            switch (easeMode)
            {
                case LEaseMode.EaseIn:
                    num = 1.0 - Math.Cos((x * 3.1415926535897931) / 2.0);
                    break;

                case LEaseMode.EaseOut:
                    num = Math.Sin((x * 3.1415926535897931) / 2.0);
                    break;

                case LEaseMode.EaseInOut:
                    num = -(Math.Cos(3.1415926535897931 * x) - 1.0) / 2.0;
                    break;

                default:
                    num = 0.0;
                    break;
            }
            return num;
        }
    }
}
