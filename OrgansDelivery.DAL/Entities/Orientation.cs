using Microsoft.EntityFrameworkCore;

namespace OrganStorage.DAL.Entities;

[Owned]
public class Orientation : IEquatable<Orientation>
{
    public Orientation()
    {
    }

    public Orientation(decimal x, decimal y)
    {
        X = x;
        Y = y;
    }

    // x & y rotation limits in degrees
    public decimal X { get; set; }
    public decimal Y { get; set; }

	public bool Equals(Orientation orientation)
	{
		return X == orientation.X && Y == orientation.Y;
	}
}
