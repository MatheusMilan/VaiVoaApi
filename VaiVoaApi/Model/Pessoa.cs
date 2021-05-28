using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVoaApi.Model
{
    public class Pessoa
    {
        public Pessoa(string email, List<Cartao> cartoesVirtuais)
        {
            Email = email;
            CartoesVirtuais = cartoesVirtuais;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public List<Cartao> CartoesVirtuais { get; set; }
    }
}
