namespace Bwa.Core.IO
{
    public interface IWriter
    {
        void Write(string text);
        void WriteLine(string text);
        void WriteLine();
    }
}
