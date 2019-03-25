using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator;
using Ninject.Modules;
using Repository;

namespace SimpleCalculatorConsole
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDiagnostics>().To<DbUsingEntityFramework>().Named("DbEntity");
            Bind<ICalculatorRepository>().To<CalculatorRepositoryEntityFramework>();
            Bind<IDummyDiagnostics>().To<DummyDiagnostics>();
            Bind<ISimpleCalculator>().To<SimpleCalculator>();
        }
    }
}
