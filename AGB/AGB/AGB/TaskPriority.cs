namespace AGB;

public enum TaskPriority
{
	Base = 0,
	AboveNormal = 512,
	High = 1024,
	RealTime = int.MaxValue
}
