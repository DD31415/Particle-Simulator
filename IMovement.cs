using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public interface IMovement
{
	Particle Parent { get; set; }
	PointF Translate();
}