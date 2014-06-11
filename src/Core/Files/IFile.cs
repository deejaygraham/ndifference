
namespace NDifference
{
	public interface IFile
	{
		string Name { get; }

		string Folder { get; }

		string FullPath { get; }

		string RelativeTo(IFile other);
	}
}
