using System;

namespace ChessClub
{
    class ChessPlayers
    {
        protected int _PlayerId;
        protected string _PlayerFirstName;
        protected string _PlayerLastName;
        protected int _PlayerAge;

        public ChessPlayers()
        {
            _PlayerId = 0;
            _PlayerFirstName = string.Empty;
            _PlayerLastName = string.Empty;
            _PlayerAge = 0;
        }

        public ChessPlayers(int id, string firstName, string lastName, int age)
        {
            _PlayerId = id;
            _PlayerFirstName = firstName;
            _PlayerLastName = lastName;
            _PlayerAge = age;
        }

        public int GetPlayerId() { return _PlayerId; }
        public string GetPlayerFirstName() { return _PlayerFirstName; }
        public string GetPlayerLastName() { return _PlayerLastName; }
        public int GetPlayerAge() { return _PlayerAge; }
        public void SetPlayerId(int id) { _PlayerId = id; }
        public void SetPlayerFirstName(string firstName) { _PlayerFirstName = firstName; }
        public void SetPlayerLastName(string lastName) { _PlayerLastName = lastName; }
        public void SetPlayerAge(int age) { _PlayerAge = age; }

        public virtual void AddChange()
        {
            Console.Write("Player ID=");
            SetPlayerId(int.Parse(Console.ReadLine()));
            Console.Write("First Name=");
            SetPlayerFirstName(Console.ReadLine());
            Console.Write("Last Name=");
            SetPlayerLastName(Console.ReadLine());
            Console.Write("Age=");
            SetPlayerAge(int.Parse(Console.ReadLine()));
        }

        public virtual void Print()
        {
            Console.WriteLine();
            Console.WriteLine($"    Player ID: {GetPlayerId()}");
            Console.WriteLine($"         Name: {GetPlayerFirstName()} {GetPlayerLastName()}");
            Console.WriteLine($"          Age: {GetPlayerAge()}");
        }
    }

    class ChessCoach : ChessPlayers
    {
        protected int _YearsOfExperience;

        public ChessCoach()
            : base()
        {
            _YearsOfExperience = 0;
        }

        public ChessCoach(int id, string firstName, string lastName, int age, int yearsOfExperience)
            : base(id, firstName, lastName, age)
        {
            _YearsOfExperience = yearsOfExperience;
        }

        public void SetYearsOfExperience(int yearsOfExperience) { _YearsOfExperience = yearsOfExperience; }
        public int GetYearsOfExperience() { return _YearsOfExperience; }

        public override void AddChange()
        {
            base.AddChange();
            Console.Write("Years of Experience=");
            SetYearsOfExperience(int.Parse(Console.ReadLine()));
        }

        public override void Print()
        {
            base.Print();
            Console.WriteLine($"Years of Experience: {GetYearsOfExperience()}");
            Console.WriteLine();
        }
    }

    class ChessProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many chess players do you want to enter?");
            int maxPlayers;
            while (!int.TryParse(Console.ReadLine(), out maxPlayers))
                Console.WriteLine("Please enter a whole number");

            ChessPlayers[] players = new ChessPlayers[maxPlayers];

            Console.WriteLine("How many coaches do you want to enter?");
            int maxCoaches;
            while (!int.TryParse(Console.ReadLine(), out maxCoaches))
                Console.WriteLine("Please enter a whole number");

            ChessCoach[] coaches = new ChessCoach[maxCoaches];

            int choice, rec, type;
            int playerCounter = 0, coachCounter = 0;
            choice = Menu();
            while (choice != 4)
            {
                Console.WriteLine("Enter 1 for Coach or 2 for Player");
                while (!int.TryParse(Console.ReadLine(), out type))
                    Console.WriteLine("1 for Coach or 2 for Player");

                try
                {
                    switch (choice)
                    {
                        case 1: // Add
                            if (type == 1) //Coach
                            {
                                if (coachCounter <= maxCoaches)
                                {
                                    coaches[coachCounter] = new ChessCoach();
                                    coaches[coachCounter].AddChange();
                                    coachCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of coaches has been added");
                            }
                            else //Player
                            {
                                if (playerCounter <= maxPlayers)
                                {
                                    players[playerCounter] = new ChessPlayers();
                                    players[playerCounter].AddChange();
                                    playerCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of players has been added");
                            }
                            break;
                        case 2: //Change
                            Console.Write("Enter the record number you want to change: ");
                            while (!int.TryParse(Console.ReadLine(), out rec))
                                Console.Write("Enter the record number you want to change: ");
                            rec--;

                            if (type == 1) //Coach
                            {
                                while (rec > coachCounter - 1 || rec < 0)
                                {
                                    Console.Write("The number you entered was out of range, try again");
                                    while (!int.TryParse(Console.ReadLine(), out rec))
                                        Console.Write("Enter the record number you want to change: ");
                                    rec--;
                                }
                                coaches[rec].AddChange();
                            }
                            else // Player
                            {
                                while (rec > playerCounter - 1 || rec < 0)
                                {
                                    Console.Write("The number you entered was out of range, try again");
                                    while (!int.TryParse(Console.ReadLine(), out rec))
                                        Console.Write("Enter the record number you want to change: ");
                                    rec--;
                                }
                                players[rec].AddChange();
                            }
                            break;
                        case 3: // Print All
                            if (type == 1) //Coach
                            {
                                for (int i = 0; i < coachCounter; i++)
                                    coaches[i].Print();
                            }
                            else // Player
                            {
                                for (int i = 0; i < playerCounter; i++)
                                    players[i].Print();
                            }
                            break;
                        default:
                            Console.WriteLine("You made an invalid selection, please try again");
                            break;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                choice = Menu();
            }
        }

        private static int Menu()
        {
            Console.WriteLine("Please make a selection from the menu");
            Console.WriteLine("1-Add  2-Change  3-Print  4-Quit");
            int selection = 0;
            while (selection < 1 || selection > 4)
                while (!int.TryParse(Console.ReadLine(), out selection))
                    Console.WriteLine("1-Add  2-Change  3-Print  4-Quit");
            return selection;
        }
    }
}