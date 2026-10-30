using NDifference.Exceptions;
using NDifference.Inspection;
using NDifference.Inspectors;
using NDifference.Reporting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace NDifference.Plugins
{
	/// <summary>
	/// Loads plugin assemblies once and owns their instances for the process lifetime.
	/// </summary>
	public sealed class PluginCatalog : IDisposable
	{
		private readonly List<IInspector> inspectors = new List<IInspector>();
		private readonly List<IReportWriter> reportWriters = new List<IReportWriter>();
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
		}

		private void Load(string path)
		{
			Assembly assembly;

			try
			{
				AssemblyName.GetAssemblyName(path);
				assembly = new PluginLoadContext(path).LoadPluginAssembly();
			}
			catch (BadImageFormatException)
			{
				return;
			}
			catch (Exception exception)
			{
				throw new PluginLoadException(path, "load", exception);
			}

			RuntimeTypeProvider provider;

			try
			{
				provider = new RuntimeTypeProvider(assembly);
			}
			catch (Exception exception)
			{
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
				throw new PluginLoadException(path, "activate types from", exception);
			}

			this.inspectors.AddRange(discoveredInspectors);
			this.reportWriters.AddRange(discoveredReportWriters);
		}

		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(nameof(PluginCatalog));
			}
		}

		private sealed class RuntimeTypeProvider : IRuntimeTypeProvider
		{
			private readonly Type[] types;

			public RuntimeTypeProvider(Assembly assembly)
			{
				this.types = assembly.GetTypes();
			}

			public IEnumerable<Type> GetRuntimeTypesImplementing(Type requiredInterface)
			{
				return this.types.Where(
					type => !type.IsAbstract
						&& !type.IsInterface
						&& requiredInterface.IsAssignableFrom(type));
			}
		}
	}
}
