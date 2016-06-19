using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support
{
    public class MyException : Exception
    {
        public MessageBox.Erro Error { get; set; }

        public MyException(string message, MessageBox.Erro error)
            : base(message)
        {
            Error = error;
        }

        public MyException(string message, Exception innerException, MessageBox.Erro error)
            : base(message, innerException)
        {
            Error = error;
        }
    }
}
