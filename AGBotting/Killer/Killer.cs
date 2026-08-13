using System;
using System.Collections.Generic;
using System.Threading;

using AGB;
using AGB.D2;

using D2Data;

namespace AGB.D2.Modules
{
    /// <summary>
    /// This module takes care of moving to the target and killing the target
    /// </summary>
    public class Killer : Module
    {
        private TaskManager KillManager = new TaskManager();
        private AutoResetEvent StartedAttacking = new AutoResetEvent(false);

        private AttackStrategy mAttackStrategy;

        private AttackStrategy AttackStrategy
        {
            get
            {
                // If we haven't found an attack strategy yet, let's pick one
                // this is assuming we're in game and READY to attack the first monster (all
                // skills have been loaded n such)
                if (mAttackStrategy == null)
                {
                    // Default strategy is just punching
                    AttackStrategy strategy = new AttackStrategy(Game);

                    SkillType skill = Game.Hero.GetBestMaxedSkill();

                    if (skill == SkillType.None)
                        ThrowModuleException(new ModuleException(this, "Killer couldn't find a maxed skill that it supports."));

                    switch (skill)
                    {
                        #region Sorceresses
                        case SkillType.Blizzard:
                            strategy = new RangedTimedTwoAttack(Game, SkillType.GlacialSpike, SkillType.Blizzard);
                            break;
                        case SkillType.FrozenOrb:
                            strategy = new RangedTimedTwoAttack(Game, SkillType.GlacialSpike, SkillType.FrozenOrb);
                            break;
                        case SkillType.Lightning:
                        case SkillType.ChainLightning:
                            strategy = new RangedTimedTwoAttack(Game, SkillType.Lightning, SkillType.ChainLightning);
                            break;
                        case SkillType.Meteor:
                            strategy = new RangedTimedTwoAttack(Game, SkillType.FireBall, SkillType.Meteor);
                            break;
                        #endregion

                        #region Paladins
                        case SkillType.BlessedHammer:
                            strategy = new HammerdinAttack(Game);
                            break;
                        #endregion
                    }

                    mAttackStrategy = strategy;
                }

                return mAttackStrategy;
            }
        }

        public Killer()
        {
            Name = "Killer";
            Author = "ApacheChief";
            Version = "0.1.0";
        }

        /// <summary>
        /// Basically puts out a hit on the specified monster; killer will travel
        /// to the monster and kill it
        /// </summary>
        /// <param name="level"></param>
        /// <param name="monster"></param>
        public void Kill(AreaLevel level, NPCClass monster)
        {
            throw new NotImplementedException();
        }

        public void Kill(NPCClass id)
        {
            Game.TaskManager.AddTask((int)TaskPriority.Base, "Killing " + id.ToString(),
                delegate()
                {
                    DateTime start = DateTime.Now;

                    List<NPC> monsters = Game.NPCs.FindAll(id, 5000);

                    if (monsters == null)
                        ThrowModuleException(new ModuleException(this, "Couldn't find " + id + "..."));

                    // Put out the hit
                    Kill(monsters);  
                });
        }

        public void Kill(List<NPC> targets)
        {
            foreach (NPC target in targets)
                Kill(target);
        }

        public void Kill(NPC target)
        {
            try
            {
                // AttackStrategy.Attack should be recursive, and keep adding
                // attack tasks by itself if it needs more
                Game.TaskManager.AddTask(AttackStrategy.Attack(target, (int)TaskPriority.High, 0));
            }
            catch (AttackStrategyException e)
            {
                ThrowModuleException(new ModuleException(this, e.Message));
            }
        }

        /*
        /// <summary>
        /// Recursively attacks the target, by use tasks made by pulsing the attack strategy
        /// </summary>
        /// <param name="target"></param>
        /// <param name="attacks">Number of attacks tried</param>
        private void Fire(NPC target, int attacks)
        {
            if (attacks > 10)
                return;

            if (target.IsAlive)
            {
                Task task = AttackStrategy.Pulse(target);

                Game.TaskManager.AddTask(task);

                if (!task.IsFinished.WaitOne(20000, false))
                    ThrowModuleException(new ModuleException(this, "Killer::Fire timed out while attacking"));

                Fire(target, ++attacks);
            }
        }*/

        /*
        public void Kill(List<NPC> targets)
        {
            StartedAttacking.Reset();

            KillManager.AddTask(TaskPriority.Normal, "Killed targets",
                delegate()
                {
                    foreach (NPC target in targets)
                        // Make sure we're still in game, since we're not tied to the Game.TaskManager directly
                        if(Game.Seed != 0)
                            Kill(target);
                });

            // We need to wait for it to start attacking, after the first task
            // has been added
            if (!StartedAttacking.WaitOne(1000, false))
                ThrowModuleException(new ModuleException(this, "Never started attacking?"));
        }

        /// <summary>
        /// Killer assumes we're at the same level as the monster, so there's no
        /// need to travel.
        /// 
        /// Automatically selects an attack strategy based on the Hero's skills
        /// </summary>
        /// <param name="target"></param>
        public void Kill(NPC target)
        {
            Map map = Game.MapManager.GetMap(Game.Hero.AreaLevel);

            // Default strategy is just punching
            AttackStrategy strategy = new AttackStrategy(Game, map, target);

            SkillType skill = Game.Hero.GetBestMaxedSkill();

            if(skill == SkillType.None)
                ThrowModuleException(new ModuleException(this, "Killer couldn't find a maxed skill that it supports."));

            switch (skill)
            {
                case SkillType.Blizzard:
                    strategy = new RangedTimedTwoAttack(Game, map, target, SkillType.GlacialSpike, SkillType.Blizzard);
                    break;
                case SkillType.FrozenOrb:
                    strategy = new RangedTimedTwoAttack(Game, map, target, SkillType.GlacialSpike, SkillType.FrozenOrb);
                    break;
                case SkillType.Lightning:
                case SkillType.ChainLightning:
                    strategy = new RangedTimedTwoAttack(Game, map, target, SkillType.Lightning, SkillType.ChainLightning);
                    break;
                case SkillType.Meteor:
                    strategy = new RangedTimedTwoAttack(Game, map, target, SkillType.FireBall, SkillType.Meteor);
                    break;
            }

            DateTime start = DateTime.Now;

            // Time out in 20 seconds, immune?  gotta check that -.-
            while (target.IsAlive && DateTime.Now.Subtract(start).TotalMilliseconds < 5000)
            {
                Task task = strategy.Pulse();

                if (task == null)
                    break;

                Game.TaskManager.AddTask(task);

                // Set this after the first task has been added
                StartedAttacking.Set();

                // 20 seconds because we might have to go to town and heal or something inbetween attacks.
                if (!task.IsFinished.WaitOne(20000, false))
                    ThrowModuleException(new ModuleException(this, "Couldn't kill the monster.  Immune?" + Environment.NewLine + "Target.Id = " + target.Id + "; Strategy = " + strategy.ToString()));
            }

            // Set this again, just in case the monster was dead//didn't exist
            // before we started attacking it
            StartedAttacking.Set();
        }
         */

        public override void GameExited(Game game)
        {
            // Hard reset
            KillManager.Reset();
        }
    }
}
