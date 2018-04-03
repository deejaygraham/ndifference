using NDifference.Exceptions;
using NDifference.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Inspectors
{
	public class InspectorRepository
	{
		private List<IAssemblyCollectionInspector> aci = new List<IAssemblyCollectionInspector>();

		private List<IAssemblyInspector> ai = new List<IAssemblyInspector>();

		private List<ITypeCollectionInspector> tci = new List<ITypeCollectionInspector>();

		private List<ITypeInspector> ti = new List<ITypeInspector>();

		public ReadOnlyCollection<IAssemblyCollectionInspector> AssemblyCollectionInspectors 
		{ 
			get
			{
				return new ReadOnlyCollection<IAssemblyCollectionInspector>(this.aci);
			}
		}

		public ReadOnlyCollection<IAssemblyInspector> AssemblyInspectors 
		{ 
			get
			{
				return new ReadOnlyCollection<IAssemblyInspector>(this.ai);
			}
		}

		public ReadOnlyCollection<ITypeCollectionInspector> TypeCollectionInspectors
		{
			get
			{
				return new ReadOnlyCollection<ITypeCollectionInspector>(this.tci);
			}
		}

		public ReadOnlyCollection<ITypeInspector> TypeInspectors
		{
			get
			{
				return new ReadOnlyCollection<ITypeInspector>(this.ti);
			}
		}

		public void Find(IFileFinder finder)
		{
			Task t1 = Task.Run(() => 
			{
				this.aci.AddRange(new AssemblyCollectionInspectorPluginDiscoverer(finder).Find());
				this.aci.ForEach(x => x.Enabled = true);
			});

			Task t2 = Task.Run(() => 
			{
				this.ai.AddRange(new AssemblyInspectorPluginDiscoverer(finder).Find());
				this.ai.ForEach(x => x.Enabled = true);
			});

			Task t3 = Task.Run(() => 
			{
				this.tci.AddRange(new TypeCollectionInspectorPluginDiscoverer(finder).Find());
				this.tci.ForEach(x => x.Enabled = true);
			});

			Task t4 = Task.Run(() => 
			{
				this.ti.AddRange(new TypeInspectorPluginDiscoverer(finder).Find());
				this.ti.ForEach(x => x.Enabled = true);
			});

            Task.Factory.ContinueWhenAll(new[] { t1, t2, t3, t4 }, tasks =>
            {
                StringBuilder builder = new StringBuilder();

                foreach (var t in tasks)
                {
                    if (t.Status == TaskStatus.Faulted)
                    {
                        builder.AppendLine(t.Exception.GetBaseException().Message);
                    }
                }

                string message = builder.ToString();

                if (!String.IsNullOrEmpty(message))
                {
                    throw new PluginLoadException(message);
                }
            });
		}

		public void Filter(InspectorFilter filter)
		{
			filter.Filter(this.aci);
			filter.Filter(this.ai);
			filter.Filter(this.tci);
			filter.Filter(this.ti);
		}
	}
}
