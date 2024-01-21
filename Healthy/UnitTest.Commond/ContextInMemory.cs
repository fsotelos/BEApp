using Healthy.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.UnitTest.Commond
{
    public class ContextInMemory
    {
        public HealthyDbContext GetContext()
        {
            //Indicamos que utilizará base de datos en memoria
            //y que no deseamos que marque error si realizamos
            //transacciones en el código de nuestra aplicación
            var options = new DbContextOptionsBuilder<HealthyDbContext>()
                          .ConfigureWarnings
                          (x => x.Ignore(InMemoryEventId
                                    .TransactionIgnoredWarning))
                          .EnableSensitiveDataLogging (true)
                          .UseInMemoryDatabase(databaseName: "TestHealthy")
                                   .Options;
            //Inicializamos la configuración de la base de datos
            var context = new HealthyDbContext(options);
            //Mandamos llamar la función para inicializar los 
            //datos de prueba
            
            return context;
        }
    }
}
