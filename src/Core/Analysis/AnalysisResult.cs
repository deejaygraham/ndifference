using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDifference.Analysis
{
	/// <summary>
	/// Composite object used to capture all changes identified when
	/// analyzing two collections of assemblies.
    /// </summary>
	public class AnalysisResult
    {
        private IdentifiedChangeCollection _breakingChanges = new IdentifiedChangeCollection();

		private IdentifiedChangeCollection _topLevelChanges = new IdentifiedChangeCollection();

		private List<IdentifiedChangeCollection> _assemblyLevelChanges = new List<IdentifiedChangeCollection>();

		private List<IdentifiedChangeCollection> _typeLevelChanges = new List<IdentifiedChangeCollection>();

		public bool Cancelled { get; set; }


		// https://docs.microsoft.com/en-us/dotnet/core/compatibility/
		// type moved to another namespace
		// remove public type
		// unsealed to sealed

		// remove enum member
		// remove getter or setting of property or changing visibility
		// new member to an interface

		// change value of enumeration
		// change value of public constant

		// change type of return value, type of property
		// add remove or change order of parameters
		// adding or removing abstract from member
		// adding virtual to a member
		// virtual member now abstract
		// add or remove static from member
		// new constructor if no other constructor added for default
		// add readonly to a field
		// reducing visibility of a member
		// changing type of a member

		// removing assembly
		// changing public key of an assembly
		// need summary of breaking changes - pull out from all method signature changes
		// methods removed
		// properties removed
		// summary of obsolete methods

		public IdentifiedChangeCollection Summary
		{
			get
			{
				return this._topLevelChanges;
			}
            set
            {
                this._topLevelChanges = value;
            }
		}

		public ReadOnlyCollection<IdentifiedChangeCollection> AssemblyLevelChanges
		{
			get
			{
				return new ReadOnlyCollection<IdentifiedChangeCollection>(this._assemblyLevelChanges);
			}
		}

        public List<IdentifiedChangeCollection> AssemblyChangesModifiable
        {
            get
            {
                return this._assemblyLevelChanges;
            }
        }

		public ReadOnlyCollection<IdentifiedChangeCollection> TypeLevelChanges
		{
			get
			{
				return new ReadOnlyCollection<IdentifiedChangeCollection>(this._typeLevelChanges);
			}
		}

        public IdentifiedChangeCollection BreakingChanges
        {
            get
            {
                return this._breakingChanges;
			}
        }

		public void ReplaceSummary(IdentifiedChangeCollection summary)
		{
			this._topLevelChanges = summary;
		}

		public void Assembly(IdentifiedChangeCollection assembly)
		{
			this._assemblyLevelChanges.Add(assembly);
		}

		public void Type(IdentifiedChangeCollection type)
		{
			this._typeLevelChanges.Add(type);
		}
	}
}
