using System;

namespace AGB;

public class Op
{
	public OpType Type;

	public string Value;

	public int Precedence;

	public Op()
	{
	}

	public Op(OpType type, string value)
	{
		Type = type;
		Value = value;
		if (Type == OpType.Operator)
		{
			switch (Value)
			{
			case "==":
				Precedence = 5;
				break;
			case ">=":
				Precedence = 5;
				break;
			case "<=":
				Precedence = 5;
				break;
			case ">":
				Precedence = 5;
				break;
			case "<":
				Precedence = 5;
				break;
			case "(":
				Precedence = 2;
				break;
			case ")":
				Precedence = 1;
				break;
			}
		}
	}

	public bool Evaluate(int operand1, int operand2)
	{
		if (Type != OpType.Operator)
		{
			throw new Exception("OpType is operand, not operator!");
		}
		return Value switch
		{
			"==" => operand1 == operand2, 
			"&&" => operand1 > 0 && operand2 > 0, 
			"||" => operand1 > 0 || operand2 > 0, 
			">=" => operand1 >= operand2, 
			"<=" => operand1 <= operand2, 
			">" => operand1 > operand2, 
			"<" => operand1 < operand2, 
			_ => throw new Exception("Operator value " + Value + " doesn't exist"), 
		};
	}
}
