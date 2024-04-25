namespace RMUD3.Server
{
	public class HashSetWithEvents<T> : HashSet<T>
	{
		public delegate void ItemAddedEventHandler(T item);
		public delegate void ItemRemovedEventHandler(T item);

		public event ItemAddedEventHandler? OnAdd;
		public event ItemRemovedEventHandler? OnRemove;

		public new void Add(T item)
		{
			base.Add(item);
			OnAdd?.Invoke(item);
		}

		public new void Remove(T item)
		{
			base.Remove(item);
			OnRemove?.Invoke(item);
		}
	}
}
