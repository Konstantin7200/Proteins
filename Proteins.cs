
namespace Hueta
{
    class Program {
        static void Main(string[] args)
        {
            Console.WriteLine("Андрей!!!");
            Protein protein = new Protein("h", "a", "BBBBBCCCCC");
            Protein protein1 = new Protein("ajs", "aks", "ABBBA");
            Console.WriteLine(protein.diff(protein1));
        }
 }

    class InputHandler
    {
        string fileName;

        Protein GetProtein()
        {
            File.ReadLines(fileName);
            return null;
        }
    }
    class OutputHandler
    {

    }

    class DataHandler
    {

    }

    class Protein
    {
        string name;
        string species;
        string aminoacid;

        public Protein(string name, string species, string aminoacid)
        {
            this.name = name;
            this.species = species;
            this.aminoacid = aminoacid;
        }

        public char mode()
        {
            List<char> keys=new List<char>();
            Dictionary<char, int> dict=new Dictionary<char, int>();
            for (int i = 0; i < aminoacid.Length; i++)
            {
                if (dict.Keys.Contains(aminoacid[i]))
                {
                    dict[aminoacid[i]]++;
                }
                else 
                { 
                    dict.Add(aminoacid[i], 1);
                    keys.Add(aminoacid[i]);
                }
            }
            int MAX = dict.Values.Max();
            char maxLetter='Z';
            for(int i=0;i<keys.Count;i++)
            {
                if (MAX == dict[keys[i]] && keys[i]<maxLetter)
                {
                    maxLetter = keys[i];
                }
            }
            return maxLetter;
            
        }
        public bool search()
        {


            return false;
        }
        public int diff(Protein another)
        {
            int length;
            int count=0;
            if (aminoacid.Length < another.aminoacid.Length)
                length = aminoacid.Length;
            else length = another.aminoacid.Length;
            for(int i=0;i<length;i++)
            {
                if (aminoacid[i] != another.aminoacid[i])
                {
                    count++;
                }    
            }
            count += Math.Abs(aminoacid.Length - another.aminoacid.Length);
            return count;
        }

    }

    class Proteins
    {
        List<Protein> proteins;

        
    }

}


