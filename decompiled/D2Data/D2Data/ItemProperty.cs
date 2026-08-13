namespace D2Data;

public class ItemProperty
{
	public readonly BaseProperty Base;

	public readonly int Param;

	public readonly string SParam;

	public readonly int Min;

	public readonly int Max;

	public ItemProperty(string code, int Param, int Min, int Max)
	{
		Base = BaseProperty.GetByCode(code);
		this.Param = Param;
		this.Min = Min;
		this.Max = Max;
	}

	public ItemProperty(BaseProperty Base, int Param, int Min, int Max)
	{
		this.Base = Base;
		this.Param = Param;
		this.Min = Min;
		this.Max = Max;
	}

	public ItemProperty(string code, string SParam, int Min, int Max)
	{
		Base = BaseProperty.GetByCode(code);
		this.SParam = SParam;
		Param = -1;
		this.Min = Min;
		this.Max = Max;
	}

	public ItemProperty(BaseProperty Base, string SParam, int Min, int Max)
	{
		this.Base = Base;
		this.SParam = SParam;
		Param = -1;
		this.Min = Min;
		this.Max = Max;
	}

	public ItemProperty(string code, string SParam, int Param, int Min, int Max)
	{
		Base = BaseProperty.GetByCode(code);
		this.SParam = SParam;
		this.Param = Param;
		this.Min = Min;
		this.Max = Max;
	}

	public ItemProperty(BaseProperty Base, string SParam, int Param, int Min, int Max)
	{
		this.Base = Base;
		this.SParam = SParam;
		this.Param = Param;
		this.Min = Min;
		this.Max = Max;
	}
}
