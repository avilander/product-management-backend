using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Abstractions
{
    /// <summary>
    /// Representa um valor nulo (sem request) em casos de uso que não precisam de entrada.
    /// </summary>
    public sealed class Unit
    {
        public static readonly Unit Value = new();
        private Unit() { }
    }
}
