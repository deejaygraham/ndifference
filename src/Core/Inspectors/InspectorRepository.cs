using NDifference.Exceptions;
using NDifference.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NDifference.Inspectors
{
	public class InspectorRepository
	{
		private List<IAssemblyCollectionInspector> aci = new List<IAssemblyCollectionInspector>();

		private List<IAssemblyInspector> ai = new List<IAssemblyInspector>();

		private List<ITypeCollectionInspector> tci = new List<ITypeCollectionInspector>();

		private List<ITypeInspector> ti = new List<ITypeInspector>();

        private List<IAnalysisInspector> iai = new List<IAnalysisInspector>();

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


        public ReadOnlyCollection<IAnalysisInspector> AnalysisInspectors
        {
            get
            {
                return new ReadOnlyCollection<IAnalysisInspector>(this.iai);
            }
        }

		public void Find(IFileFinder finder)
		{
            try
            {
                var pluginDiscoverer = new PluginDiscoverer<IInspector>(finder);
                var plugins = pluginDiscoverer.Find();

                this.aci.AddRange(plugins.OfType<IAssemblyCollectionInspector>());
                this.aci.ForEach(x => x.Enabled = true);

                this.ai.AddRange(plugins.OfType<IAssemblyInspector>());
                this.ai.ForEach(x => x.Enabled = true);

                this.tci.AddRange(plugins.OfType<ITypeCollectionInspector>());
                this.tci.ForEach(x => x.Enabled = true);

                this.ti.AddRange(plugins.OfType<ITypeInspector>());
                this.ti.ForEach(x => x.Enabled = true);

                this.iai.AddRange(plugins.OfType<IAnalysisInspector>());
                this.iai.ForEach(x => x.Enabled = true);
            }
            catch (AggregateException ae)
            {
                StringBuilder message = new StringBuilder();

                foreach (Exception e in ae.InnerExceptions)
                {
                    message.AppendLine(e.GetBaseException().Message);
                }

                throw new PluginLoadException(message.ToString());
            }
            catch (Exception ex)
            {
                throw new PluginLoadException(ex.GetBaseException().Message);
            }
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
