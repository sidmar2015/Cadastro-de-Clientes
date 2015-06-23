using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDePessoas
{
    public class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        //limitei a quantidade de números da sorte para 3 para facilitar os testes
        public int[] numSorte = new int[3];
    }
}
