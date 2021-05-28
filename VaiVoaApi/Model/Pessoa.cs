using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaiVoaApi.Model
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<Cartao> CartoesVirtuais { get; set; }
    }
}
