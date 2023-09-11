using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using PluralsightLicense.Data;
using PluralsightLicense.Service;

namespace PluralsightLicense.Consoles
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            DeveloperService ds = new DeveloperService();
            TeamService ts = new TeamService();
            DevTeamService dts = new DevTeamService();

            while (true)
            {
            tostart:
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("1.Add Team");
                Console.WriteLine("2.Remove Team");
                Console.WriteLine("3.List Teams");
                Console.WriteLine("4.Add Developer");
                Console.WriteLine("5.Remove Developer");
                Console.WriteLine("6.List Developers");
                Console.WriteLine("7.Assign Developer's to Team");
                Console.WriteLine("8.List Team Developers");
                Console.WriteLine("9.Remove Team Developers");
                Console.WriteLine("10.List Unlicensed Developers");
                Console.Write("What option do you want to select? ");
                string input = Console.ReadLine();
                if (input.Equals("1"))
                {
                    Console.WriteLine();
                    Console.Write("Enter name of team: ");
                    string teamName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(teamName))
                    {
                        Team team = new Team() { TeamName = teamName };
                        ts.CreateTeam(team);
                    }
                    goto tostart;
                }
                else if (input.Equals("2"))
                {
                deleteTeamPluralLicenses:
                    Console.Write("Enter team id to delete the team: ");
                    string deleteId = Console.ReadLine();
                    int deleteid;
                    if (!string.IsNullOrEmpty(deleteId) && Int32.TryParse(deleteId, out deleteid))
                    {
                        ts.DeleteTeam(Convert.ToInt32(deleteId));
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid input.");
                        goto deleteTeamPluralLicenses;
                    }
                    goto tostart;
                }
                else if (input.Equals("3"))
                {
                    var teams = ts.GetTeams();
                    if (teams.Count > 0)
                    {
                        Console.WriteLine("Team list");
                        Console.WriteLine("Id\tName");
                        foreach (var team in teams)
                        {
                            Console.WriteLine(team.TeamId + "\t" + team.TeamName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Developers available in system.");
                    }
                    goto tostart;
                }
                else if (input.Equals("4"))
                {
                    Console.WriteLine();
                    Console.Write("Enter name of developer: ");
                    string developerName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(developerName))
                    {
                        Developer developer = new Developer() { DeveloperName = developerName };
                    PluralLicenses:
                        Console.Write("Assign pluralsight license (Yes or No)? ");
                        string isPlural = Console.ReadLine();
                        if (!string.IsNullOrEmpty(isPlural) && (isPlural.Trim().ToLower().Equals("y") || isPlural.Trim().ToLower().Equals("n") || isPlural.Trim().ToLower().Equals("yes") || isPlural.Trim().ToLower().Equals("no")))
                        {
                            if (isPlural.Trim().ToLower().Equals("y") || isPlural.Trim().ToLower().Equals("yes"))
                            {
                                developer.IsPluralsightLicenseAssigned = true;
                            }
                            else
                            {
                                developer.IsPluralsightLicenseAssigned = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter valid input.");
                            goto PluralLicenses;
                        }
                        ds.CreateDeveloper(developer);
                    }
                    goto tostart;
                }
                else if (input.Equals("5"))
                {
                deletePluralLicenses:
                    Console.Write("Enter developer id: ");
                    string deleteId = Console.ReadLine();
                    int deleteid;
                    if (!string.IsNullOrEmpty(deleteId) && Int32.TryParse(deleteId, out deleteid))
                    {
                        ds.DeleteDeveloper(deleteid);
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid input.");
                        goto deletePluralLicenses;
                    }
                    goto tostart;
                }
                else if (input.Equals("6"))
                {
                    var developers = ds.GetDevelopers(0);
                    if (developers.Count > 0)
                    {
                        Console.WriteLine("Developer list");
                        Console.WriteLine("Id\tName\t\tHas License");
                        foreach (var developer in developers)
                        {
                            Console.WriteLine(developer.DeveloperId + "\t" + developer.DeveloperName + "\t\t\t" + (developer.IsPluralsightLicenseAssigned ? "Yes" : "No"));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Developers not available in system.");
                    }
                    goto tostart;
                }
                else if (input.Equals("7"))
                {
                addTeam:
                    Console.WriteLine();
                    Console.Write("Enter team id: ");
                    string teamId = Console.ReadLine();
                    int teamID;
                    if (!string.IsNullOrEmpty(teamId) && Int32.TryParse(teamId, out teamID) && ts.IsAvailable(teamID))
                    {
                        DevTeam teamDeveloperteam = new DevTeam() { TeamId = teamID };
                    addDevlopers:
                        Console.Write("Enter id's of developers to assign in the team id " + teamID + " (Separated by comma): ");
                        string developerids = Console.ReadLine();
                        if (!string.IsNullOrEmpty(developerids))
                        {
                            foreach (var item in developerids.Split(","))
                            {
                                int id;
                                if (!(int.TryParse(item, out id) && ds.IsAvailable(id)))
                                {
                                    Console.WriteLine("Please enter valid input.");
                                    goto addDevlopers;
                                }
                            }
                            teamDeveloperteam.DeveloperIds = developerids;
                            dts.AssignDeveloperstoTeam(teamDeveloperteam);
                        }
                        else
                        {
                            Console.WriteLine("Please enter correct input.");
                            goto addDevlopers;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter correct input.");
                        goto addTeam;
                    }
                    goto tostart;
                }
                else if (input.Equals("8"))
                {
                    var teamdevelopers = dts.GetTeamDevelopers();
                    if (teamdevelopers.Count > 0)
                    {
                        Console.WriteLine("Team list");
                        Console.WriteLine("Id\tTeam Name\t\tDeveloperName");
                        foreach (var teamdeveloper in teamdevelopers)
                        {
                            Console.WriteLine(teamdeveloper.Id + "\t" + teamdeveloper.TeamName + "\t\t" + teamdeveloper.DeveloperName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No developer assigned to team.");
                    }
                    goto tostart;
                }
                else if (input.Equals("9"))
                {
                addTeam:
                    Console.WriteLine();
                    Console.Write("Enter team id: ");
                    string teamId = Console.ReadLine();
                    int teamID;
                    if (!string.IsNullOrEmpty(teamId) && Int32.TryParse(teamId, out teamID) && ts.IsAvailable(teamID))
                    {
                        DevTeam teamDeveloperteam = new DevTeam() { TeamId = teamID };
                    deleteDevelopers:
                        Console.Write("Enter id's of developers to assign in the team id " + teamID + " (Separated by comma): ");
                        string developerids = Console.ReadLine();
                        if (!string.IsNullOrEmpty(developerids))
                        {
                            foreach (var item in developerids.Split(","))
                            {
                                int id;
                                if (!(int.TryParse(item, out id) && ds.IsAvailable(id)))
                                {
                                    Console.WriteLine("Please enter valid input.");
                                    goto deleteDevelopers;
                                }
                            }
                            teamDeveloperteam.DeveloperIds = developerids;
                            dts.RemoveDevelopersFromTeam(teamDeveloperteam);
                        }
                        else
                        {
                            Console.WriteLine("Please enter correct input.");
                            goto deleteDevelopers;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter correct input.");
                        goto addTeam;
                    }
                    goto tostart;
                }
                else if (input.Equals("10"))
                {
                    var developers = ds.UnlicensedDevelopers();
                    if (developers.Count > 0)
                    {
                        Console.WriteLine("Developer list");
                        Console.WriteLine("Id\tName\t\tHas License");
                        foreach (var developer in developers)
                        {
                            Console.WriteLine(developer.DeveloperId + "\t" + developer.DeveloperName + "\t\t\t" + (developer.IsPluralsightLicenseAssigned ? "Yes" : "No"));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Developers not available in system.");
                    }
                    goto tostart;
                }
                else
                {
                    Console.WriteLine("Please try again");
                }
                Console.Write("Press any key to start");
                Console.ReadLine();
            }
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger log = new Logger();
            log.Log(e.ExceptionObject.ToString(), 0, 0);
            Console.WriteLine("Unknown error occurred.");
            Environment.Exit(0);
        }
    }
}