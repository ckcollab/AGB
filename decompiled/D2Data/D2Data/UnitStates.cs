using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace D2Data;

public class UnitStates : Collection<UnitState>
{
	public UnitState this[StateType state]
	{
		get
		{
			using (IEnumerator<UnitState> enumerator = GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UnitState current = enumerator.Current;
					if (current.BaseState.Type == state)
					{
						return current;
					}
				}
			}
			return null;
		}
	}

	public bool Contains(StateType stateType)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseState.Type == stateType)
			{
				return true;
			}
		}
		return false;
	}

	public void Set(StateType stateType)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseState.Type == stateType)
			{
				base[i].Stats.Clear();
				return;
			}
		}
		Add(new UnitState(stateType));
	}

	public void Set(BaseState baseState)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseState.Type == baseState.Type)
			{
				base[i].Stats.Clear();
				return;
			}
		}
		Add(new UnitState(baseState));
	}

	public void Set(UnitState state)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseState.Type == state.BaseState.Type)
			{
				base[i].Stats = state.Stats;
				return;
			}
		}
		Add(state);
	}

	public void Remove(StateType state)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseState.Type == state)
			{
				RemoveAt(i);
				break;
			}
		}
	}

	public void Remove(BaseState state)
	{
		for (int i = 0; i < base.Count; i++)
		{
			if (base[i].BaseState.Type == state.Type)
			{
				RemoveAt(i);
				break;
			}
		}
	}
}
