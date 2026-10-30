using NDifference.Exceptions;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NDifference.Plugins
{
	/// <summary>
	/// Loads plugin assemblies once and owns their instances and load contexts.
	/// </summary>
	public sealed class PluginCatalog : IDisposable
	{
		private readonly List<IInspector> inspectors = new List<IInspector>();
		private readonly List<IReportWriter> reportWriters = new List<IReportWriter>();
		private readonly List<PluginLoadContext> loadContexts = new List<PluginLoadContext>();
		private bool disposed;

		public PluginCatalog(params IFileFinder[] finders)
		{
			if (finders == null)
			{
				throw new ArgumentNullException(nameof(finders));
			}

			try
			{
				IEnumerable<IFile> files = finders
					.Where(finder => finder != null)
					.SelectMany(finder =>
					{
						finder.Filter = FileFilterConstants.AssemblyFilter;
						return finder.Find();
					})
					.GroupBy(file => file.FullPath, StringComparer.OrdinalIgnoreCase)
					.Select(group => group.First());

				foreach (IFile file in files)
				{
					this.Load(file.FullPath);
				}
			}
			catch
			{
				this.Dispose();
				throw;
			}
		}

		public ReadOnlyCollection<IInspector> Inspectors
		{
			get
			{
				this.ThrowIfDisposed();
				return new ReadOnlyCollection<IInspector>(this.inspectors);
			}
		}

		public ReadOnlyCollection<IReportWriter> ReportWriters
		{
			get
			{
				this.ThrowIfDisposed();
				return new ReadOnlyCollection<IReportWriter>(this.reportWriters);
			}
		}

		public InspectorRepository CreateInspectorRepository()
		{
			this.ThrowIfDisposed();

			var repository = new InspectorRepository();
			repository.AddRange(this.inspectors);
			return repository;
		}

		public IReportingRepository CreateReportingRepository()
		{
			this.ThrowIfDisposed();

			var repository = new ReportingRepository();
			repository.AddRange(this.reportWriters);
			return repository;
		}

		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}

			this.disposed = true;

			foreach (IDisposable plugin in this.inspectors
				.Cast<object>()
				.Concat(this.reportWriters.Cast<object>())
				.OfType<IDisposable>()
				.Distinct())
			{
				plugin.Dispose();
			}

			this.inspectors.Clear();
			this.reportWriters.Clear();

			foreach (PluginLoadContext loadContext in this.loadContexts)
			{
				Release(loadContext);
			}

			this.loadContexts.Clear();
		}

		private void Load(string path)
		{
			PluginLoadContext loadContext = null;
			IAssemblyReflector reflector;

			try
			{
				loadContext = new PluginLoadContext(path, true);
				reflector = new MsReflector(loadContext.LoadPluginAssembly());
			}
			catch (BadImageFormatException)
			{
				if (loadContext != null)
				{
					Release(loadContext);
				}

				return;
			}
			catch (Exception exception)
			{
				if (loadContext != null)
				{
					Release(loadContext);
				}

				throw new PluginLoadException(path, "load", exception);
			}

			IRuntimeTypeProvider provider;

			try
			{
				provider = reflector as IRuntimeTypeProvider;

				if (provider == null)
				{
					throw new InvalidOperationException(
						"The selected reflector cannot provide runtime plugin types.");
				}

				reflector.GetTypes(AssemblyReflectionOption.Public).Any();
			}
			catch (Exception exception)
			{
				Release(loadContext);
				throw new PluginLoadException(path, "inspect types in", exception);
			}

			List<IInspector> discoveredInspectors;
			List<IReportWriter> discoveredReportWriters;

			try
			{
				discoveredInspectors =
					new ObjectInstantiator<IInspector>()
						.CreateTypesImplementingInterface(provider);
				discoveredReportWriters =
					new ObjectInstantiator<IReportWriter>()
						.CreateTypesImplementingInterface(provider);
			}
			catch (Exception exception)
			{
				Release(loadContext);
				throw new PluginLoadException(path, "activate types from", exception);
			}

			if (!discoveredInspectors.Any() && !discoveredReportWriters.Any())
			{
				Release(loadContext);
				return;
			}

			this.inspectors.AddRange(discoveredInspectors);
			this.reportWriters.AddRange(discoveredReportWriters);
			this.loadContexts.Add(loadContext);
		}

		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(nameof(PluginCatalog));
			}
		}

		private static void Release(PluginLoadContext loadContext)
		{
#if NET10_0_OR_GREATER
			if (loadContext != null && loadContext.IsCollectible)
			{
				loadContext.Unload();
			}
#endif
		}
	}
}
