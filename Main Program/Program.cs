using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Main_Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("To Log In, Choose an option\nFor Admin Log In Press: A\nFor Employee Log In Press: E\nTo Exit Press: X\n ");
            string select = Convert.ToString(Console.ReadLine());
            Admin accessAdmin = new Admin();
            DataManipulation value = new DataManipulation();
            Employee accessEmployee = new Employee();
            if (select == "A" || select == "E")
            {
                Choice(select);
            }
            else if (select == "X" )
            {
                Choice(select);
            }
            else
            { 
                do
                {
                    Console.WriteLine("Wrong Choice.");
                    Console.WriteLine("To Log In, Choose an option\nFor Admin Log In Press: A\nFor Employee Log In Press: E\nTo Exit Press: X\n ");
                    select = Convert.ToString(Console.ReadLine());
                } while (select != "A" && select != "E" && select != "X");
             
                Choice(select);
            }

        }

        static void Choice(string choose)
        {
            DataManipulation value = new DataManipulation();
            if (choose == "A")
            {
                value.Readcsv();
                // accessAdmin.AdminLogIn();
            }
            else if (choose == "E")
            {
                value.Readcsv();
                //accessEmployee.EmployeeLogIn();
            }
            else if (choose == "X")
            {
                Console.WriteLine("You have exited. Press Enter to close.");
            }
        }
    }

    class Admin
    {

        public void AdminLogIn()
        {
            Console.WriteLine("To create new employee press: C\nTo edit employee press: R\nTo delete employee press: D\nTo Log Out press: X ");
            string select = Convert.ToString(Console.ReadLine());
            if (select == "C")
            {

                CreateUser();
                Console.WriteLine("New user created successfully.");
            }
            else if (select == "R")
            {
                Console.WriteLine("Employee Log In successful.");
            }
            else if (select == "D")
            {
                RemoveEmployee();
            }
            else if (select == "X")
            {
                Console.WriteLine("You have Loged Out successful. Press Enter to close the program");
            }
            else
            {
                Console.WriteLine("Wrong Choice.");
            }
        }

        static List<string> Details()
        {
            Console.WriteLine("Enter username: ");
            string name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter employee password: ");
            string pass = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter address: ");
            string address = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter status: ");
            string status = Convert.ToString(Console.ReadLine());
            List<string> details = new List<string>();
            details.Add(name);
            details.Add(pass);
            details.Add(address);
            details.Add(status);
            return details;
        }

        static Dictionary<string, List<string>> Collection(string id, List<string>details)
        {
            var collection = new Dictionary<string, List<string>>();
            collection.Add(id, details);
            return collection;
        }

        public void CreateUser()
        {
            string csvalter = "TestFile.csv";
            if (!File.Exists(csvalter) == true)
            {
                CreateCsvNew();
                DataManipulate(csvalter);
            }
            else 
            {
                DataManipulate(csvalter);
            }

        }

        static void CreateCsvNew()
        {
            string csvpath = "TestFile.csv";
            using (StreamWriter sr = File.AppendText(csvpath))
            {
                sr.Write("ID");
                sr.Write(",");
                sr.Write("Name");
                sr.Write(",");
                sr.Write("Password");
                sr.Write(",");
                sr.Write("Address");
                sr.Write(",");
                sr.Write("Status\n");
            }

        }
        static void RemoveEmployee()
        {
            DataManipulation delete = new DataManipulation();
            var collect = delete.ForUse();
            Console.WriteLine(string.Join(", ", collect.Keys));
            Console.WriteLine(string.Join(", ", collect["2"]));
            Console.WriteLine("Enter employee id: ");
            string empID = Convert.ToString(Console.ReadLine());
            var edit = delete.idList();
            if (collect.ContainsKey(empID))
            {
                collect.Remove(empID);
                Console.WriteLine(string.Join(", ", collect.Keys));
                Console.WriteLine("Employee details of Employee ID {0} successfully deleted.", empID);
            }
            else if (!edit.Contains(empID))
            {
                Console.WriteLine("Employee details of Employee ID {0} does not exist.", empID);
            }
        }
        static void DataManipulate(string csvpath)
        {
            Console.WriteLine("Enter employee id: ");
            string id = Convert.ToString(Console.ReadLine());
            var collection = new Dictionary<string, List<string>>();
            List<string> details = new List<string>();
            details = Details();
            collection = Collection(id, details);
            foreach (String key in collection.Keys)
            {
                using (StreamWriter sr = File.AppendText(csvpath))
                {
                    sr.Write(key);
                    sr.Write(",");
                    for (int i = 0; i < details.Count; i++)
                    {

                        sr.Write(details[i]);
                        sr.Write(",");

                    }
                    sr.Write("\n");
                }

            }
        }

        
    }

    class Employee
    {
        public void EmployeeLogIn()
        {
            Console.WriteLine("To profile press: R\nTo Log Out press: X ");
            string select = Convert.ToString(Console.ReadLine());
            
            if (select == "R")
            {
               
                Console.WriteLine("Employee Log In successful.");
            }
            
            else if (select == "X")
            {
                Console.WriteLine("You have Loged Out successful. Press Enter to close the program");
            }
            else
            {
                Console.WriteLine("Wrong Choice.");
            }
        }

    }

    class DataManipulation
    {
        public List<string> listOfId;
        public List<string> listOfPass;
        public Dictionary<string, List<string>> collectionDetails;
        
        public void Collection()
        {
            string csvpath = "TestFile.csv";
            var collection = new Dictionary<string, List<string>>();
            var details = new List<string>();
            var idList = new List<string>();
            var passList = new List<string>();
            string id;
            string pass;
            var subdetails = new List<string>();
            using (var reader = new StreamReader(csvpath))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    details = line.Split(',').ToList();
                    id = details[0];
                    pass =details[2];
                    subdetails = details.Skip(1).ToList();
                    collection.Add(details[0], subdetails);
                    idList.Add(id);
                    passList.Add(pass);
                }
            }
            listOfPass =passList;
            listOfId = idList;
            collectionDetails= collection;
        }

        public List<string> idList ()
        {
            Collection();
            var list= listOfId;
            return list;
        }

        public Dictionary<string, List<string>> ForUse()
        {
            string csvpath = "TestFile.csv";
            var collection = new Dictionary<string, List<string>>();
            var details = new List<string>();
            var idList = new List<string>();
            var passList = new List<string>();
            string id;
            string pass;
            var subdetails = new List<string>();
            using (var reader = new StreamReader(csvpath))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    details = line.Split(',').ToList();
                    id = details[0];
                    pass = details[2];
                    subdetails = details.Skip(1).ToList();
                    collection.Add(details[0], subdetails);
                    idList.Add(id);
                    passList.Add(pass);
                }
            }
            return collection;
        }
        
        public void Readcsv()
        {
            Collection();
            Console.WriteLine("Enter employee id: ");
            string empID = Convert.ToString(Console.ReadLine());
            var dic = new Dictionary<string, string>();
            var first = new List<string>();
            first = collectionDetails["ID"];
            var second = new List<string>();
            Admin enter = new Admin();
            Employee enterEmp = new Employee();
            if (listOfId.Contains(empID))

            {
                second = collectionDetails[empID];

                for (int i = 0; i < first.Count; i++)
                {
                    for (int j = 0; j < second.Count; j++)
                    {
                        if (i == j)
                        {
                            dic.Add(first[i], second[j]);

                        }
                    }
                }
                string status =dic["Status"];
                Console.WriteLine("Enter Password: ");
                string pass = Convert.ToString(Console.ReadLine());
                if(pass== dic["Password"] && status=="Admin")
                {
                    Console.WriteLine("Admin Log In successful.");
                    enter.AdminLogIn();
                }
                else if (pass != dic["Password"] && status == "Admin")
                {
                    do
                    {
                        Console.WriteLine("Wrong Password, Enter correct Password ");
                        pass= Convert.ToString(Console.ReadLine());
                    } while (pass != dic["Password"]);
                    Console.WriteLine("Admin Log In successful.");
                    enter.AdminLogIn();
                }
                else if (pass == dic["Password"] && status == "Employee")
                {
                    Console.WriteLine("Employee Log In successful.");
                    enterEmp.EmployeeLogIn();
                }
                else if (pass != dic["Password"] && status == "Employee")
                {
                    do
                    {
                        Console.WriteLine("Wrong Password, Enter correct Password ");
                        pass = Convert.ToString(Console.ReadLine());
                    } while (pass != dic["Password"]);
                    Console.WriteLine("Employee Log In successful.");
                    enterEmp.EmployeeLogIn(); 
                }
                
                else
                {
                    Console.WriteLine("Log In failed due to unauthentic Log In attempt.");
                }
                    
                
            }
            else if (!listOfId.Contains(empID))
            {
                Console.WriteLine("Employee ID {0} Does not exists in the Employee List.", empID);
            }
            
        }
    }
}
