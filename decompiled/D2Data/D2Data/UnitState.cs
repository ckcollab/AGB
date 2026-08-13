using System.Text;

namespace D2Data;

public class UnitState
{
	public BaseState BaseState;

	public Stats Stats;

	public UnitState(int stateID)
	{
		BaseState = BaseState.Get(stateID);
		Stats = new Stats();
	}

	public UnitState(StateType stateType)
	{
		BaseState = BaseState.Get(stateType);
		Stats = new Stats();
	}

	public UnitState(BaseState state)
	{
		BaseState = state;
		Stats = new Stats();
	}

	public override string ToString()
	{
		if (Stats.Count == 0)
		{
			return BaseState.ToString();
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(BaseState);
		stringBuilder.Append(" (");
		int num = 0;
		while (true)
		{
			stringBuilder.Append(Stats[num].ToString());
			if (++num >= Stats.Count)
			{
				break;
			}
			stringBuilder.Append(", ");
		}
		stringBuilder.Append(")");
		return stringBuilder.ToString();
	}
}
