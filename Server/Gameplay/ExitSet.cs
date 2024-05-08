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
			var range = SideToPos(location.Size, pos.Side);

			// Check if pos is within range
			var posOnSide = new Vector2(pos.Pos.X, pos.Pos.Y);
			if (posOnSide.X < 0 || posOnSide.X >= range.X || posOnSide.Y < 0 || posOnSide.Y >= range.Y)
				throw new Exception($"Exit position {posOnSide} is out of range for side {pos.Side}. Range: {range}");

			exits.Add(pos, exit);
		}

		private static Vector2 SideToPos(Vector3 pos, Side side)
		{
			return side switch
			{
				Side.North => new Vector2(pos.X, 1),
				Side.East => new Vector2(pos.Y, 1),
				Side.South => new Vector2(pos.X, 1),
				Side.West => new Vector2(pos.Y, 1),
				Side.Up => new Vector2(pos.X, pos.Y),
				Side.Down => new Vector2(pos.X, pos.Y),
				_ => throw new Exception("Invalid side: " + side)
			};
		}
	}
}
