using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class SimpleMovement : IMovement
{
	private Particle _Parent;

	public Particle Parent
	{
		get
		{
			return _Parent;
		}
		set
		{
			_Parent = value;
		}
	}

	public SimpleMovement(Particle parent)
	{
		this.Parent = parent;
	}

	public PointF Translate()
	{
		float x = this.Parent.Location.X + (this.Parent.Heading.X * this.Parent.Force);
		float y = this.Parent.Location.Y + (this.Parent.Heading.Y * this.Parent.Force);

		return new PointF(x, y);
	}
}