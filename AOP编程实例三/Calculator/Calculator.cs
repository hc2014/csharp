using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class Calculator
    {
        private double _x;
        private double _y;

        public Calculator()
        { }

        public Calculator(double x, double y)
        {
            this._x = x;
            this._y = y;
        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }
    }
}
