using NDifference.Exceptions;
using NDifference.Plugins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NDifference.Inspectors
{
	/// <summary>
	/// Contains all inspectors, collected by type.
	/// </summary>
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

                this.AddRange(plugins);
            }
            catch (PluginLoadException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PluginLoadException(
                    "Inspector plugin discovery failed. " + ex.Message,
                    ex);
            }
        }

		public void AddRange(IEnumerable<IInspector> inspectors)
		{
			if (inspectors == null)
			{
				throw new ArgumentNullException(nameof(inspectors));
			}

			IInspector[] plugins = inspectors.ToArray();

			foreach (IInspector inspector in plugins)
			{
				inspector.Enabled = true;
			}

			this.aci.AddRange(plugins.OfType<IAssemblyCollectionInspector>());
			this.ai.AddRange(plugins.OfType<IAssemblyInspector>());
			this.tci.AddRange(plugins.OfType<ITypeCollectionInspector>());
			this.ti.AddRange(plugins.OfType<ITypeInspector>());
			this.iai.AddRange(plugins.OfType<IAnalysisInspector>());
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
