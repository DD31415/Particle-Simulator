using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

public class SampleParticleSystem : ParticleSystem
{
	public override Scene Parent
	{
		get
		{
			return base.Parent;
		}
	}
	
	public override List<Particle> Particles
	{
		get
		{
			return base.Particles;
		}
	}
	
	public SampleParticleSystem(Scene parent) : base(parent) {}
	public SampleParticleSystem(Scene parent, int ParticleCount) : base(parent)
	{
		this.CreateParticles(ParticleCount);
	}

	public override void CreateParticles(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Particle p = this.CreateParticle();


			this.Particles.Add(p);
		}
	}

	public override Particle CreateParticle()
	{
		Particle p = new Particle(this);

		p.Location = this.RandomLocation();
		p.Heading = RandomHeading();
		p.Color = this.RandomColor();
		p.Movement = new SimpleMovement(p);
		p.Size = new SizeF(1f, 1f);
		p.PreDraw += Particle_PreDraw;
		p.Force = (float)rnd.NextDouble() * 2f;

		return p;
	}

	private void Particle_PreDraw(Particle sender, Graphics g)
	{
		this.CheckBounds(sender);
	}

	private void CheckBounds(Particle p)
	{
		float Width = (float)this.Parent.Client.ClientSize.Width;
		float Height = (float)this.Parent.Client.ClientSize.Height;

		float x = p.Location.X + p.Size.Width;
		float y = p.Location.Y + p.Size.Height;

		if (x < 0 || x > Width) p.Heading = this.NegateX(p.Heading);
		if (y < 0 || y > Height) p.Heading = this.NegateY(p.Heading);

		if (x < 0 && y > Height) p.Heading = this.Negate(p.Heading);
		if (x > Width && y < 0) p.Heading = this.Negate(p.Heading);
	}

	private PointF Negate(PointF p)
	{
		return new PointF(-p.X, -p.Y);
	}

	private PointF NegateX(PointF p)
	{
		return new PointF(-p.X, p.Y);
	}

	private PointF NegateY(PointF p)
	{
		return new PointF(p.X, -p.Y);
	}

	private Random rnd = new Random();

	private Color RandomColor()
	{
		return Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
	}

	private PointF RandomLocation()
	{
		float x = (float)rnd.NextDouble() * this.Parent.Client.ClientSize.Width;
		float y = (float)rnd.NextDouble() * this.Parent.Client.ClientSize.Height;

		return new PointF(x, y);
	}

	private PointF RandomHeading()
	{
		float x = ((float)rnd.NextDouble() * 2) - 1f;
		float y = ((float)rnd.NextDouble() * 2) - 1f;

		return new PointF(x, y);
	}
}