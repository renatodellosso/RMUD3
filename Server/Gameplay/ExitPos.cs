namespace RMUD3.Server.Gameplay
{
	public enum Side
	{
		North,
		East,
		South,
		West,
		Up,
		Down
	}

	public record ExitPos(Side Side, int Pos);
}
