using ERPAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPMVC.DTO
{
    public class ResponseDTO<T>
    {
        public List<Alert> alerts { get; set; }
        public int Estatus { get; set; }

        public T model { get; set; }
    }
}
