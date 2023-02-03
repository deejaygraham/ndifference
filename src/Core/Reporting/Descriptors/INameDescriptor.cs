
namespace NDifference.Reporting
{
	public interface INameDescriptor : IDescriptor
	{
		string Name { get; set; }

        string Reason { get; }
    }
}
