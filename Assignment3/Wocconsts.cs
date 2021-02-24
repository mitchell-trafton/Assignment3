using System;
using System.Collections.Generic;
/************************************************************
 * Assignment 3
 * Programmers: Robert Tyler Trotter z1802019
 *              Mitchell Trafton     z1831076
 ***********************************************************/
namespace Assignment3
{
    public enum itemType
    {
        Helmet, Neck, Shoulders, Back, Chest, Wrist,
        Gloves, Belt, Pants, Boots, Ring, Trinket, None
    };
    public enum Race { Orc, Troll, Tauren, Forsaken };

    public enum Class
    {
        Warrior, Mage, Druid, Priest,
        Warlock, Rogue, Paladin, Hunter, Shaman
    };

    public enum Role { Tank, Healer, Damage  };

    public enum GuildType { Casual, Questing, MythicPls, Raiding, PVP };

    static class Constants
    {
        public static uint MAX_ILVL = 360;
        public static uint MAX_PRIMARY = 200;
        public static uint MAX_STAMINA = 275;
        public static uint MAX_LEVEL = 60;
        public static uint GEAR_SLOTS = 14;
        public static uint MAX_INVENTORY_SIZE = 20;

        public static Dictionary<Class, List<Role>> allowedRolls = new Dictionary<Class, List<Role>>() //dictionary contining lists of allowed roles for each race
        {
            { Class.Warrior, new List<Role>{ Role.Tank, Role.Damage } },
            { Class.Mage, new List<Role>{ Role.Damage } },
            { Class.Druid, new List<Role>{ Role.Tank, Role.Healer, Role.Damage } },
            { Class.Priest, new List<Role>{ Role.Healer, Role.Damage } },
            { Class.Warlock, new List<Role>{ Role.Damage } },
            { Class.Rogue, new List<Role>{ Role.Damage } },
            { Class.Paladin, new List<Role>{ Role.Tank, Role.Healer, Role.Damage } },
            { Class.Hunter, new List<Role>{ Role.Damage } },
            { Class.Shaman, new List<Role>{ Role.Healer, Role.Damage } }
        };
    }

}