using System;
using System.Collections.Generic;

using AGB;
using AGB.D2;

namespace AGB.D2.Modules
{
    public class AttackStrategyException : Exception
    {
        public AttackStrategyException(string message)
            : base(message)
        {

        }
    }

    internal class AttackStrategy
    {
        protected Game Game;

        public AttackStrategy(Game game)
        {
            Game = game;
        }

        /// <summary>
        /// Should be the next-best-Task for this specific strategy, and only ONE Task!
        /// 
        /// This will get called again as soon as the last Task has completed.
        /// </summary>
        /// <example>If you wanted to move to a better location to attack from, you create 
        /// the Task to move, then the next Pulse should consider your new location to be 
        /// satisfactory and then add a Task to attack</example>
        /// <returns></returns>
        /// ABOVE IS OLD!
        public virtual Task Attack(NPC target, int priority, int counter)
        {
            if (target == null || !target.IsAlive)
                return null;
            // we died, fuck it!
            if (!Game.Hero.IsAlive)
                return null;

            Task attack = new Task(priority, AttackString(target.Id, D2Data.SkillType.Attack, target.Life),
                delegate()
                {
                    // Let's just punch 'em
                    Game.Hero.Attack(target, D2Data.SkillType.Attack);
                    System.Threading.Thread.Sleep(1000);

                    Game.TaskManager.AddTask(Attack(target, priority, ++counter));
                });

            return attack;
        }

        /// <summary>
        /// Helper function to make the "Attacked DefiledWarrior with Blizzard" kind of string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string AttackString(D2Data.NPCClass npcClass, D2Data.SkillType type, int percentLife)
        {
            return "Attacked " + npcClass + " (" + percentLife + "%) with " + type;
        }
    }
}
