using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using PluralsightLicense.Data;
using PluralsightLicense.Repo;

namespace PluralsightLicense.Service
{
    public interface ITeamService
    {
        bool CreateTeam(Team Team);
        List<Team> GetTeams();
        bool DeleteTeam(int teamId);
        bool IsAvailable(int teamId);
    }

    public class TeamService : ITeamService
    {
        readonly ITeamRepo teamRepo;
        public TeamService()
        {
            teamRepo = new TeamRepo();
        }

        /// <summary>
        /// Returns false if it can't create the item
        /// </summary>
        /// <returns>bool</returns>
        public bool CreateTeam(Team team)
        {
            return teamRepo.Insert(team);
        }

        /// <summary>
        /// Returns all if there is no team id given
        /// </summary>
        /// <returns>bool</returns>
        public List<Team> GetTeams()
        {
            return teamRepo.GetAll();
        }



        /// <summary>
        /// Returns false if it can't delete the item
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteTeam(int teamId)
        {
            return teamRepo.Delete(teamId);
        }

        /// <summary>
        /// Returns false if it can't delete the item
        /// </summary>
        /// <returns>bool</returns>
        public bool IsAvailable(int teamId)
        {
            return teamRepo.IsAvailable(teamId);
        }


        
        
    }
}