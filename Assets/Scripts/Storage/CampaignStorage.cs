using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;
using AssemblyCSharp;
using Mono.Data.SqliteClient;
using System.Linq;
using System;

namespace Assets.Scripts.Storage
{
    class CampaignStorage
    {

        string insertCampaignSQL = "insert into Campaign (Name) VALUES ('{0}');";
        string updateCampaignSQL = "update Campaign set Name = '{0}' where ID = {1};";

        string insertCampaignRoomsSQL = "insert into CampaignRoomLink VALUES ({0}, {1}, {2});";
        string clearCampaignRoomsSQL = "delete * from CampaignRoomLink where {0} = {1};";

        string insertCampaignCharactersSQL = "insert into CampaignCharacter VALUES ({0},{1});";
        string clearCampaignCharsSQL = "delete * from CampaignCharacter where {0} = {1};";

        string selectCampaignSQL = "select * from Campaign;";
        string selectWhereCampaignSQL = "select * from Campaign where {0} = {1};";

        public void SaveCampaign(Campaign campaign)
        {
            int ID = GetIDByName(campaign.Name);
            Debug.Log(ID);
            if (ID == -1)               //campaign is new
            {
                var finalSQL = String.Format(insertCampaignSQL, campaign.Name);
                Debug.Log(finalSQL);
                Database.runModifyQuery(finalSQL);
            }
        }

        public string GetNameByID(int id)
        {
            var sql = "select Name from Campaign where ID = " + id + ";";
            var reader = Database.runSelectQuery(sql);
            List<string> results = new List<string>();
            while (reader.Read())
            {
                results.Add(reader.GetValue(0).ToString());
            }
            return results.Count > 0 ? results[0] : "";

        }

        public int GetIDByName(string name)
        {

            var sql = "select ID from Campaign where Name = " + name +";";
            var reader = Database.runSelectQuery(sql);
            List<int> results = new List<int>();
            while (reader.Read())
            {
                results.Add(Int32.Parse(reader.GetValue(0).ToString()));
            }
            return results.Count > 0 ? results[0] : -1;
        }

        public Campaign GetCampaignByName(string campaignName)
        {
            return null;
        }
    }
}
