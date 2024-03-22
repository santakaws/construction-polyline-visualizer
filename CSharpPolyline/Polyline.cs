using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPolyline
{
    internal class Polyline
    {
        private List<Line> line_segments = new List<Line>();
        
        public Polyline() { }

        public void AddLine(Line line) { 
            line_segments.Add(line);
        }

        public List<Line> LineSegments
        {
            get { return line_segments; }
        }
    }
}
