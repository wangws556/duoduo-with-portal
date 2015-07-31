using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unisys.CPF.Databus.Common
{
	public class FactoryExportDefinition<T> : ExportDefinition
	{
		private readonly string contractName;

		public FactoryExportDefinition(string contractName, Type type, Func<Type, T> resolutionMethod)
		{
			this.contractName = contractName;
			ResolutionMethod = resolutionMethod;
			ServiceType = type;
		}

		public override string ContractName
		{
			get { return contractName; }
		}

		public Type ServiceType { get; private set; }
		public Func<Type, T> ResolutionMethod { get; private set; }
	}
}
