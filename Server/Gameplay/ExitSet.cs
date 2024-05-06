namespace RMUD3.Server.Gameplay
{
	public class ExitSet(Location location)
	{
		private readonly Location location = location;

		private readonly Dictionary<ExitPos, Exit> exits = [];

		public void Add(ExitPos pos, Exit exit)
		{
			if (exits.ContainsKey(pos))
				throw new Exception($"Exit already exists at position {pos}.");

			// Parse exit side to determine range for pos
			var range = pos.Side switch
			{
				Side.North => new Vector2(location.Size.X, location.Size.Z),
				Side.East => new Vector2(location.Size.Z, location.Size.Y),
				Side.South => new Vector2(location.Size.X, location.Size.Z),
				Side.West => new Vector2(location.Size.Z, location.Size.Y),
				Side.Up => new Vector2(location.Size.X, location.Size.Y),
				Side.Down => new Vector2(location.Size.X, location.Size.Y),
				_ => throw new Exception("Invalid side.")
			};

			// Check if pos is within range
			if (pos.Pos.X < 0 || pos.Pos.X >= range.X || pos.Pos.Y < 0 || pos.Pos.Y >= range.Y)
				throw new Exception($"Exit position {pos} is out of range for side {pos.Side}.");

			exits.Add(pos, exit);
		}
	}
}
