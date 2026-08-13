using System.Collections.Generic;

namespace AGB;

public class InfixToPostfix
{
	public static List<Op> Convert(string requirements)
	{
		List<Op> postfix = new List<Op>();
		string operand = "";
		string op = "";
		for (int i = 0; i < requirements.Length; i++)
		{
			if (requirements[i] == ' ')
			{
				continue;
			}
			if (char.IsLetterOrDigit(requirements[i]))
			{
				operand += requirements[i];
				if (op != "")
				{
					postfix.Add(new Op(OpType.Operator, op));
					op = "";
				}
				continue;
			}
			if (operand != "")
			{
				postfix.Add(new Op(OpType.Operand, operand));
				operand = "";
			}
			op += requirements[i];
			if (op == ")" || op == "(")
			{
				postfix.Add(new Op(OpType.Operator, op));
				op = "";
			}
		}
		if (operand != "")
		{
			postfix.Add(new Op(OpType.Operand, operand));
		}
		if (op != "")
		{
			postfix.Add(new Op(OpType.Operator, op));
		}
		Stack<Op> stack = new Stack<Op>();
		List<Op> infix = new List<Op>();
		foreach (Op o in postfix)
		{
			if (o.Type == OpType.Operator)
			{
				while (stack.Count > 0 && stack.Peek().Precedence >= o.Precedence)
				{
					infix.Add(stack.Pop());
				}
				stack.Push(o);
			}
			else
			{
				infix.Add(o);
			}
		}
		while (stack.Count > 0)
		{
			infix.Add(stack.Pop());
		}
		infix.RemoveAll((Op tempOp) => tempOp.Type == OpType.Operator && (tempOp.Value == "(" || tempOp.Value == ")"));
		return infix;
	}
}
