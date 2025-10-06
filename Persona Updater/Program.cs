// System
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

// EllieMae
using EllieMae.Encompass.Runtime;
using EllieMae.Encompass.Client;
using EllieMae.Encompass.BusinessObjects.Users;
// using EllieMae.Encompass.AsmResolver;    -> this is not being used anywhere in this program
// using EllieMae.Encompass.BusinessObjects;    -> this is not being used anywhere in this program

namespace EmLiteStuff
{
    class Program
    {
        public static void Main()
        {
            // new EllieMae.Encompass.Runtime.RuntimeServices().Initialize();
            new RuntimeServices().Initialize();
            startApplication();
        }

        private static void startApplication()
        {
            // Start a new Session
            // Session s = new EllieMae.Encompass.Client.Session();
            Session s = new Session();
            s.Start("https://TEBE11361020.ea.elliemae.net$TEBE11361020", "zfons", "Brightowl1!"); // Ensure installed SDK version matches server version

            Console.WriteLine("You are now connected to server " + s.ServerURI);
            Console.WriteLine("Beginning persona update...");

            // Start timer
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var reader = new StreamReader(File.OpenRead(@"C:\z\dev\console apps\Persona Updater\io\input.txt"));
            // EllieMae.Encompass.BusinessObjects.Users.Persona p = s.Users.Personas.GetPersonaByName("Underwriter");
            Persona p = s.Users.Personas.GetPersonaByName("Post Closer");

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] users = line.Split(',');

                try
                {
                    User user = s.Users.GetUser(users[0]);

                    if (!user.Equals(null))
                    {
                        user.Personas.Add(p);
                        // user.Personas.Remove(p);
                        user.Commit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + users[0] + " >> " + ex);
                    Console.ReadLine();
                }
            }

            // End session to gracefully disconnect from server
            s.End();

            // Stop timer
            timer.Stop();

            Console.WriteLine("Update complete.");
            Console.WriteLine("Time to complete: " + timer.Elapsed.ToString());
        }
    }
}



/* 
 
* The version of code below uses an output text file to effeciently log errors

var FileDate = DateTime.Today.ToString("yyyy_MM_dd");
var reader = new StreamReader(File.OpenRead(@"C:\z\dev\console apps\Persona Updater\io\input.txt"));

using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\z\dev\console apps\Persona Updater\io\output.txt"))
{
    // Get a specific persona
    EllieMae.Encompass.BusinessObjects.Users.Persona p = s.Users.Personas.GetPersonaByName("Underwriter");

    // Run code while there are lines in the given csv file
    while (!reader.EndOfStream)
    {
        // Establish the line being read from the given csv file as a string variable
        // Establish the array of users and split the values on any commas
        string line = reader.ReadLine();
        string[] users = line.Split(',');

        try
        {
            User user = s.Users.GetUser(users[0]); // Establish the current user from the list of users

            if (!user.Equals(null)) 
            {
                user.Personas.Add(p); // Add the persona to the user's list of personas
                // user.Personas.Remove(p); // Remove the persona from the user's list of personas
                user.Commit();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(users[0] + " " + ex + " " + FileDate);
            file.WriteLine(users[0] + " " + ex + " " + FileDate);
        }

    }

}

*/