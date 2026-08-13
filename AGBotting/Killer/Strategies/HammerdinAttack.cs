using System;
using System.Collections.Generic;

using AGB;
using AGB.D2;

using D2Data;

namespace AGB.D2.Modules
{
    internal class HammerdinAttack : AttackStrategy
    {
        public HammerdinAttack(Game game)
            : base(game)
        {
            if (Game.Hero.Skills[SkillType.Concentration] == 0)
                throw new AttackStrategyException("The Hammerdin attack strategy requires the Concentration aura");

            if (Game.Hero.Skills[SkillType.Teleport] == 0)
                throw new AttackStrategyException("The Hammerdin attack strategy requires the Teleport skill");
        }

        #region The MEAT!
        public override AGB.Task Attack(NPC target, int priority, int counter)
        {
            if (target == null || !target.IsAlive)
                return null;
            // we died, fuck it!
            if (!Game.Hero.IsAlive)
                return null;

            Task nextMove = null;

            // Move around every 10 attacks, in case we're not hitting the monster
            if ((counter % 10) == 0)
            {
                nextMove = new Task(priority, "Teleporting next to " + target.Id,
                    delegate()
                    {
                        // Teleport around the monster
                        Random rand = new Random();

                        int x = 0;
                        int y = 0;

                        // try 20 times to find a random place around the monster
                        for (int i = 0; i < 1000; i++)
                        {
                            Map map = Game.MapManager.GetMap(Game.Hero.AreaLevel);

                            x = rand.Next(target.X - 1, target.X + 1);
                            y = rand.Next(target.Y - 1, target.Y + 1);

                            // Found a good shpot
                            if (map.IsWalkable(x - map.StitchedX, y - map.StitchedY))
                                break;

                            // we never found a good spot
                            if (i == 999)
                            {
                                x = target.X;
                                y = target.Y;
                                //throw new AttackStrategyException("Couldn't find a good spot to whack the monster from");
                            }
                        }

                        Game.Hero.TeleportWait(x, y, 5000);

                        Game.TaskManager.AddTask(Attack(target, priority, ++counter));
                    });
            }
            else
            {
                nextMove = new Task(priority, AttackString(target.Id, SkillType.BlessedHammer, target.LifeAsPercent),
                    delegate()
                    {
                        Game.Hero.SelectSkill(SkillHand.Right, SkillType.Concentration);

                        Game.Hero.Attack(target, SkillType.BlessedHammer);

                        System.Threading.Thread.Sleep(250);

                        Game.TaskManager.AddTask(Attack(target, priority, ++counter));
                    });
            }

            return nextMove;
        }
        #endregion
    }
}
