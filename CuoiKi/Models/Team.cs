﻿using System;

namespace CuoiKi.Models
{
    public class Team : ModelBase
    {
        public override string ID { get; }
        public string StageID { get; set; }
        public string TechLeadID { get; set; }
        public string Name { get; set; }

        public Team(string ID, string StageID, string TechLeadID, string Name)
        {
            this.ID = ID;
            this.StageID = StageID;
            this.TechLeadID = TechLeadID;
            this.Name = Name;
        }

        public static Team CreateNewTeam(string StageID, string TechLeadID, string Name)
        {
            string ID = "Team-" + new Random().NextInt64().ToString();
            return new(ID, StageID, TechLeadID, Name);
        }
    }
}
