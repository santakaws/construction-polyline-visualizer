using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPolyline
{
    internal class Line
    {
        private double start_node;
        private double end_node;

        public Line(double start_node, double end_node)
        {
            this.start_node = start_node;
            this.end_node = end_node;
        }

        public double StartNode { 
            get { return start_node; } 
        }

        public double EndNode { 
            get { return end_node; } 
        }
    }
}
