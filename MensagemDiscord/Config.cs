using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensagemDiscord
{
    public class Config
    {
        public string Usuario { get; set; }
        public string Url { get; set; }
        public string Mensagem { get; set; }


        public static Config NewConfig()
        {
            return new Config { Mensagem = "", Url = "", Usuario = "" };
        }

    }
}
