using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matricula.Application.Shared
{
    public class Response<T>
    {
        public int Status { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }


        public Response(string message, int status)
        {
            Message = message;
            Status = status;
        }

        public Response(string message, int status, T data)
        {
            Message = message;
            Status = status;
            Data = data;
        }


        public Response(string message, List<T> teacherss, T data)
        {
            Message = message;
            Status = 200;
            Data = data;
        }

        public Response(string message, T data)
        {
            Message = message;
            Data = data;
            Status = 201;
        }
    }
}
