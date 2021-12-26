namespace Alleles
{
    public delegate void PhenotypeExecutioner(ref Wight sender);
    public class PhenotypeExecute
    {
        public string Execution {get;set;}
        public PhenotypeExecutioner Execute {get;set;}
    }
          
}