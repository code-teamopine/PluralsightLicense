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
    public interface IDevTeamService
    {
        bool RemoveDevelopersFromTeam(DevTeam teamDeveloper);
        bool AssignDeveloperstoTeam(DevTeam teamDeveloper);
        List<TeamDevelopersVM> GetTeamDevelopers();
    }

    public class DevTeamService : IDevTeamService
    {
        readonly IDevTeamRepo devTeamRepo;
        public DevTeamService()
        {
            devTeamRepo = new DevTeamRepo();
        }

        
        /// <summary>
        /// Returns all if there is no team id given
        /// </summary>
        /// <returns>bool</returns>
        public List<TeamDevelopersVM> GetTeamDevelopers()
        {
            return devTeamRepo.GetTeamDevelopers();
        }

        
        public bool AssignDeveloperstoTeam(DevTeam teamDeveloper)
        {
            return devTeamRepo.AssignDeveloperstoTeam(teamDeveloper);
        }

        public bool RemoveDevelopersFromTeam(DevTeam teamDeveloper)
        {
            return devTeamRepo.RemoveDevelopersFromTeam(teamDeveloper);
        }
    }
}