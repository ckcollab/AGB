using System;
using System.Collections.Generic;

using AGB;
using AGB.D2;

using D2Data;

namespace AGB.D2.Modules
{
    /// <summary>
    /// Blizzard/Meteor sorcs, for example
    /// </summary>
    internal class RangedTimedTwoAttack : AttackStrategy
    {
        #region Fields
        /// <summary>
        /// For example, Glacial Spike
        /// </summary>
        public SkillType Untimed;

        /// <summary>
        /// For example, Blizzard
        /// </summary>
        public SkillType Timed;
        #endregion

        #region Constructor
        public RangedTimedTwoAttack(Game game, SkillType untimed, SkillType timed)
            : base(game)
        {
            Untimed = untimed;
            Timed = timed;
        }
        #endregion

        #region The MEAT!
        public override AGB.Task Attack(NPC target, int priority, int counter)
        {
            if (target == null || !target.IsAlive)
                return null;

            // we died, fuck it!
            if (!Game.Hero.IsAlive)
                return null;

            SkillType attackToUse = SkillType.None;

            bool isTimed = false;

            // For every 4 Untimed (glacials) let's throw one Timed (blizzard)
            if ((counter % 4) == 0)
            {
                attackToUse = Timed;
                isTimed = true;
            }
            else
                attackToUse = Untimed;

            // we faiiiiled
            if (counter > 20)
                return null;

            Task attack = new Task(priority, AttackString(target.Id, attackToUse, target.Life) + " (Is Timed = " + isTimed + ", AttackCounter = " + counter + ")",
                delegate()
                {
                    // Nothing complicated, no moving around or nothing, let's just get
                    // the job done, for now.
                    Game.Hero.Attack(target, attackToUse);

                    System.Threading.Thread.Sleep(500);

                    Game.TaskManager.AddTask(Attack(target, priority, ++counter));
                });

            return attack;
        }
        #endregion
    }
}
