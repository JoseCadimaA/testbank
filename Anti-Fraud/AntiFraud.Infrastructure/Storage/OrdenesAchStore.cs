using AntiFraud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Infrastructure.Storage
{
    public class OrdenesAchStore
    {
        private static List<OrdenACH> _ordenes;

        public static void Add(OrdenACH orden)
        {
            if (_ordenes == null)
                _ordenes = new List<OrdenACH>();

            _ordenes.Add(orden);
        }

        public static decimal GetTotalTransferBySourceAccount(Guid sourceAccountId)
        {
            if (_ordenes == null)
                _ordenes = new List<OrdenACH>();

            return _ordenes
            .Where(o => o.SourceAccountId == sourceAccountId)
            .Sum(o => o.Amount);
        }

    }
}
