
using System.Xml.Linq;

namespace Proteins
{
    interface IHandleInput
    {
        string[] handleInput(string pathname);
    }
    interface IHandleLogic
    {
        List<Protein> search(string substr,List<Protein> allProteins);
        int diff(Protein protein1, Protein protein2);
        char mode(string proteinName, List<Protein> allProteins);
    }
    interface IHandleOutput
    {
        
    }
    class OutputHandler
    {
        void output(string command, int commandNum, string result)
        {
            string pathname = "genedata.txt";
            string splitter = "-------------------------------------\n";
            File.WriteAllText(pathname, splitter);
            File.WriteAllText(pathname, "00" + commandNum + "\t" + command + "\n");
            File.WriteAllText(pathname, result);
            File.WriteAllText(pathname, splitter);
        }
    }
    class LogicHandler
    {
        static public List<Protein> search(string substr, List<Protein> allProteins)
        {
            List<Protein> proteins = new List<Protein>();
            for (int i = 0; i < allProteins.Count; i++)
            {
                if (allProteins[i].getAminoacid().Contains(substr))
                    proteins.Add(allProteins[i]);
            }
            return proteins;
        }
        static public int diff(Protein protein1, Protein protein2)
        {
            string aminoacid1 = protein1.getAminoacid();
            string aminoacid2 = protein2.getAminoacid();
            int count=Math.Abs(aminoacid1.Length-aminoacid2.Length);
            int length = aminoacid1.Length < aminoacid2.Length ? aminoacid1.Length :aminoacid2.Length;
            for (int i = 0; i < length; i++)
                if (aminoacid1[i] != aminoacid2[i])
                    count++;
            return count;
        }

        static public char mode(string proteinName,List<Protein> allProteins)
        {
            string aminoacid="";
            Dictionary<char, int> dict = new Dictionary<char, int>();
            foreach(Protein protein in allProteins)
            {
                if(protein.getName()==proteinName)
                {
                    aminoacid = protein.getAminoacid();
                    break;
                }
            }
            for(int i=0;i<aminoacid.Length;i++)
            {
                if (!dict.Keys.Contains(aminoacid[i]))
                    dict.Add(aminoacid[i], 0);
                else dict[aminoacid[i]]++;
            }
            int max = dict.Values.Max();
            char result = 'Z';
            foreach(char symbol in dict.Keys)
            {
                if (dict[symbol] == max && symbol < result)
                    result = symbol;
            }

            return result;
        }
    }

    class ResultFormer()
    {
        public static string formFromSearch(List<Protein> allProteins)
        {
            string result="organism"+"                 "+"protein\n";

            if (allProteins.Count == 0)
                result+= "NOT FOUND/n";
            for(int i=0;i<allProteins.Count;i++)
            {
                result += allProteins[i].getOrganism + "                 " + allProteins[i].getAminoacid() + "\n";
            }
            return result;
        }
        public static string formFromDiff(int difference,string proteinName1,string proteinName2)
        {
            string result="";
            if (difference == -1)
            {
                result += "Missing:" + proteinName1;
            }
            else if (difference == -2)
            {
                result += "Missing:" + proteinName2;
            }
            else if (difference == -1)
            {
                result += "Missing:" + proteinName1 + "and" + proteinName2;
            }
            else
            {
                result += "amino-acids difference:\n" + difference;
            }
            return result;
        }
        public static string formFromMode()
        {
            string result="";



            return result;
        }

    }

    class CommandCenter
    {
        public void analyzeCommand(string commandString,List<Protein> allProteins)
        {
            string[] command = commandString.Split('\t');
            if (command[0]=="search")
            {
                LogicHandler.search(Protein.decodeAminoacid(command[1]), allProteins);
            }
            else if (command[0]=="diff")
            {
                string name1 = command[1];
                string name2 = command[2];
                Protein protein1=null, protein2=null;
                foreach (Protein protein in allProteins)
                {
                    if(protein.getName()==name1)
                    {
                        protein1 = protein;
                    }
                    else if(protein.getName()==name2)
                    {
                        protein2 = protein;
                    }
                }
                if(protein1!=null&&protein2!=null)
                LogicHandler.diff(protein1, protein2);
            }

            else if (command[0] == "mode")
            {
                LogicHandler.mode(command[1], allProteins);
            }
        }
    }
    class InputHandler : IHandleInput
    {
        public string[] handleInput(string pathname)
        {
            string allText = File.ReadAllText(pathname);
            return allText.Split('\n');
        }
    }
    class Protein
    {
        string name;
        private string aminoacid;
        string organism;
        public string getAminoacid()
        {
            return aminoacid;
        }
        public string getName()
        {
            return name;
        }
        public string getOrganism()
        {
            return organism;
        }
        public Protein(string args)
        {
            string[] splitted = args.Split("\t");
            
                name = splitted[0];
                organism = splitted[1];
                aminoacid = decodeAminoacid(splitted[2]);
            
        }
        static public string decodeAminoacid(string aminoacid)
        {
            string result="";
            for(int i=0;i<aminoacid.Length;i++)
            {
                if (aminoacid[i] - 48 < 10)
                {
                    i++;
                    result += aminoacid[i] * aminoacid[i - 1];
                }
                else result += aminoacid[i];
            }
            return result;
        }
    }

    class Program
    {
        public static void Main()
        {
            InputHandler inputHandler = new InputHandler();
            string[] proteins = inputHandler.handleInput("sequences.0.txt");
            string[] commands=inputHandler.handleInput("commands.0.txt");

            List<Protein> allProteins = new List<Protein>();
            for (int i = 0; i < proteins.Length-1; i++)
            {
                allProteins.Add(new Protein(proteins[i]));
            }
            CommandCenter commandCenter = new CommandCenter();
            for(int i=0;i<commands.Length;i++)
                commandCenter.analyzeCommand(commands[i], allProteins);

        }
    }

}
