using System;
using System.Collections.Generic;

using D2Data;

namespace AGB.D2.Modules
{
    public enum PickItResult
    {
        None,
        Sell,
        Keep,
        Identify,
        DoNotLog
    }

    public class PickItRequirement
    {
        public string Stats;
        public string Mods;

        public List<Op> StatOps;
        public List<Op> ModOps;

        public string Description;

        public ItemClass ItemClass;
        public ItemType ItemType;

        public ItemQuality Quality;

        public PickItResult Result;

        /// <summary>
        /// Parameterless constructor just for XML serializing
        /// </summary>
        public PickItRequirement()
        {
            
        }

        public PickItRequirement(PickItResult result, string stats, string mods)
        {
            Stats = stats;
            Mods = mods;

            Result = result;
        }

        /// <summary>
        /// Only call this if you called the parameterless constructor and have set
        /// stats/mods and such
        /// </summary>
        public void BuildOps()
        {
            if (Stats != null && Stats != "")
            {
                StatOps = InfixToPostfix.Convert(Stats);

                // Make sure all of the keywords in Requirements are _real_
                foreach (Op op in StatOps)
                {
                    int value;
                    if (op.Type == OpType.Operand && !Item.IsStatKeyword(op.Value) && !Int32.TryParse(op.Value, out value))
                        throw new ArgumentException("Item requirements keyword is invalid: " + op.Value);
                }
            }

            if (Mods != null && Mods != "")
            {
                ModOps = InfixToPostfix.Convert(Mods);

                foreach (Op op in ModOps)
                {
                    int value;
                    if (op.Type == OpType.Operand && !Item.IsStatKeyword(op.Value) && !Int32.TryParse(op.Value, out value))
                        throw new ArgumentException("Item requirements keyword is invalid: " + op.Value);
                }
            }
        }
    }

    public class PickItEvaluator
    {
        private Dictionary<ItemType, List<PickItRequirement>> TypeRequirements;
        private Dictionary<ItemClass, List<PickItRequirement>> ClassRequirements;

        public PickItEvaluator()
        {
            TypeRequirements = new Dictionary<ItemType, List<PickItRequirement>>();
            ClassRequirements = new Dictionary<ItemClass, List<PickItRequirement>>();
        }

        public void AddRequirements(PickItRequirement requirement)
        {
            // Are we going by item type (like Gloves) or by class (like Bramble Mitts)
            if (requirement.ItemType != 0)
            {
                if (!TypeRequirements.ContainsKey(requirement.ItemType))
                    TypeRequirements.Add(requirement.ItemType, new List<PickItRequirement>());

                TypeRequirements[requirement.ItemType].Add(requirement);
            }
            else
            {
                if (!ClassRequirements.ContainsKey(requirement.ItemClass))
                    ClassRequirements.Add(requirement.ItemClass, new List<PickItRequirement>());

                ClassRequirements[requirement.ItemClass].Add(requirement);
            }
        }

        /// <summary>
        /// Evaluates an items stats and mods to figure out if it's worth keeping, based on your requirements
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PickItResult Evaluate(Item item)
        {
            if (item.IsIdentified)
            {
                // Since this item is identified, let's get nasty with stats n such

                if (TypeRequirements.ContainsKey(item.Action.BaseItem.BaseType.Type))
                {
                    foreach (PickItRequirement req in TypeRequirements[item.Action.BaseItem.BaseType.Type])
                    {
                        if (item.Action.Quality == req.Quality)
                        {
                            if (Evaluate(req.StatOps, item.SimpleStats) && Evaluate(req.ModOps, item.SimpleMods))
                                return req.Result;
                        }
                    }
                }

                if (ClassRequirements.ContainsKey(item.Action.BaseItem.Class))
                {
                    foreach (PickItRequirement req in ClassRequirements[item.Action.BaseItem.Class])
                    {
                        if (item.Action.Quality == req.Quality)
                        {
                            if (Evaluate(req.StatOps, item.SimpleStats) && Evaluate(req.ModOps, item.SimpleMods))
                                return req.Result;
                        }
                    }
                }
            }
            else
            {
                // Not identified, just check if the type/class exists as a requirement
                // AND that it has something to identify, otherwise assume they want it kept unid

                if (TypeRequirements.ContainsKey(item.Action.BaseItem.BaseType.Type))
                {
                    foreach (PickItRequirement req in TypeRequirements[item.Action.BaseItem.BaseType.Type])
                    {
                        if ((req.StatOps != null && req.StatOps.Count > 0) || (req.ModOps != null && req.ModOps.Count > 0))
                            return PickItResult.Identify;
                    }
                }

                if (ClassRequirements.ContainsKey(item.Action.BaseItem.Class))
                {
                    foreach (PickItRequirement req in ClassRequirements[item.Action.BaseItem.Class])
                    {
                        if ((req.StatOps != null && req.StatOps.Count > 0) || (req.ModOps != null && req.ModOps.Count > 0))
                            return PickItResult.Identify;
                    }
                }

                // If we reached this point, it may mean that neither class/type had requirements
                // so let's make sure and just Keep it
                if (TypeRequirements.ContainsKey(item.Action.BaseItem.BaseType.Type) || ClassRequirements.ContainsKey(item.Action.BaseItem.Class))
                    return PickItResult.Keep;
            }

            return PickItResult.None;
        }

        private bool Evaluate(List<Op> req, Dictionary<string, int> stats)
        {
            // No requirements, good to go, joe
            if (req == null || req.Count == 0)
                return true;

            List<Op> tempOpList = new List<Op>();
            tempOpList.AddRange(req);

            Stack<int> stack = new Stack<int>();

            foreach (Op op in tempOpList)
            {
                if (op.Type == OpType.Operand)
                {
                    int value;

                    if (!Int32.TryParse(op.Value, out value))
                    {
                        if (stats.ContainsKey(op.Value))
                            value = stats[op.Value];
                    }

                    stack.Push(value);
                }
                else
                {
                    int op1 = stack.Pop();
                    int op2 = stack.Pop();

                    int result = op.Evaluate(op2, op1) ? 1 : 0;

                    stack.Push((int)result);
                }
            }

            return stack.Pop() == 1;
        }
    }
}
