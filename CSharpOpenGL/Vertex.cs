using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpOpenGL
{
	public class Vertex
	{
                public Vertex(int x, int y)
                {
                        this.X = x;
                        this.Y = y;
                }
                public int X { get; set; }
                public int Y { get; set; }
                public VertexType Type { get; set; }
	}
}
