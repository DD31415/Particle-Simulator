using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class Particle
{
	public delegate void DrawDel(Particle sender, Graphics g);

	public event DrawDel PreDraw;
	public event DrawDel PostDraw;

	private ParticleSystem _Parent;
	
	private PointF _Location;
	private SizeF _Size = new SizeF(1f, 1f);
	private PointF _Heading;
	private float _Force;

	private Color _Color = Color.White;
	private Pen ColorPen = new Pen(Color.White, 1f);
	
	private float _Mass;
	private IMovement _Movement;
	private bool _Visible = true;


	public virtual ParticleSystem Parent
	{
		get
		{
			return _Parent;
		}
	}


	public virtual PointF Location
	{
		get
		{
			return _Location;
		}
		set
		{
			_Location = value;
		}
	}

	public virtual SizeF Size
	{
		get
		{
			return _Size;
		}
		set
		{
			_Size = value;
		}
	}

	public virtual PointF Heading
	{
		get
		{
			return _Heading;
		}
		set
		{
			_Heading = value;
		}
	}

	public virtual float Force
	{
		get
		{
			return _Force;
		}
		set
		{
			_Force = value;
		}
	}

	public virtual Color Color
	{
		get
		{
			return _Color;
		}
		set
		{
			_Color = value;
			this.ColorPen = new Pen(value, 1f);
		}
	}

	public virtual float Mass
	{
		get
		{
			return _Mass;
		}
		set
		{
			_Mass = value;
		}
	}

	public virtual RectangleF Bounds
	{
		get
		{
			return new RectangleF(_Location, _Size);
		}
	}

	public virtual IMovement Movement
	{
		get
		{
			return _Movement;
		}
		set
		{
			_Movement = value;
		}
	}

	public virtual bool Visible
	{
		get
		{
			return _Visible;
		}
		set
		{
			_Visible = value;
		}
	}

	public Particle(ParticleSystem pSystem)
	{
		_Parent = pSystem;
	}

	public virtual void Update(Graphics g)
	{
		if (this.Movement != null) this.Location = this.Movement.Translate();
	}

	public virtual void Draw(Graphics g)
	{
		if (this.Visible == true)
		{
			if (this.PreDraw != null) this.PreDraw(this, g);
		
			g.DrawRectangle(this.ColorPen, this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height);

			if (this.PostDraw != null) this.PostDraw(this, g);
		}
	}

	public void Dispose()
	{
		this.Parent.Particles.Remove(this);
	}
}