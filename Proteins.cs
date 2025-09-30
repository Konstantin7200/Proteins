
namespace Proteins
{
    interface IHandleInput
    {
        string[] handleInput(string pathname);
    }
    interface IHandleLogic
    {
        List<Protein> search(string substr,List<Protein> AllProteins);
        int diff(Protein protein1, Protein protein2);
        char mode(string proteinName, List<Protein> allProteins);
    }
    interface IHandleOutput
    {

    }
    class LogicHandler
    {
        public List<Protein> search(string substr, List<Protein> AllProteins)
        {
            List<Protein> proteins = new List<Protein>();
            for (int i = 0; i < AllProteins.Count; i++)
            {
                if (AllProteins[i].getAminoacid().Contains(substr))
                    proteins.Add(AllProteins[i]);
            }
            return proteins;
        }
        public int diff(Protein protein1, Protein protein2)
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

        public char mode(string proteinName,List<Protein> allProteins)
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
                aminoacid.ToDictionary
            }

            return ' ';
        }
    }
    class InputHandler : IHandleInput
    {
        public string[] handleInput(string pathname)
        {
            string allText = File.ReadAllText("pathname");
            return allText.Split();
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
        Protein(string args)
        {
            string[] splitted = args.Split("\t");
            name = splitted[0];
            organism = splitted[1];
            aminoacid=decodeAminoacid(splitted[2]);
        }
        string decodeAminoacid(string aminoacid)
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
            
        }
    }


    
}
