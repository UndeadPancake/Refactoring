using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamning_2_ra_kod
{
    class Person
    {
        public string name, adress, phone, email;
        public Person(string N, string A, string P, string E)
        {
            name = N; adress = A; phone = P; email = E;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> Dict = LoadAdressList();


            Console.WriteLine("Hej och välkommen till adresslistan");
            Console.WriteLine("Skriv 'sluta' för att sluta!");
            string command;
            do
            {
                Console.Write("> ");
                command = Console.ReadLine();
                if (command == "sluta")
                {
                    Console.WriteLine("Hej då!");
                }
                else if (command == "ny")
                {
                    Console.WriteLine("Lägger till ny person");
                    Dict.Add(AddPerson());
                }
                else if (command == "ta bort")
                {
                    Console.Write("Vem vill du ta bort (ange namn): ");
                    string toBeRemoved = Console.ReadLine();
                    int found = IndexFinder(Dict, toBeRemoved);
                    if (found == -1)
                    {
                        Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", toBeRemoved);
                    }
                    Dict.RemoveAt(found);
                }
                else if (command == "visa")
                {
                    for (int i = 0; i < Dict.Count(); i++)
                    {
                        Person P = Dict[i];
                        Console.WriteLine("{0}, {1}, {2}, {3}", P.name, P.adress, P.phone, P.email);
                    }
                }
                else if (command == "ändra")
                {
                    Console.Write("Vem vill du ändra (ange namn): ");
                    string personChange = Console.ReadLine();
                    int found = IndexFinder(Dict, personChange);
                    if (found == -1)
                    {
                        Console.WriteLine("Tyvärr: {0} fanns inte i telefonlistan", personChange);
                    }
                    else
                    {
                        Dict[found] = PersonChanger(Dict[found], personChange);
                    }
                }
                else
                {
                    Console.WriteLine("Okänt kommando: {0}", command);
                }
            } while (command != "sluta");
        }
        static List<Person> LoadAdressList()
        {
            List<Person> Dict = new List<Person>();

            Console.Write("Laddar adresslistan ... ");
            using (StreamReader fileStream = new StreamReader(@"..\..\address.lis"))
            {
                while (fileStream.Peek() >= 0)
                {
                    string line = fileStream.ReadLine();
                    string[] word = line.Split('#');
                    Person P = new Person(word[0], word[1], word[2], word[3]);
                    Dict.Add(P);
                }
            }
            Console.WriteLine("klart!");
            return Dict;
        }
        static Person AddPerson()
        {
            Console.Write("  1. ange namn:    ");
            string name = Console.ReadLine();
            Console.Write("  2. ange adress:  ");
            string adress = Console.ReadLine();
            Console.Write("  3. ange telefon: ");
            string phone = Console.ReadLine();
            Console.Write("  4. ange email:   ");
            string email = Console.ReadLine();
            Person returnValue = new Person(name, adress, phone, email);
            return returnValue;
        }
        static int IndexFinder(List<Person> Dict, string toBeFound)
        {
            int found = -1;
            for (int i = 0; i < Dict.Count(); i++)
            {
                if (Dict[i].name == toBeFound) found = i;
            }
            return found;
        }
        static Person PersonChanger(Person person, string personChange)
        {
            Console.Write("Vad vill du ändra (namn, adress, telefon eller email): ");
            string elementToChange = Console.ReadLine();
            Console.Write("Vad vill du ändra {0} på {1} till: ", elementToChange, personChange);
            string newValue = Console.ReadLine();
            switch (elementToChange)
            {
                case "namn": person.name = newValue; break;
                case "adress": person.adress = newValue; break;
                case "telefon": person.phone = newValue; break;
                case "email": person.email = newValue; break;
                default: break;
            }
            return person;
        }
    }
}
