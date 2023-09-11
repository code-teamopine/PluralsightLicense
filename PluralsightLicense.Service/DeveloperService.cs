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
    public interface IDeveloperService
    {
        bool CreateDeveloper(Developer developer);
        List<Developer> GetDevelopers(int teamId);
        bool DeleteDeveloper(int developerId);
        List<Developer> UnlicensedDevelopers();
    }
    public class DeveloperService : IDeveloperService
    {
        readonly IDeveloperRepo developerRepo;
        public DeveloperService()
        {
            developerRepo = new DeveloperRepo();
        }

        /// <summary>
        /// Returns false if it can't create the item
        /// </summary>
        /// <returns>bool</returns>
        public bool CreateDeveloper(Developer developer)
        {
            return developerRepo.Insert(developer);
        }

        /// <summary>
        /// Returns all if there is no team id given
        /// </summary>
        /// <returns>bool</returns>
        public List<Developer> GetDevelopers(int teamId)
        {
            return developerRepo.GetAll(teamId);
        }

        /// <summary>
        /// Returns false if it can't delete the item
        /// </summary>
        /// <returns>bool</returns>
        public bool DeleteDeveloper(int developerId)
        {
            return developerRepo.Delete(developerId);
        }

        /// <summary>
        /// Returns false if it can't delete the item
        /// </summary>
        /// <returns>bool</returns>
        public bool IsAvailable(int developerId)
        {
            return developerRepo.IsAvailable(developerId);
        }

        public List<Developer> UnlicensedDevelopers()
        {
            return developerRepo.GetAllUnlicensed();
        }
    }
}