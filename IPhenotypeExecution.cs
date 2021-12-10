namespace Alleles
{
    public delegate void PhenotypeExecute(ref Wight sender);
    public interface IPhenotypeExecution
    {
        public string Execution {get;set;}
        public PhenotypeExecute Execute {get;set;}
    }
          
}